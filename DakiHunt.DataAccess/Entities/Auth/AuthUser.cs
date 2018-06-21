using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DakiHunt.DataAccess.Entities.Auth
{
    public class AuthUser : IdentityUser
    {
        public string RefreshToken { get; set; }
    }
}
