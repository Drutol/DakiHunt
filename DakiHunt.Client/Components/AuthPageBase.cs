using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DakiHunt.Client.Interfaces;
using DakiHunt.Models.ViewModels;
using Microsoft.AspNetCore.Blazor.Components;

namespace DakiHunt.Client.Components
{
    public class AuthPageBase : BlazorComponent
    {
        [Inject]
        private IAuthorizationProvider AuthorizationProvider { get; set; }

        protected readonly SignInViewModel SignInViewModel = new SignInViewModel();
        protected Dictionary<string, string> Errors;

        protected async Task SignIn()
        {
            Debug.WriteLine("Entering SignIn");
            var result = await AuthorizationProvider.SignIn(SignInViewModel);
            Debug.WriteLine(result);
            //await ClientFactory.Create("/api/account/login")
            //    .OnBadRequest<Dictionary<string, string>>(errors => _errors = errors)
            //    .OnOK(null, "/")
            //    .Post(SignInViewModel);
            //StateHasChanged();
        }

    }
}
