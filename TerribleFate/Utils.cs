using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TerribleFate
{
    public class Utils
    {
        public static SoundPlayer GetSoundPlayer(string sound)
        {
            if (File.Exists(sound)) //is a physical file
            {
                return new SoundPlayer(sound);
            }

            try
            {
                return new SoundPlayer(System.Reflection.Assembly.GetAssembly(typeof(Countdown)).GetManifestResourceStream("TerribleFate." + sound.Replace("/", ".")));
            }
            catch
            {

            }


            return null;
        }

        public static IEnumerable<string> GetBuiltInSounds()
        {
            return new string[] { "click.wav", "collected.wav", "ding.wav", "wave.wav" };
        }
    }
}
