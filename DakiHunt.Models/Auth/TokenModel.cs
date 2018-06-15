using System;
using System.Collections.Generic;
using System.Text;

namespace DakiHunt.Models.Auth
{
    public class TokenModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
