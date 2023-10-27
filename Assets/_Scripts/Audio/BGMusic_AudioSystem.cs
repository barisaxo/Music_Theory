using System.Collections;
using Audio;
using UnityEngine;

public sealed class BGMusic_AudioSystem : AudioSystem
{
    public BGMusic_AudioSystem(VolumeData data) : base(1, nameof(BGMusic_AudioSystem))
    {
        Loop = true;
        VolumeLevelSetting = data.GetScaledLevel(VolumeData.DataItem.BGMusic);
        foreach (var a in AudioSources) a.playOnAwake = true;

        //foreach (var a in AudioSources)
        //    a.clip = Random.Range(1, 5) switch
        //    {
        //        _ => throw new System.NotImplementedException()
        //    };
    }

    public void NextSong()
    {
        foreach (var a in AudioSources)
            a.clip = a.clip switch
            {
                _ => throw new System.NotImplementedException()
            };
    }

    public void Pause()
    {
        foreach (var a in AudioSources) a.Pause();
    }

    public void Resume()
    {
        CurrentVolumeLevel = CurrentVolumeLevel;
        foreach (var a in AudioSources)
            if (a.isPlaying)
                return;

        FadeInAndResume().StartCoroutine();

        IEnumerator FadeInAndResume()
        {
            yield return null;
            if (CurrentVolumeLevel < VolumeLevelSetting)
            {
                CurrentVolumeLevel += Time.deltaTime * 1.75f;
                FadeInAndResume().StartCoroutine();
            }
            else
            {
                CurrentVolumeLevel = VolumeLevelSetting;
                foreach (var a in AudioSources) a.UnPause();
            }
        }
    }
}