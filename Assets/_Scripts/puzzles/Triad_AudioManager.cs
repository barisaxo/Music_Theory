using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;


public class Triad_AudioSystem : AudioSystem
{

    public Triad_AudioSystem(VolumeData data) : base(numOfAudioSources: 1, nameof(Triad_AudioSystem))
    {

    }

    public void PlayOneShot(AudioClip ac)
    {
        AudioSources[0].Stop();
        AudioSources[0].PlayOneShot(ac);
    }

    //public void AssignRoot(AudioClip root)
    //{
    //    AudioSources[0].clip = root;
    //}
    //public void AssignThird(AudioClip third)
    //{
    //    AudioSources[1].clip = third;
    //}
    //public void AssignFifth(AudioClip fifth)
    //{
    //    AudioSources[2].clip = fifth;
    //}


}