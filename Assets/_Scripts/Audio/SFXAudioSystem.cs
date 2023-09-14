using Audio;
using UnityEngine;

public sealed class SoundFXAudioSystem : AudioSystem
{
    public SoundFXAudioSystem(VolumeData volumeData) : base(2, nameof(SoundFXAudioSystem))
    {
        AudioSources[0].volume = volumeData.GetScaledLevel(VolumeData.DataItem.SoundFX);
        AudioSources[1].volume = volumeData.GetScaledLevel(VolumeData.DataItem.SoundFX);
    }

    public void PlayOneShot(AudioClip ac)
    {
        AudioSources[0].clip = ac;
        AudioSources[0].Play();
    }

    public void PlayClip(AudioClip ac)
    {
        AudioSources[1].clip = ac;
        AudioSources[1].Play();
        AudioSources[1].loop = true;
    }

    public void StopClip()
    {
        AudioSources[1].Stop();
    }
}