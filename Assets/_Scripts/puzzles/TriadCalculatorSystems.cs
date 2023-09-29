using MusicTheory;
using UnityEngine;
using MusicTheory.Arithmetic;

public static class TriadCalculatorSystems
{
    public static void SetChordTones(this TriadCalculator t)
    {
        int scaleDegreeIndex = t.Scale.GetIndex(t.ScaleDegree);

        t.RootCard.SetTextString("<size=60%><font-weight=\"100\">" + "Root:" + "</font-weight><size=100%>" + "   " + t.Keys[scaleDegreeIndex].Name);

        t.ThirdCard.SetTextString("<size=60%><font-weight=\"100\">" + "Third:" + "</font-weight><size=100%>" + "   " + t.Keys[(scaleDegreeIndex + 2) % t.Keys.Length].Name);

        t.FifthCard.SetTextString("<size=60%><font-weight=\"100\">" + "Fifth:" + "</font-weight><size=100%>" + "   " + t.Keys[(scaleDegreeIndex + 4) % t.Keys.Length].Name);

        t.ChordCard.CurrentRoot = t.Keys[scaleDegreeIndex];
        t.ChordCard.Triad = t.Keys[scaleDegreeIndex].GetTriadQuality(
                    t.Keys[(scaleDegreeIndex + 2) % t.Keys.Length],
                    t.Keys[(scaleDegreeIndex + 4) % t.Keys.Length]);
    }

    public static ChromaticKeyCircle NewChromaticKeyCircleOfFifths(this TriadCalculator t)
    {
        t.CircleOfFifths?.SelfDestruct();

        return new ChromaticKeyCircle("Key Circle Of Fifths",
            2.25f,
            new Vector2(-Cam.UIOrthoX + 2.15f, 0),
            t.Key,
            ChromaticKeyCircle.CircleType.Fifths);
    }

    public static DiatonicKeyCircle NewDiatonicKeyCircle(this TriadCalculator t)
    {
        t.DiatonicKeyCircle?.SelfDestruct();

        return new("Nominal Circle of Steps",
             2.25f,
             new Vector2(Cam.UIOrthoX - 2.15f, 1f),
             t.Keys,
             t.Scale,
             t.Mode,
             t.ScaleDegree,
             DiatonicKeyCircle.CircleType.Seconds);
    }

    public static void PlayTriad(this TriadCalculator t)
    {
        MusicTheory.Keys.Key root = t.Keys[(t.Scale.GetIndex(t.ScaleDegree) + 0) % t.Scale.ScaleDegrees.Length];
        MusicTheory.Keys.Key third = t.Keys[(t.Scale.GetIndex(t.ScaleDegree) + 2) % t.Scale.ScaleDegrees.Length];
        MusicTheory.Keys.Key fifth = t.Keys[(t.Scale.GetIndex(t.ScaleDegree) + 4) % t.Scale.ScaleDegrees.Length];

        float rootFreq = root.GetHertz();
        float thirdFreq = third.Id < root.Id ? third.GetHertz() * 2 : third.GetHertz();
        float fifthFreq = fifth.Id < third.Id || fifth.Id < root.Id ? fifth.GetHertz() * 2 : fifth.GetHertz();

        AudioClip newAC = Audio.WaveGenerator.CreateTriadSineWave(rootFreq, thirdFreq, fifthFreq);
        t.TriadAudio.PlayOneShot(newAC);
    }
}