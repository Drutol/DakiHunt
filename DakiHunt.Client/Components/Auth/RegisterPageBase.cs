using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.Client.Interfaces;
using DakiHunt.Models.ViewModels;
using Microsoft.AspNetCore.Blazor.Components;

namespace DakiHunt.Client.Components.Auth
{
    public class RegisterPageBase : BlazorComponent
    {
        [Inject]
        public IAuthorizationProvider AuthorizationProvider { get; set; }

        protected RegisterViewModel RegisterViewModel { get; } = new RegisterViewModel();
        protected Dictionary<string, string> Errors { get; set; }

        public async Task Register()
        {
            var result = await AuthorizationProvider.Register(RegisterViewModel);
        }
    }
}
