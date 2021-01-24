namespace PointOfSale.Presentation.Helpers
{
    public enum Sounds
    {
        Startup, Shutdown, Error, Confirm
    }
    public static class SoundHelper
    {
        private static readonly System.Media.SoundPlayer _player = new System.Media.SoundPlayer();

        public static void Play(Sounds sound)
        {
            var soundLocation = "..\\..\\..\\Media\\Sounds\\";
            soundLocation += sound + ".wav";

            _player.SoundLocation = soundLocation;

            _player.Play();
        }
    }
}
