using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DakiHunt.Client.Interfaces;
using DakiHunt.Models.ViewModels;
using Microsoft.AspNetCore.Blazor.Components;

namespace DakiHunt.Client.Components.Auth
{
    public class SignInPageBase : BlazorComponent
    {
        [Inject]
        private IAuthorizationProvider AuthorizationProvider { get; set; }

        protected SignInViewModel SignInViewModel { get; } = new SignInViewModel();
        protected Dictionary<string, string> Errors { get; set; }

        protected async Task SignIn()
        {
            var result = await AuthorizationProvider.SignIn(SignInViewModel);
        }

    }
}
