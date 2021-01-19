using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows.Forms;
using RDPG_Twitch_TTS_Bot.Controllers;
using RDPG_Twitch_TTS_Bot.Properties;
using RDPG_Twitch_TTS_Bot.Services;

namespace RDPG_Twitch_TTS_Bot
{
    public partial class MainForm : Form
    {
        private readonly AuthenticationController _authenticationController;

        private readonly ITwitchService _twitchService;

        private readonly ISpeechSynthesizerService _speechSynthesizerService;

        private readonly ConfigurationService _configurationService;

        private readonly Timer speakersCommandTimer = new Timer();

        public MainForm
        (
            AuthenticationController authenticationController,
            ITwitchService twitchService,
            ISpeechSynthesizerService windowsSpeechSynthesizerService,
            ConfigurationService configurationService
        )
        {
            _authenticationController = authenticationController;
            _twitchService = twitchService;
            _speechSynthesizerService = windowsSpeechSynthesizerService;
            _configurationService = configurationService;

            if (_speechSynthesizerService.GetVoices().Count == 0)
            {
                MessageBox.Show(
                    Resources.MainForm_MainForm_No_voice_packs_available__Please_install_Windows_Speech_Pack_and_retry_,
                    Resources.MainForm_MainForm_Error,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                Environment.Exit(1);

                return;
            }

            InitializeComponent();

            BindEventHandlers();

            authenticationController.CheckAuthentication();

            FillVoices();

            speakersCommandTimer.Interval = _configurationService.SpeakersCommandTimeout;
        }

        private void FillVoices()
        {
            var dictionary = _speechSynthesizerService
                .GetVoices()
                .Select(voice => voice.VoiceInfo)
                .ToDictionary(info => $"{info.Name} / {info.Age} / {info.Gender}", info => info.Name);

            voiceComboBox.DataSource = new BindingSource(dictionary, null);
            voiceComboBox.DisplayMember = "Key";
            voiceComboBox.ValueMember = "Value";

            voiceComboBox.SelectedItem = dictionary.First(item => item.Value == _speechSynthesizerService.Voice);
        }

        private void BindEventHandlers()
        {
            _authenticationController.AuthenticationFailed += message => ShowAuthenticateForm();

            _speechSynthesizerService.SynthesizerStateChanged +=
                state => voiceComboBox.Invoke(
                    new Action(
                        () => voiceComboBox.Enabled = state == SynthesizerState.Ready
                    )
                );

            _twitchService.RedeemReceived += args => ttsHistoryListBox.Invoke(
                new Action(
                    () => ttsHistoryListBox.Items.Add($"New redeem ({args.RewardTitle}): {args.Login}: {args.Message}")
                )
            );

            // todo: move to command handlers classes
            _twitchService.MessageReceived += args =>
            {
                if (!speakersCommandTimer.Enabled && args.ChatMessage.Message == _configurationService.SpeakersCommand)
                {
                    string message = $"Available speakers: {String.Join(" | ", _speechSynthesizerService.GetVoices().Select((voice) => voice.VoiceInfo.Name).ToArray())} (use name as [speaker name] before your TTS message, e.g. [speaker]My TTS message!)";

                    _twitchService.SendMessage(message, args.ChatMessage.Channel);

                    speakersCommandTimer.Start();
                }
            };
        }

        private void ShowAuthenticateForm()
        {
            var result = new AuthenticateForm(_authenticationController, _twitchService).ShowDialog(this);

            if (result == DialogResult.OK)
            {
                return;
            }

            MessageBox.Show(
                Resources
                    .MainForm_ShowAuthenticateForm_Authentication_is_failed__please_restart_application_and_try_again_,
                Resources.MainForm_MainForm_Error,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            Environment.Exit(1);
        }

        private void volumeTrackBar_Scroll(object sender, EventArgs e)
        {
            _speechSynthesizerService.Volume = volumeTrackBar.Value;
        }

        private void testTtsButton_Click(object sender, EventArgs e)
        {
            const string testMessage = "this is a test tts message";

            _speechSynthesizerService.Speak(testMessage);

            ttsHistoryListBox.Items.Add(testMessage);
        }

        private void skipButton_Click(object sender, EventArgs e)
        {
            _speechSynthesizerService.Skip();
        }

        private void skipAllButton_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(
                    Resources.MainForm_skipAllButton_Click_Are_you_sure_,
                    Resources.MainForm_skipAllButton_Click_Confirmation,
                    MessageBoxButtons.YesNo
                ) == DialogResult.Yes
            )
            {
                _speechSynthesizerService.SkipAll();
            }
        }

        private void voiceComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var voice = ((KeyValuePair<string, string>)voiceComboBox.SelectedItem).Value;

            _speechSynthesizerService.Voice = _configurationService.Voice = voice;
        }
    }
}