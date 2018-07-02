using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities.Auth;
using DakiHunt.DataAccess.Interfaces.Service;
using DakiHunt.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DakiHunt.Api.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors("GlobalPolicy")]
    public class UserController : ApiControllerBase
    {
        public UserController(IUserService userService, UserManager<AuthUser> userManager) : base(userService, userManager)
        {

        }

        [HttpGet]
        [ActionName("me")]
        public async Task<IActionResult> GetMyInfo()
        {
            var user = await CurrentAuthUser;
            var appUser = await CurrentUser;
            return new JsonResult(new AccountInfoDto
            {
                Username = user.UserName,
                Email = user.UserName,
                RegisteredAt = appUser.RegistrationDateTime,
                UserProperties = new UserPropertiesDto
                {
                    AccountType = appUser.AccountProperties.AccountType,
                    MaxHunts = appUser.AccountProperties.MaxHunts,
                    MinHuntUpdateTimeInSeconds = appUser.AccountProperties.MinHuntUpdateTimeInSeconds
                }
            });
        }
    }
}
