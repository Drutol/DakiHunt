using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DakiHunt.DataAccess.Entities.Auth
{
    public class AuthUser : IdentityUser
    {
#if DEBUG
        public const string DebugRefreshToken = "FB6EDDBA-0C49-42E9-8E13-8E681349F46F";
        public const string DebugUsername = "Test";

        public bool DebugUser { get; set; }
#endif

        public string RefreshToken { get; set; }         
    }
}
