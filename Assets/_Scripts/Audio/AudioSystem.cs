using System.Collections;
using UnityEngine;

namespace Audio
{
    public abstract class AudioSystem
    {
        private readonly string _name;
        private readonly int _numOfAudioSources;
        private AudioSource[] _audioSources;
        private int _cuedAudioClip;
        private int _cuedAudioSource;
        private int _cuedStartTime;
        private float _currentVolumeLevel = .6f;
        private bool _loop;
        private bool _muted;
        private GameObject _parent;
        private float _volumeLevelSetting = .6f;
        protected double dspTime;
        protected double realTime;
        public bool Running;
        protected double NextEventTime { get; set; }

        private void Destruct()
        {
            _audioSources = null;
            AudioClipSettings = null;
            Object.DestroyImmediate(Parent);
        }

        public AudioSystem(int numOfAudioSources, string name)
        {
            _numOfAudioSources = numOfAudioSources;
            _name = name;
        }

        public virtual GameObject Parent
        {
            get
            {
                if (_parent != null) _parent.SetActive(true);
                return _parent != null ? _parent : _parent = new GameObject(_name);
            }
        }

        public virtual bool Muted
        {
            get => _muted;
            set
            {
                _muted = value;
                CurrentVolumeLevel = value ? 0 : VolumeLevelSetting;
            }
        }

        public virtual bool Loop
        {
            get => _loop;
            set
            {
                _loop = value;
                foreach (var a in AudioSources) a.loop = value;
            }
        }

        public virtual float VolumeLevelSetting
        {
            get => _volumeLevelSetting;
            set
            {
                _volumeLevelSetting = value;
                CurrentVolumeLevel = value;
            }
        }

        public float CurrentVolumeLevel
        {
            get => _currentVolumeLevel;
            set
            {
                _currentVolumeLevel = value;

                if (_audioSources == null) return;
                foreach (var a in AudioSources)
                    a.volume = value;
            }
        }

        public virtual AudioClipSettings AudioClipSettings { get; set; }

        public virtual AudioSource[] AudioSources
        {
            get
            {
                return _audioSources ??= SetUpASs();

                AudioSource[] SetUpASs()
                {
                    var audioSources = new AudioSource[_numOfAudioSources];
                    for (var i = 0; i < _numOfAudioSources; i++)
                    {
                        GameObject child = new(nameof(AudioSource) + i);
                        child.transform.SetParent(Parent.transform);

                        audioSources[i] = child.AddComponent<AudioSource>();
                        audioSources[i].loop = false;
                        audioSources[i].playOnAwake = false;
                        audioSources[i].volume = VolumeLevelSetting;
                    }

                    return audioSources;
                }
            }
        }

        public virtual void ResetCues()
        {
            NextEventTime = AudioSettings.dspTime + .5D;
            _cuedAudioSource = 0;
            _cuedAudioClip = 0;
        }

        public virtual void Play(bool isSerial)
        {
            _ = Parent;
            CurrentVolumeLevel = 0f;
            ResetCues();
            Running = true;
            if (isSerial)
            {
                SerialAudioClipsUpdateLoop().StartCoroutine();
                return;
            }

            foreach (var a in AudioSources) a.Play();
            PlayAndFadeIn().StartCoroutine();

            IEnumerator PlayAndFadeIn()
            {
                yield return null;

                if (CurrentVolumeLevel < VolumeLevelSetting)
                {
                    CurrentVolumeLevel += Time.deltaTime * 1.75f;
                    PlayAndFadeIn().StartCoroutine();
                }
                else
                {
                    CurrentVolumeLevel = VolumeLevelSetting;
                }
            }
        }

        public virtual void Stop()
        {
            FadeOutAndStop().StartCoroutine();

            IEnumerator FadeOutAndStop()
            {
                while (CurrentVolumeLevel > .2f)
                {
                    yield return null;
                    CurrentVolumeLevel -= Time.deltaTime * 5f;
                }

                CurrentVolumeLevel = 0;
                foreach (var a in AudioSources) a.Stop();
                Running = false;
                Destruct();
            }
        }

        private IEnumerator SerialAudioClipsUpdateLoop()
        {
            while (Running)
            {
                var time = AudioSettings.dspTime;
                if (time + 1.00D > NextEventTime)
                {
                    AudioSources[_cuedAudioSource].clip = AudioClipSettings?.AudioClips[_cuedAudioClip];
                    AudioSources[_cuedAudioSource].time = (float)AudioClipSettings?.StartTimes[_cuedStartTime] *
                                                          (float)AudioClipSettings?.AudioClips[0].length;
                    AudioSources[_cuedAudioSource].PlayScheduled(NextEventTime);

                    NextEventTime += 60.00D / (double)AudioClipSettings?.BPM *
                                     (double)AudioClipSettings?.BeatsPerAudioClip;
                    AudioSources[_cuedAudioSource].SetScheduledEndTime(NextEventTime);

                    if (++_cuedAudioSource == AudioSources.Length) _cuedAudioSource = 0;
                    if (++_cuedAudioClip == AudioClipSettings?.AudioClips.Length) _cuedAudioClip = 0;
                    if (++_cuedStartTime == AudioClipSettings?.StartTimes.Length) _cuedStartTime = 0;
                }

                yield return null;
            }
        }

    }


    public class AudioClipSettings
    {
        public AudioClip[] AudioClips;
        public float BeatsPerAudioClip;
        public float BPM;
        public float[] StartTimes;
    }
}