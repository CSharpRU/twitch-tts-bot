using System.Collections.ObjectModel;
using System.Speech.Synthesis;
using System.Linq;

namespace RDPG_Twitch_TTS_Bot.Services
{
    public class WindowsSpeechSynthesizerService : ISpeechSynthesizerService
    {
        private readonly SpeechSynthesizer _synth;

        private Prompt _lastPrompt;

        public WindowsSpeechSynthesizerService
        (
            ConfigurationService configurationService,
            SpeechSynthesizer synth = null
        )
        {
            _synth = synth ?? new SpeechSynthesizer();

            try
            {
                _synth.SelectVoice(configurationService.Voice);
            }
            catch
            {
                // ignore
            }

            _synth.SetOutputToDefaultAudioDevice();

            _synth.StateChanged += (sender, args) => SynthesizerStateChanged?.Invoke(args.State);
            _synth.SpeakStarted += (sender, args) => _lastPrompt = args.Prompt;
        }

        public int Volume
        {
            get => _synth.Volume;
            set => _synth.Volume = value >= 0 && value <= 100 ? value : 100;
        }

        public int Rate
        {
            get => _synth.Rate;
            set => _synth.Rate = value >= -10 && value <= 10 ? value : 0;
        }

        public string Voice
        {
            get => _synth.Voice.Name;
            set => _synth.SelectVoice(value);
        }

        public event ISpeechSynthesizerService.SynthesizerStateChangedHandler SynthesizerStateChanged;

        public void Speak(string text)
        {
            var voiceStarted = false;
            var promptBuilder = new PromptBuilder();

            if (text.StartsWith("[") && text.IndexOf("]") is var indexOfLastBrace && indexOfLastBrace > 0)
            {
                var voiceName = text.Substring(1, indexOfLastBrace - 1);

                text = text.Substring(indexOfLastBrace + 1);

                if (GetVoices().First(voice => voice.VoiceInfo.Name.ToLower().Contains(voiceName.ToLower())) is var voice && voice != null)
                {
                    promptBuilder.StartVoice(voice.VoiceInfo.Name);

                    voiceStarted = true;
                }
            }

            promptBuilder.AppendText(text);

            if (voiceStarted)
            {
                promptBuilder.EndVoice();
            }

            _synth.SpeakAsync(promptBuilder);
        }

        public ReadOnlyCollection<InstalledVoice> GetVoices()
        {
            return _synth.GetInstalledVoices();
        }

        public void Skip()
        {
            lock (_lastPrompt)
            {
                if (_lastPrompt == null) return;

                _synth.SpeakAsyncCancel(_lastPrompt);

                _lastPrompt = null;
            }
        }

        public void SkipAll()
        {
            _synth.SpeakAsyncCancelAll();

            _lastPrompt = null;
        }
    }
}