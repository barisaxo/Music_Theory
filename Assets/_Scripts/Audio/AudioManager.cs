namespace Audio
{
    public class AudioManager
    {
        private BGMusic_AudioSystem _bgMusic;
        private SoundFXAudioSystem _sfx;
        public SoundFXAudioSystem SFX => _sfx ??= new SoundFXAudioSystem(VolumeData);
        public BGMusic_AudioSystem BGMusic => _bgMusic ??= new BGMusic_AudioSystem(VolumeData);

        private static VolumeData VolumeData => DataManager.Io.Volume;

        #region INSTANCE

        private AudioManager()
        {
        }

        public static AudioManager Io => Instance.Io;

        private static class Instance
        {
            private static AudioManager _io;
            internal static AudioManager Io => _io ??= new AudioManager();
        }

        #endregion INSTANCE
    }
}
