using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSale.Presentation.Helpers
{
    public enum Sounds
    {
        Startup, Shutdown, Error, Confirm
    }
    public static class SoundHelper
    {
        private static System.Media.SoundPlayer _player = new System.Media.SoundPlayer();

        public static void Play(Sounds sound)
        {
            string soundLocation = "..\\..\\..\\Media\\Sounds\\";
            soundLocation += sound + ".wav";

            _player.SoundLocation = soundLocation;

            _player.Play();
        }
    }
}
