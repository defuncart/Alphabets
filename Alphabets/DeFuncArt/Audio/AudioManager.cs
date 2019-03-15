using DeFuncArt.Helpers;
using Plugin.SimpleAudioPlayer;
using System.IO;

namespace DeFuncArt.Audio
{
    /// <summary>
    /// An audio manager which acts as a wrapper for the simple audio player plugin.
    /// Only supports one voice of polyphony.
    /// </summary>
    public static class AudioManager
    {
        /// <summary>A resource prefix for where all audio files are located.</summary>
        private const string RESOURCE_PREFIX = "Resources.Audio";

        /// <summary>The default file format.</summary>
        private const string DEFAULT_FILE_FORMAT = "wav";

        /// <summary>The simple audio player.</summary>
        private static readonly ISimpleAudioPlayer player;

        /// <summary>
        /// Whether the manager is currently playing an audiofile.
        /// </summary>
        public static bool IsPlaying => player.IsPlaying;

        /// <summary>
        /// Initializes the <see cref="T:DeFuncArt.Audio.AudioManager"/> class.
        /// </summary>
        static AudioManager()
        {
            player = CrossSimpleAudioPlayer.Current;
        }

        /// <summary>
        /// Play a specified filename with a given file format. Defaults to WAV.
        /// </summary>
        /// <param name="filename">The filename (i.e. SFX.answerCorrect).</param>
        /// <param name="fileFormat">The file format.</param>
        public static void Play(string filename, string fileFormat = DEFAULT_FILE_FORMAT, float volume = 1)
        {
            //determine resource id
            string resourceId = $"{FileHelper.AssemblyName}.{RESOURCE_PREFIX}.{filename}.{fileFormat}";

            //get the stream
            Stream stream = FileHelper.GetStream(resourceId);

            //load the stream
            player.Load(stream);

            //set volume
            if(volume >= 0 && volume <= 1)
            {
                player.Volume = volume;
            }

            //play
            player.Play();
        }
    }
}
