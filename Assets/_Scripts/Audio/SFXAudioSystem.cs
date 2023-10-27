using Audio;
using UnityEngine;
using System.Collections;

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

    //public void PlayOneShot(AudioClip ac, float startPos)
    //{
    //    if (AudioSources[0].isPlaying)
    //    { }
    //    AudioSources[0].clip = ac;
    //    AudioSources[0].time = startPos;

    //    Play().StartCoroutine();

    //    IEnumerator Play()
    //    {
    //        Debug.Log(AudioSources[0].clip);
    //        AudioSources[0].Play();
    //        yield return new WaitForSecondsRealtime(3.9f);
    //        AudioSources[0].Stop();
    //    }

    //}

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
