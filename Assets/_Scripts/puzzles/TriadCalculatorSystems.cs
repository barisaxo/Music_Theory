using MusicTheory;
using MusicTheory.Keys;
using MusicTheory.Scales;
using UnityEngine;

public static class TriadCalculatorSystems
{
    public static void SetChordTones(this TriadCalculator t)
    {
        t.Root.SetTextString(nameof(t.Root) + ": " +
             t.RomanCircle.Scale.Root(t.RomanCircle.CurrentScaleDegree, t.CircleOfFifths.CurrentKey)
            .EnharmonicNoteName(t.CircleOfFifths.CurrentKey));

        t.Third.SetTextString(nameof(t.Third) + ": " +
            t.RomanCircle.Scale.Third(t.RomanCircle.CurrentScaleDegree, t.CircleOfFifths.CurrentKey)
           .EnharmonicNoteName(t.CircleOfFifths.CurrentKey));

        t.Fifth.SetTextString(nameof(t.Fifth) + ": " +
            t.RomanCircle.Scale.Fifth(t.RomanCircle.CurrentScaleDegree, t.CircleOfFifths.CurrentKey)
           .EnharmonicNoteName(t.CircleOfFifths.CurrentKey));
    }

    public static void SetScale(this TriadCalculator t, Scale scale)
    {
        t.Scale = scale;
    }

    public static RomanCircle NewDiatonicRomanCircle(this TriadCalculator t)
    {
        t.RomanCircle?.SelfDestruct();

        return new RomanCircle("Roman Circle of Steps",
             1.25f,
             new Vector2(-Cam.UIOrthoX + 2.15f, -Cam.UIOrthoY + 1.35f),
             t.Scale,
             ModeDegree.Prime,
             0,
             RomanCircle.CircleType.Seconds);
    }


}