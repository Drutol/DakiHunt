using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.Models.ViewModels;

namespace DakiHunt.Models.Dtos
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }

        public static implicit operator RegisterDto(RegisterViewModel viewModel)
        {
            return new RegisterDto
            {
                Email = viewModel.Email,
                Username = viewModel.Username,
                Password = viewModel.Password,
                RepeatPassword = viewModel.ConfirmPassword              
            };
        }
    }
}
