using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Logging;
using DakiHunt.Client.Interfaces;
using Microsoft.Extensions.Logging;

namespace DakiHunt.Client.BL
{
    public class ApiCommunicator : IApiCommunicator
    {
        private readonly IAuthorizationProvider _authorizationProvider;
        private readonly ILogger<ApiCommunicator> _logger;

        public ApiCommunicator(IAuthorizationProvider authorizationProvider, ILogger<ApiCommunicator> logger)
        {
            _authorizationProvider = authorizationProvider;
            _logger = logger;
        }

        public async Task<string> GetMyEmail()
        {
            _logger.LogDebug("Entered GetMyEmail");
            var client = await _authorizationProvider.ObtainAuthenticatedHttpClient();
            if (client == null)
                return "";

            try
            {
                var response = await client.GetAsync("user/me");
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                _logger.LogWarning("Failed GetMyEmail");
                return null;
            }
        }
    }
}
