using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Scales;
using MusicTheory.ScaleDegrees;
using MusicTheory.RomanNumerals;
using MusicTheory.Modes;
using MusicTheory.Arithmetic;

namespace MusicTheory
{
    public class RomanCircle : Circle, IRoman<RomanCircle>
    {
        public RomanCircle(string name, float radius, Vector2 pos, Scale scale, Mode mode, ScaleDegree scaleDegree, CircleType type) : base(name, radius, pos)
        {
            Scale = scale;
            Mode = mode;
            CurrentScaleDegree = scaleDegree;
            Type = type;
            PointNames = GetPointNames();
            this.DrawCircle();
        }

        public CircleType Type;
        public Scale Scale { get; set; }
        public Mode Mode { get; set; }
        public ScaleDegree CurrentScaleDegree { get; set; }
        public RomanNumeral RomanNumeral { get => CurrentScaleDegree.ToRoman(); }

        public enum CircleType { Seconds, Thirds, Fifths, }

        private string[] GetPointNames()
        {
            int modeOffset = 0;
            for (int i = 0; i < Scale.Modes.Length; i++)
                if (Mode.Equals(Scale.Modes[i])) { modeOffset = i; break; }

            int scaleDegreeOffset = 0;
            for (int i = 0; i < Scale.ScaleDegrees.Length; i++)
                if (CurrentScaleDegree.Equals(Scale.ScaleDegrees[i])) { scaleDegreeOffset = i; break; }

            string[] temp = new string[Scale.Steps.Length];

            for (int i = 0; i < temp.Length; i++)
            {
                Triads.Triad triad = Scale.ScaleDegrees[(i + modeOffset + scaleDegreeOffset) % Scale.ScaleDegrees.Length].GetTriadQuality(
                    Scale.ScaleDegrees[(i + 2 + modeOffset + scaleDegreeOffset) % Scale.ScaleDegrees.Length],
                    Scale.ScaleDegrees[(i + 4 + modeOffset + scaleDegreeOffset) % Scale.ScaleDegrees.Length]);

                temp[i] = Scale.ScaleDegrees[(i + scaleDegreeOffset) % Scale.ScaleDegrees.Length].ToRoman().Name + triad.Name;
            }

            return temp;
        }

        public ScaleDegree ScrollRoman(int delta)
        {
            this.RotateCounterClockwise();

            int x = 0;

            for (int i = 0; i < Scale.ScaleDegrees.Length; i++)
                if (CurrentScaleDegree.Equals(Scale.ScaleDegrees[i])) { x = i; break; }

            return CurrentScaleDegree = Scale.ScaleDegrees[(x + delta) % Scale.ScaleDegrees.Length];
        }
    }


}