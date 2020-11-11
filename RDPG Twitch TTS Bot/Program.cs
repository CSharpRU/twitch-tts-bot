using System;
using System.Configuration;
using System.Windows.Forms;
using RDPG_Twitch_TTS_Bot.Controllers;
using RDPG_Twitch_TTS_Bot.Services;

namespace RDPG_Twitch_TTS_Bot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var configurationService = new ConfigurationService();
            var windowsSpeechSynthesizerService = new WindowsSpeechSynthesizerService(configurationService);
            var twitchService = new TwitchService(ConfigurationManager.AppSettings.Get("clientId"),
                configurationService, windowsSpeechSynthesizerService);

            Application.Run(
                new MainForm(
                    new AuthenticationController(
                        configurationService,
                        twitchService
                    ),
                    twitchService,
                    windowsSpeechSynthesizerService,
                    configurationService
                )
            );
        }
    }
}