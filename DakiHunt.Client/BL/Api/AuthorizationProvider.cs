using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using DakiHunt.Client.Interfaces;
using DakiHunt.Client.Utils;
using DakiHunt.Models.Auth;
using DakiHunt.Models.Dtos;
using DakiHunt.Models.ViewModels;
using Microsoft.AspNetCore.Blazor.Browser.Http;
using Microsoft.Extensions.Logging;

namespace DakiHunt.Client.BL.Api
{
    public class AuthorizationProvider : IAuthorizationProvider
    {
        private const string ApiUrl = "https://localhost:44357/";

        private readonly AppVariables _appVariables;
        private readonly ILogger<AuthorizationProvider> _logger;
        private HttpClient _authenticatedHttpClient;
        private bool _initialized;
        private bool _initializing;
        private SemaphoreSlim _initSemaphore;
        private bool _isAuthorized;
        private TokenModel _token;

        public event EventHandler<bool> OnAuthStatusChanged;

        public AuthorizationProvider(AppVariables appVariables, ILogger<AuthorizationProvider> logger)
        {
            _appVariables = appVariables;
            _logger = logger;

            Initialize();
        }

        private TokenModel Token
        {
            get => _token;
            set
            {
                _token = value;
                _appVariables.TokenModel = value;
            }
        }

        public bool IsAuthorized
        {
            get => _isAuthorized;
            set
            {
                _isAuthorized = value;
                OnAuthStatusChanged?.Invoke(this, value);
            }
        }

        public async Task<bool> SignIn(SignInViewModel signInViewModel)
        {
            _logger.LogInformation("Entered SignIn");
            using (var httpClient = new HttpClient(new BrowserHttpMessageHandler()) {BaseAddress = new Uri(ApiUrl)})
            {
                try
                {
                    var response = await httpClient.PostAsync("/account/login",
                        new JsonHttpContent((SignInDto) signInViewModel));
                    response.EnsureSuccessStatusCode();

                    var token = await response.ReadContent<TokenModel>();
                    Token = token;

                    _logger.LogInformation("SignIn success.");

                    return true;
                }
                catch (WebException e)
                {
                    _logger.LogWarning($"SignIn failed. {e.Status}");
                }
                catch (Exception e)
                {
                    _logger.LogError($"SignIn errored. {e.Message}");
                }
                finally
                {
                    IsAuthorized = Token != null;
                }
            }

            return false;
        }

        public async Task<bool> Register(RegisterViewModel registerViewModel)
        {
            _logger.LogDebug("Entered Register");
            using (var httpClient = new HttpClient(new BrowserHttpMessageHandler()) {BaseAddress = new Uri(ApiUrl)})
            {
                try
                {
                    var response = await httpClient.PostAsync("/account/register",
                        new JsonHttpContent((RegisterDto) registerViewModel));
                    response.EnsureSuccessStatusCode();

                    var token = await response.ReadContent<TokenModel>();
                    Token = token;

                    _logger.LogInformation("Register success.");

                    return true;
                }
                catch (WebException e)
                {
                    _logger.LogWarning($"Register failed. {e.Status}");
                }
                catch (Exception e)
                {
                    _logger.LogError($"Register errored. {e.Message}");
                }
                finally
                {
                    IsAuthorized = Token != null;
                }
            }

            return false;
        }

        private async Task<TokenModel> RequestRefreshToken()
        {
            _logger.LogDebug("RefreshToken Register");
            using (var httpClient = new HttpClient(new BrowserHttpMessageHandler()) { BaseAddress = new Uri(ApiUrl) })
            {
                try
                {
                    var response = await httpClient.PostAsync("/account/refreshToken",
                        new StringContent(Token.RefreshToken));
                    response.EnsureSuccessStatusCode();

                    var token = await response.ReadContent<TokenModel>();                    
                    _logger.LogInformation("RefreshToken success.");

                    return token;
                }
                catch (WebException e)
                {
                    _logger.LogWarning($"RefreshToken failed. {e.Status}");
                }
                catch (Exception e)
                {
                    _logger.LogError($"RefreshToken errored. {e}");
                }
            }

            return null;
        }

        public async Task<HttpClient> ObtainAuthenticatedHttpClient()
        {
            if (!_initialized)
                await Initialize();
            try
            {
                //we have to have token
                if (Token != null)
                {
                    //let's check if it's valid
                    if (Token.ExpirationDate - DateTime.UtcNow <= TimeSpan.FromMinutes(5))
                    {
                        //oops it's not valid, let's refresh it
                        Token = await RequestRefreshToken();
                        if (Token != null)
                        {
                            _authenticatedHttpClient = CreateHttpClient();
                            return _authenticatedHttpClient;
                        }

                        return null;
                    }

                    //yeah it's valid so use existing one. 
                    if (_authenticatedHttpClient == null)
                        _authenticatedHttpClient = CreateHttpClient();

                    return CreateHttpClient();
                }

                //we don't have token, sorry
                return null;
            }
            finally
            {
                IsAuthorized = Token != null;
            }
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient(new BrowserHttpMessageHandler())
            {
                BaseAddress = new Uri(ApiUrl),
                Timeout = TimeSpan.FromSeconds(60)
            };

            if (Token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Token);

            return client;
        }

        public async Task Initialize()
        {
            if (_initialized)
                return;

            if (_initializing)
            {
                await _initSemaphore.WaitAsync();
                return;
            }

            _initSemaphore = new SemaphoreSlim(0);
            _initializing = true;
            //retrieve saved token
            Token = _appVariables.TokenModel;
            //if there was token to begin with, we will try to refresh it
            if (Token != null)
            {
                Token = await RequestRefreshToken();
                if (Token == null)
                    IsAuthorized = false;
                else
                    IsAuthorized = true;
            }

            _initialized = true;
            _initializing = false;
            _initSemaphore.Release(int.MaxValue);
        }

        public void SignOut()
        {
            Token = null;
            IsAuthorized = false;
            _authenticatedHttpClient = null;
        }
    }
}