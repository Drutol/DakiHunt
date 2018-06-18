using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;

namespace DakiHunt.Client.Shared.Bases
{
    public class MessageBoxBase : BlazorComponent
    {
        private bool _isOpen;
        private TaskCompletionSource<bool> _tcs;
        public static MessageBoxBase Instance { get; private set; }

        public MessageBoxBase()
        {
            Instance = this;
        }

        public string Title { get; set; }
        public string Content { get; set; }

        public async Task<bool> Show()
        {
            _tcs = new TaskCompletionSource<bool>();
            IsOpen = true;
            return await _tcs.Task;
        }

        protected bool IsOpen
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                StateHasChanged();
            }
        }

        protected void OnCancelClick()
        {
            _tcs.SetResult(false);
            IsOpen = false;
        }

        protected void OnPrimaryClick()
        {
            _tcs.SetResult(true);
            IsOpen = false;
        }

        protected void OnSecondaryClick()
        {
            _tcs.SetResult(false);
            IsOpen = false;
        }

        protected void OnBackgroundClick()
        {
            _tcs.SetResult(false);
            IsOpen = false;
        }
    }
}
