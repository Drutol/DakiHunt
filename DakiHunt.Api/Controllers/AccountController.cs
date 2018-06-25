using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities.Auth;
using DakiHunt.Models.Auth;
using DakiHunt.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DakiHunt.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [EnableCors("GlobalPolicy")]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly UserManager<AuthUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] SignInDto model)
        {
            if (model == null || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Username))
                return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            
            if (result.Succeeded)
            {
                var user = _userManager.Users.SingleOrDefault(r => r.UserName.Equals(model.Username,StringComparison.CurrentCultureIgnoreCase));
                user.RefreshToken = GenerateRefreshToken(user);
                user.LockoutEnabled = false;
                await _userManager.UpdateAsync(user);
                return Ok(GenerateJwtToken(user));
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new AuthUser
            {
                Email = model.Email,
                UserName = model.Username,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                user.RefreshToken = GenerateRefreshToken(user);
                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, false);
                return Ok(GenerateJwtToken(user));
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken()
        {
            string refreshToken = "";
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                refreshToken = await reader.ReadToEndAsync();
            }

            AuthUser user = null;
#if DEBUG
            if (refreshToken == AuthUser.DebugRefreshToken)
            {
                user = _userManager.Users.FirstOrDefault(authUser => authUser.UserName == AuthUser.DebugUsername);
            }
#else
            var user = _userManager.Users.FirstOrDefault(appUser => appUser.RefreshToken == refreshToken);
#endif

            if (user != null)
            {
                user.RefreshToken = GenerateRefreshToken(user);
                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, false);
                return Ok(GenerateJwtToken(user));
            }

            return Forbid();
        }

        private string GenerateRefreshToken(AuthUser user)
        {
#if DEBUG
            return user.DebugUser ? AuthUser.DebugRefreshToken : Guid.NewGuid().ToString();
#else
            return Guid.NewGuid().ToString();
#endif
        }

        private TokenModel GenerateJwtToken(AuthUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new TokenModel
            {
                ExpirationDate = expires,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = user.RefreshToken
            };
        }
    }
}
