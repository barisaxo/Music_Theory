
using UnityEngine;
using MusicTheory.Keys;
using MusicTheory.Scales;
using MusicTheory.ScaleDegrees;
using MusicTheory.RomanNumerals;
using MusicTheory.Modes;
using MusicTheory.Arithmetic;

namespace MusicTheory
{
    public class DiatonicKeyCircle : Circle
    {
        public DiatonicKeyCircle(string name, float radius, Vector2 pos, Key[] keys,
            Scale scale, Mode mode, ScaleDegree scaleDegree, CircleType type) : base(name, radius, pos)
        {
            Type = type;
            Keys = keys;
            Scale = scale;
            Mode = mode;
            ScaleDegree = scaleDegree;
            PointNames = GetPointNames();
            this.DrawCircle();
        }

        public enum CircleType { Seconds, Thirds, Fifths, }
        public readonly CircleType Type;
        public readonly Key[] Keys;
        public Scale Scale { get; set; }
        public Mode Mode { get; set; }
        public ScaleDegree ScaleDegree { get; set; }
        public RomanNumeral RomanNumeral { get => ScaleDegree.ToRoman(); }

        private string[] GetPointNames()
        {
            string[] temp = new string[Keys.Length];
            int modeOffset = Scale.GetIndex(Mode);
            int scaleDegreeOffset = 0;
            for (int i = 0; i < Scale.ScaleDegrees.Length; i++)
                if (ScaleDegree.Equals(Scale.ScaleDegrees[i])) { scaleDegreeOffset = Scale.ScaleDegrees.Length - i; break; }

            for (int i = 0; i < temp.Length; i++)
            {
                Triads.Triad triad = Scale.ScaleDegrees[(i + modeOffset) % Scale.ScaleDegrees.Length].GetTriadQuality(
                   Scale.ScaleDegrees[(i + 2 + modeOffset) % Scale.ScaleDegrees.Length],
                   Scale.ScaleDegrees[(i + 4 + modeOffset) % Scale.ScaleDegrees.Length]);

                temp[(i + scaleDegreeOffset) % Scale.ScaleDegrees.Length] =
                    Keys[0].GetInterval(Keys[i]).ToRoman().Name + triad.Name + "\n" +
                    Keys[i].Name + triad.Name;
            }

            return temp;
        }

        public ScaleDegree GetClickedScaleDegree(GameObject go)
        {
            for (int i = 0; i < PointCards.Length; i++)
                if (go.transform.IsChildOf(PointCards[i].TMP.gameObject.transform))
                    return Scale.ScaleDegrees[(i + Scale.GetIndex(ScaleDegree)) % Scale.ScaleDegrees.Length];

            return ScaleDegree;
        }
    }
}