using System;
using System.Linq;
using System.Web;
using RDPG_Twitch_TTS_Bot.Services;
using TwitchLib.Api.Core.Exceptions;

namespace RDPG_Twitch_TTS_Bot.Controllers
{
    public class AuthenticationController
    {
        public event AuthenticationFailedHandler AuthenticationFailed;

        public delegate void AuthenticationFailedHandler(string message);

        private readonly ConfigurationService _configurationService;

        private readonly ITwitchService _twitchService;

        public AuthenticationController(ConfigurationService configurationService, ITwitchService twitchService)
        {
            _configurationService = configurationService;
            _twitchService = twitchService;
        }

        public void CheckAuthentication()
        {
            try
            {
                _twitchService.AccessToken = _configurationService.OauthAccessToken;
            }
            catch (Exception e) when (e is InvalidCredentialException || e is AggregateException)
            {
                if (e is AggregateException exception && !exception.InnerExceptions.OfType<BadScopeException>().Any())
                {
                    throw;
                }

                AuthenticationFailed?.Invoke(e.Message);
            }
        }

        public string GetTokenFromCallback(string url)
        {
            if (!url.StartsWith(ConfigurationService.CallbackUrl))
            {
                throw new UriFormatException("Callback url is invalid: " + url);
            }

            var chunks = url.Split(new char[] {'#'}, 2);

            if (chunks.Length < 2)
            {
                throw new UriFormatException("Callback url is invalid (hash part expected): " + url);
            }

            var queryParams = HttpUtility.ParseQueryString(chunks[1]);

            return queryParams.Get("access_token");
        }

        public void SetToken(string token)
        {
            _configurationService.OauthAccessToken = token;
        }
    }
}