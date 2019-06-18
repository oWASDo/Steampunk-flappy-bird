using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Audio;

namespace FlappyBird
{

    static class Audio
    {
        private static Dictionary<string, AudioClip> dictionary;

        static Audio()
        {
            dictionary = new Dictionary<string, AudioClip>();
            dictionary.Add("BackGround", new AudioClip("Assets/Run.wav"));
            dictionary.Add("Flap", new AudioClip("Assets/flap.wav"));
            dictionary.Add("Dead", new AudioClip("Assets/dead.wav"));
        }

        //RETURN AN AUDIO
        //Return an audio from audio dictionary
        public static AudioClip GetAudio(string AudioName)
        {
            return dictionary[AudioName];
        }
    }
}
