using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DakiHunt.Models.ViewModels;

namespace DakiHunt.Client.Interfaces
{
    public interface IAuthorizationProvider
    {
        bool IsAuthorized { get; }
        event EventHandler<bool> OnAuthStatusChanged;

        Task<bool> SignIn(SignInViewModel signInViewModel);
        Task<bool> Register(RegisterViewModel registerViewModel);
        Task<HttpClient> ObtainAuthenticatedHttpClient();
    }
}
