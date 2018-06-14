using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.Extensions;
using DakiHunt.Client.Interfaces;
using DakiHunt.Models.Dtos;
using DakiHunt.Models.ViewModels;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Browser.Http;

namespace DakiHunt.Client.BL
{
    public class AuthorizationProvider : IAuthorizationProvider
    {
        private const string ApiUrl = "https://localhost:8765/";

        private readonly LocalStorage _localStorage;

        public AuthorizationProvider(LocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<bool> SignIn(SignInViewModel signInViewModel)
        {            
            Debug.WriteLine("Entered signin");
            using (var httpClient = new HttpClient(new BrowserHttpMessageHandler()) {BaseAddress = new Uri(ApiUrl)})
            {
                Debug.WriteLine("created http client");
                var response = await httpClient.PostAsync("/account/login",
                    new StringContent(JsonUtil.Serialize((SignInDto) signInViewModel)));
                Debug.WriteLine("made request");
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("read request");
                Debug.WriteLine(content);
            }

            return true;
        }

        public Task<bool> Register(RegisterViewModel registerViewModel)
        {
           throw new NotImplementedException();
        }

        public Task<HttpClient> ObtainAuthenticatedHttpClient()
        {
            throw new NotImplementedException();
        }
    }
}
