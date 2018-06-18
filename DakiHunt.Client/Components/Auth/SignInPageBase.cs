using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DakiHunt.Client.Interfaces;
using DakiHunt.Models.ViewModels;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;

namespace DakiHunt.Client.Components.Auth
{
    public class SignInPageBase : BlazorComponent
    {
        [Inject]
        private IAuthorizationProvider AuthorizationProvider { get; set; }

        [Inject]
        private IMessageBoxProvider MessageBoxProvider { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        protected SignInViewModel SignInViewModel { get; } = new SignInViewModel();
        protected Dictionary<string, string> Errors { get; set; }

        protected async Task SignIn()
        {
            var result = await AuthorizationProvider.SignIn(SignInViewModel);
            if(!result)
                await MessageBoxProvider.ShowMessageBox("Error", "Unable to sign-in please check your credentials.");
            else
                UriHelper.NavigateTo("/");
        }

    }
}
