using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities;
using DakiHunt.DataAccess.Entities.Auth;
using DakiHunt.DataAccess.Interfaces.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DakiHunt.Api.Controllers
{
    public class ApiControllerBase : Controller
    {
        // ReSharper disable once InconsistentNaming
        protected readonly IUserService _userService;
        protected readonly UserManager<AuthUser> _userManager;

        public ApiControllerBase(IUserService userService, UserManager<AuthUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        private AppUser _currentUser;
        private AuthUser _currentAuthUser;

        private async Task<AuthUser> GetCurrrentAuthUser()
        {
            if (_currentAuthUser != null)
                return _currentAuthUser;

            _currentAuthUser = await _userManager.GetUserAsync(User);

            return _currentAuthUser;
        }

        private async Task<AppUser> GetCurrrentUser()
        {
            if (_currentUser != null)
                return _currentUser;

            var authUser = await GetCurrrentAuthUser();
            _currentUser = await _userService.FirstAsync(user => user.AuthUserKey.Equals(authUser.Id));

            return _currentUser;
        }

        public Task<AppUser> CurrentUser => GetCurrrentUser();
        public Task<AuthUser> CurrentAuthUser => GetCurrrentAuthUser();
    }
}
