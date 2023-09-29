using MusicTheory;
using UnityEngine;
using System.Collections;
using MusicTheory.Keys;

public class AudioParser
{
    public AudioParser(Key key) => GetAC(key).StartCoroutine();
    // ParseAC(genre, (int)bpm, cq, key, axe).StartCoroutine();
    public AudioClip AC { get; private set; }

    IEnumerator GetAC(Key key)
    {
        var ac = Resources.Load<AudioClip>("");//TODO

        while (ac.loadState != AudioDataLoadState.Loaded)
            yield return null;

        AC = ac;
    }

    IEnumerator ParseAC(Key key)
    {
        float tempo = 90;

        var ac = Resources.Load<AudioClip>("");//TODO
        while (ac.loadState != AudioDataLoadState.Loaded)
        {
            yield return null;
        }

        float[] samples = new float[ac.samples];
        float chordNibStart = (4f / 12f) + (key.Id / 12f);
        Debug.Log(chordNibStart + ", " + tempo + " " + key.Name);
        int offset = Mathf.CeilToInt((float)((ac.samples * ac.channels) * chordNibStart));

        AudioClip newAC = AudioClip.Create(
            key.Name,
            (int)(ac.frequency * (60f / tempo * 4f)),
            ac.channels,
            ac.frequency,
            false);

        float[] newSamples = new float[newAC.samples * newAC.channels];

        ac.GetData(samples, 0);

        for (int i = 0; i < newSamples.Length; i++)
        {
            //https://docs.unity3d.com/Manual/webgl-audio.html#audioclip
            //On WebGL, Unity ignores the offsetSample parameter.
            newSamples[i] = samples[i + offset];
        }

        newAC.SetData(newSamples, 0);

        AC = newAC;
    }
}