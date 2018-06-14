using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using Blazor.Extensions.Storage;
using DakiHunt.Client.BL;
using DakiHunt.Client.Interfaces;

namespace DakiHunt.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddStorage();
                services.AddSingleton<IApiCommunicator,ApiCommunicator>();
                services.AddSingleton<IAuthorizationProvider,AuthorizationProvider>();
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
