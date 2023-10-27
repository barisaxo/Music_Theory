using MusicTheory;
using UnityEngine;
using System.Collections;
using MusicTheory.Keys;
using Audio;

public class AudioParser
{
    //public AudioParser(Key key) => GetAC(key).StartCoroutine();
    // ParseAC(genre, (int)bpm, cq, key, axe).StartCoroutine();
    public AudioClip AC = Resources.Load<AudioClip>("Audio/2_octave_piano_whole_notes_60bpm 1");

    public AudioParser()
    {
        //AC = Resources.Load<AudioClip>("Audio/2_octave_piano_whole_notes_60bpm");
    }

    public AudioClip GetAudioClipFromKey(KeyboardNoteName key)
    {
        Debug.Log(key.ToString());
        float chordNibStart = 4f * (int)key;

        int offset = Mathf.CeilToInt((float)(AC.frequency * chordNibStart));
        Debug.Log(offset + ", Samples: " + AC.samples);
        float[] samples = new float[AC.samples];
        AC.GetData(samples, 0);


        //float samplepos = (float)((float)offset / (float)AC.samples);
        //float timepos = (float)((float)chordNibStart / (float)AC.length);

        //AudioConfiguration con = AudioSettings.GetConfiguration();
        ////AudioManager.Io.KBAudio.AudioSources[0].

        //Debugy.SetTextString("offset: " + offset + ", Samples: " + AC.samples +
        //    ", \ndsp: " + con.dspBufferSize + ", rate: " + con.sampleRate + ", mode: " + con.speakerMode +
        //    ", \nsample pos: " + samplepos + ", time pos: " + timepos +
        //    ", \nstart: " + chordNibStart + ", length:" + AC.length + ", note: " + key.ToString()
        //    + ", \npitch: " + AudioManager.Io.KBAudio.AudioSources[0].pitch
        //    + ", \nOS: " + SystemInfo.operatingSystem
        //    );

        AudioClip newAC = AudioClip.Create(
           name: key.ToString(),
           lengthSamples: (int)(AC.frequency * 4f),
           channels: AC.channels,
           frequency: AC.frequency,
           stream: false);


        float[] newSamples = new float[newAC.samples * newAC.channels];

        for (int i = 0; i < newSamples.Length; i++)
        {
            //https://docs.unity3d.com/Manual/webgl-audio.html#audioclip
            //On WebGL, Unity ignores the offsetSample parameter.
            //Debug.Log(i)
            newSamples[i] = samples[i + offset];
            //samples[i];//
        }

        newAC.SetData(newSamples, 0);

        return newAC;
    }

    //Card Debugy = new Card(nameof(Debugy), null)
    //   .SetTMPPosition(Vector2.down)
    //   .AllowWordWrap(false)
    //   .SetTextAlignment(TMPro.TextAlignmentOptions.Center);


    IEnumerator GetAC(Key key)
    {
        var ac = Resources.Load<AudioClip>("Audio/2_octave_piano_whole_notes_60bpm");

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