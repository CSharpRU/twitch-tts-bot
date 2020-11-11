using System;
using System.Windows.Forms;
using CefSharp;
using NLog;
using RDPG_Twitch_TTS_Bot.Controllers;
using RDPG_Twitch_TTS_Bot.Services;

namespace RDPG_Twitch_TTS_Bot
{
    public partial class AuthenticateForm : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly AuthenticationController _authenticationController;

        private readonly ITwitchService _twitchService;

        public AuthenticateForm(AuthenticationController authenticationController, ITwitchService twitchService)
        {
            _authenticationController = authenticationController;
            _twitchService = twitchService;

            InitializeComponent();

            BindWebBrowserHandlers();
        }

        private void BindWebBrowserHandlers()
        {
            twitchWebBrowser.AddressChanged += (sender, args) =>
            {
                Logger.Debug($"Auth address changed: {args.Address}");

                urlTextBox.Invoke(new Action(() => urlTextBox.Text = args.Address));
            };

            twitchWebBrowser.LoadError += (sender, args) =>
            {
                Logger.Debug($"Auth load error: {args.FailedUrl}");

                urlTextBox.Invoke(new Action(() => urlTextBox.Text = args.FailedUrl));

                CheckUrl(args);

                twitchWebBrowser.Stop();
            };
        }

        private void CheckUrl(LoadErrorEventArgs args)
        {
            _authenticationController.SetToken(_authenticationController.GetTokenFromCallback(args.FailedUrl));

            DialogResult = DialogResult.OK;

            Invoke(new Action(Close));
        }

        private void AuthenticateForm_Load(object sender, EventArgs e)
        {
            twitchWebBrowser.Load(_twitchService.GetOauthUrl());
        }
    }
}