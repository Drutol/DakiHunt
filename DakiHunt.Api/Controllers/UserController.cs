using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities.Auth;
using DakiHunt.DataAccess.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DakiHunt.Api.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors("GlobalPolicy")]
    [Route("[controller]/[action]")]
    public class UserController : ApiControllerBase
    {
        public UserController(IUserService userService, UserManager<AuthUser> userManager) : base(userService, userManager)
        {

        }

        [HttpGet]
        [ActionName("me")]
        public async Task<IActionResult> GetMyInfo()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return Ok(user.Email);
        }
    }
}
