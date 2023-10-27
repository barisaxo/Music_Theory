using Audio;
using UnityEngine;

public sealed class KeyBoardAudioSystem : AudioSystem
{
    public KeyBoardAudioSystem(VolumeData volumeData) : base(3, nameof(KeyBoardAudioSystem))
    {
        foreach (var a in AudioSources)
            a.volume = volumeData.GetScaledLevel(VolumeData.DataItem.SoundFX);
    }

    public void PlayNote(AudioClip ac)
    {
        AudioSources[0].clip = ac;
        AudioSources[0].Play();
    }

    public void PlayInterval(AudioClip ac1, AudioClip ac2)
    {
        AudioSources[0].clip = ac1;
        AudioSources[0].Play();
        AudioSources[1].clip = ac2;
        AudioSources[1].Play();
    }

    public void PlayChord(AudioClip ac1, AudioClip ac2, AudioClip ac3)
    {
        AudioSources[0].clip = ac1;
        AudioSources[0].Play();
        AudioSources[1].clip = ac2;
        AudioSources[1].Play();
        AudioSources[2].clip = ac3;
        AudioSources[2].Play();
    }

    public override void Stop()
    {
        foreach (var a in AudioSources) a.Stop();
    }
}