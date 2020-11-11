using TwitchLib.PubSub.Events;

namespace RDPG_Twitch_TTS_Bot.Services
{
    public interface ITwitchService
    {
        public event RedeemReceivedHandler RedeemReceived;

        public delegate void RedeemReceivedHandler(OnRewardRedeemedArgs message);

        /// <summary>
        /// 
        /// </summary>
        string AccessToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ClientId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetOauthUrl();
    }
}