using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.Client.Interfaces;

namespace DakiHunt.Client.BL
{
    public class ApiCommunicator : IApiCommunicator
    {
        private readonly IAuthorizationProvider _authorizationProvider;

        public ApiCommunicator(IAuthorizationProvider authorizationProvider)
        {
            _authorizationProvider = authorizationProvider;
        }

        public async Task<string> GetMyEmail()
        {
            return "lel";
        }
    }
}
