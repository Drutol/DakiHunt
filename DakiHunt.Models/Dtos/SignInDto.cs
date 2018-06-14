using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.Models.ViewModels;

namespace DakiHunt.Models.Dtos
{
    public class SignInDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public static implicit operator SignInDto(SignInViewModel viewModel)
        {
            return new SignInDto
            {
                Username = viewModel.Username,
                Password = viewModel.Password
            };
        }
    }
}
