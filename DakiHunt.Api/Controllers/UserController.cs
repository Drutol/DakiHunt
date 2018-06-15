using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DakiHunt.Api.Controllers
{
    [ApiController]
    [Authorize]
    [EnableCors("GlobalPolicy")]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [ActionName("me")]
        public async Task<IActionResult> GetMyInfo()
        {
            var user = await _userManager.GetUserAsync(User);

            return Ok(user.Email);
        }
    }
}
