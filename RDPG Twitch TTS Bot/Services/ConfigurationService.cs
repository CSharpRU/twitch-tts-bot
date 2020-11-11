using System;
using System.Configuration;
using System.Windows.Forms;
using RDPG_Twitch_TTS_Bot.Properties;

namespace RDPG_Twitch_TTS_Bot.Services
{
    public class ConfigurationService
    {
        public const string CallbackUrl = "http://localhost/oauth/callback";

        private const string UsernameKey = "username";
        private const string ChannelKey = "channel";
        private const string OauthAccessTokenKey = "oauthAccessToken";
        private const string VoiceKey = "voice";
        private const string RewardNameKey = "rewardName";

        private readonly Configuration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ConfigurationService(Configuration configuration = null)
        {
            _configuration = configuration ?? ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
        }

        public string Username
        {
            get => _configuration.AppSettings.Settings[UsernameKey]?.Value;
            set => Set(UsernameKey, value);
        }

        public string Channel
        {
            get => _configuration.AppSettings.Settings[ChannelKey]?.Value;
            set => Set(ChannelKey, value);
        }

        public string OauthAccessToken
        {
            get => _configuration.AppSettings.Settings[OauthAccessTokenKey]?.Value ?? "bad";
            set => Set(OauthAccessTokenKey, value);
        }

        public string Voice
        {
            get => _configuration.AppSettings.Settings[VoiceKey]?.Value;
            set => Set(VoiceKey, value);
        }

        public string RewardName
        {
            get => _configuration.AppSettings.Settings[RewardNameKey]?.Value;
            set => Set(RewardNameKey, value);
        }

        /// <summary>
        /// Saves configuration with selected mode.
        /// </summary>
        /// <seealso cref="ConfigurationSaveMode"/>
        /// <param name="mode"></param>
        public void Save(ConfigurationSaveMode mode = ConfigurationSaveMode.Full)
        {
            _configuration.Save(mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void Set(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(Resources.Argument_cannot_be_null_or_whitespace_,
                    nameof(key));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(Resources.Argument_cannot_be_null_or_whitespace_,
                    nameof(value));
            }

            _configuration.AppSettings.Settings.Remove(key);
            _configuration.AppSettings.Settings.Add(key, value);

            Save();
        }
    }
}