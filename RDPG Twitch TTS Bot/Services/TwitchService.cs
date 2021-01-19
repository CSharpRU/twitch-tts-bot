using System;
using NLog;
using RDPG_Twitch_TTS_Bot.Properties;
using TwitchLib.Api;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Interfaces;
using TwitchLib.Client;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Interfaces;

namespace RDPG_Twitch_TTS_Bot.Services
{
    public class TwitchService : ITwitchService
    {
        public event ITwitchService.RedeemReceivedHandler RedeemReceived;

        public event ITwitchService.MessageReceivedHandler MessageReceived;

        private readonly ConfigurationService _configurationService;

        private readonly ISpeechSynthesizerService _speechSynthesizerService;

        private readonly ITwitchAPI _twitchApi;

        private readonly ITwitchPubSub _twitchPubSub;

        private readonly ITwitchClient _twitchClient;

        private string _accessToken;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public TwitchService
        (
            string clientId,
            ConfigurationService configurationService,
            ISpeechSynthesizerService speechSynthesizerService,
            ITwitchAPI api = null,
            ITwitchPubSub pubSub = null,
            ITwitchClient client = null
        )
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentException(Resources.Argument_cannot_be_null_or_whitespace_, nameof(clientId));
            }

            _configurationService = configurationService;
            _speechSynthesizerService = speechSynthesizerService;
            _twitchApi = api ?? new TwitchAPI();
            _twitchPubSub = pubSub ?? new TwitchPubSub();

            _twitchClient = client ?? new TwitchClient();

            ClientId = clientId;
        }

        public string AccessToken
        {
            get => _accessToken;
            set
            {
                CheckAndSetToken(value);

                SetUserInfo();
                InitializePubSub();
                BindPubSubEventHandlers();
                BindClientEventHandlers();
                Connect();
            }
        }

        private void BindClientEventHandlers()
        {
            _twitchClient.OnMessageReceived += (sender, args) =>
            {
                Logger.Debug($"New message: {args.ChatMessage}");

                MessageReceived?.Invoke(args);
            };

            _twitchClient.OnJoinedChannel += (sender, args) =>
            {
                Logger.Debug($"Joined channel: {args.Channel}");
            };
        }

        private void Connect()
        {
            _twitchPubSub.Connect();

            _twitchClient.Initialize(new ConnectionCredentials(_configurationService.Username, _accessToken), _configurationService.Channel);
            _twitchClient.Connect();
        }

        private void CheckAndSetToken(string value)
        {
            _twitchApi.Settings.AccessToken = value;

            var check = _twitchApi.V5.Auth.CheckCredentialsAsync().Result;

            if (!check.Result)
            {
                _twitchApi.Settings.AccessToken = null;

                throw new InvalidCredentialException(check.ResultMessage);
            }

            _accessToken = value;
        }

        private void BindPubSubEventHandlers()
        {
            _twitchPubSub.OnPubSubServiceConnected += (sender, args) =>
            {
                Logger.Debug($"PubSub connected!");

                _twitchPubSub.SendTopics();
            };

            _twitchPubSub.OnRewardRedeemed += (sender, args) =>
            {
                Logger.Debug($"New redeem: {args.Login}: {args.Message} ({args.RewardTitle})");

                RedeemReceived?.Invoke(args);

                if (args.RewardTitle == _configurationService.RewardName)
                {
                    _speechSynthesizerService.Speak(args.Message);
                }
            };
        }

        private void InitializePubSub()
        {
            _twitchPubSub.ListenToRewards(_twitchApi.V5.Channels.GetChannelAsync().Result.Id);
        }

        private void SetUserInfo()
        {
            var user = _twitchApi.V5.Users.GetUserAsync().Result;

            _configurationService.Username = _configurationService.Channel = user.Name;
        }

        public string ClientId
        {
            get => _twitchApi.Settings.ClientId;
            set => _twitchApi.Settings.ClientId = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetOauthUrl()
        {
            return
                $"https://id.twitch.tv/oauth2/authorize?client_id={ClientId}&redirect_uri={ConfigurationService.CallbackUrl}&response_type=token&scope=user:read:email+chat:read+chat:edit+user_read+channel_read";
        }

        public void SendMessage(string message, string channel = null)
        {
            _twitchClient.SendMessage(channel ?? _configurationService.Channel, message);
        }
    }
}