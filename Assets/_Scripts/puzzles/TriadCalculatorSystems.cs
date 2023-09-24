using MusicTheory;
using MusicTheory.ScaleDegrees;
using MusicTheory.Scales;
using UnityEngine;
using MusicTheory.Modes;

public static class TriadCalculatorSystems
{
    public static void SetChordTones(this TriadCalculator t)
    {
        t.RootCard.SetTextString("<size=60%><font-weight=\"100\">" + "Root:" + "</font-weight><size=100%>" + "   " +
             t.Scale.Root(t.CurrentScaleDegree, t.CurrentKey));

        t.ThirdCard.SetTextString("<size=60%><font-weight=\"100\">" + "Third:" + "</font-weight><size=100%>" + "   " +
            t.Scale.Third(t.CurrentScaleDegree, t.CurrentKey));

        t.FifthCard.SetTextString("<size=60%><font-weight=\"100\">" + "Fifth:" + "</font-weight><size=100%>" + "   " +
            t.Scale.Fifth(t.CurrentScaleDegree, t.CurrentKey));

        int x = 0;

        for (int i = 0; i < t.Scale.ScaleDegrees.Length; i++)
            if (t.CurrentScaleDegree.Equals(t.Scale.ScaleDegrees[i])) { x = i; break; }

        t.ChordCard.Triad = t.CurrentScaleDegree.GetTriadQuality(
                    t.Scale.ScaleDegrees[(x + 2) % t.Scale.ScaleDegrees.Length],
                    t.Scale.ScaleDegrees[(x + 4) % t.Scale.ScaleDegrees.Length]);
    }

    public static void SetScale(this TriadCalculator t, Scale scale)
    {
        t.Scale = scale;
    }

    public static RomanCircle NewDiatonicRomanCircle(this TriadCalculator t)
    {
        t.RomanCircle?.SelfDestruct();
        t.CurrentScaleDegree = new _1();

        return new RomanCircle("Roman Circle of Steps",
             1.65f,
             new Vector2(-Cam.UIOrthoX + 2.15f, -Cam.UIOrthoY + 1.35f),
             t.Scale,
             ModeDegree.Prime,
             t.CurrentScaleDegree,
             RomanCircle.CircleType.Seconds);
    }

    public static DiatonicKeyCircle NewDiatonicKeyCircle(this TriadCalculator t)
    {
        t.DiatonicKeyCircle?.SelfDestruct();

        return new DiatonicKeyCircle(
            "Nominal Circle of Steps",
             1.25f,
             new Vector2(-Cam.UIOrthoX + 6.45f, -Cam.UIOrthoY + 1.35f),
             t.CurrentKey,
             t.Scale,
             t.CurrentScaleDegree,
             DiatonicKeyCircle.CircleType.Seconds);
    }
}