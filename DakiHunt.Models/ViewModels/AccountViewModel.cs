using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DakiHunt.Models.ViewModels
{
    public class AccountViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
