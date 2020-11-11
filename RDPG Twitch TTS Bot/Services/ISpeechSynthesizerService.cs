using System.Collections.ObjectModel;
using System.Speech.Synthesis;

namespace RDPG_Twitch_TTS_Bot.Services
{
    public interface ISpeechSynthesizerService
    {
        public int Volume { get; set; }

        public int Rate { get; set; }

        public string Voice { get; set; }

        public event SynthesizerStateChangedHandler SynthesizerStateChanged;

        public delegate void SynthesizerStateChangedHandler(SynthesizerState state);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void Speak(string text);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<InstalledVoice> GetVoices();

        /// <summary>
        /// 
        /// </summary>
        void Skip();

        /// <summary>
        /// 
        /// </summary>
        void SkipAll();
    }
}