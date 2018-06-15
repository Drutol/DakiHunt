using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DakiHunt.DataAccess.Entities.Auth
{
    public class AppUser : IdentityUser
    {
        public string RefreshToken { get; set; }
    }
}
