using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.Client.Interfaces;
using Microsoft.AspNetCore.Blazor.Components;

namespace DakiHunt.Client.Components
{
    public class NavMenuBase : BlazorComponent
    {
        [Inject]
        public IAuthorizationProvider AuthorizationProvider { get; set; }

        protected override void OnInit()
        {
            AuthorizationProvider.OnAuthStatusChanged += AuthorizationProviderOnOnAuthStatusChanged;
            base.OnInit();
        }

        private void AuthorizationProviderOnOnAuthStatusChanged(object sender, bool e)
        {
            StateHasChanged();
        }
    }
}
