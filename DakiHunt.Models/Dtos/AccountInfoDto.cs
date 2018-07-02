using System;
using System.Collections.Generic;
using System.Text;

namespace DakiHunt.Models.Dtos
{
    public class AccountInfoDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime RegisteredAt { get; set; }
        public UserPropertiesDto UserProperties { get; set; }
    }
}
