using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Blazor.Extensions;
using DakiHunt.Models.Auth;
using Microsoft.AspNetCore.Blazor;

namespace DakiHunt.Client.BL
{
    public class AppVariables
    {
        private readonly LocalStorage _localStorage;
        private TokenModel _tokenModel;

        public AppVariables(LocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public TokenModel TokenModel
        {
            get => ReadValue(ref _tokenModel);
            set => SaveValue(ref _tokenModel, value);
        }

        private T ReadValue<T>(ref T value, [CallerMemberName] string property = "") where T : class
        {
            return value ?? JsonUtil.Deserialize<T>(_localStorage.GetItem<string>(property));
        }

        private void SaveValue<T>(ref T backingField, T value, [CallerMemberName] string property = "") where T : class 
        {
            backingField = value;
            _localStorage.SetItem(property, JsonUtil.Serialize(value));
        }
    }
}
