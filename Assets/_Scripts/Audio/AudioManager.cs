namespace Audio
{
    public class AudioManager
    {
        private SoundFXAudioSystem _sfx;
        public SoundFXAudioSystem SFX => _sfx ??= new SoundFXAudioSystem(VolumeData);

        private BGMusic_AudioSystem _bgm;
        public BGMusic_AudioSystem BGMusic => _bgm ??= new BGMusic_AudioSystem(VolumeData);

        private KeyBoardAudioSystem _kba;
        public KeyBoardAudioSystem KBAudio => _kba ??= new KeyBoardAudioSystem(VolumeData);

        private static VolumeData VolumeData => DataManager.Io.Volume;

        private AudioParser _audioParser = new();
        public AudioParser AudioParser => _audioParser ??= new();

        #region INSTANCE

        private AudioManager() { }

        public static AudioManager Io => Instance.Io;

        private static class Instance
        {
            private static AudioManager _io;
            internal static AudioManager Io => _io ??= new AudioManager();
        }

        #endregion INSTANCE
    }
}
