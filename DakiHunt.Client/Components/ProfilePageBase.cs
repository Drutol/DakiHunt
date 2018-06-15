using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.Client.Interfaces;
using Microsoft.AspNetCore.Blazor.Components;

namespace DakiHunt.Client.Components
{
    public class ProfilePageBase : BlazorComponent
    {
        [Inject]
        public IApiCommunicator ApiCommunicator { get; set; }

        public string Email { get; set; }

        protected override async Task OnInitAsync()
        {
            Email = await ApiCommunicator.GetMyEmail();
        }
    }
}
