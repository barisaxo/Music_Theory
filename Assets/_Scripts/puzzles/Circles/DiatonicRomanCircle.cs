using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Scales;
using MusicTheory.ScaleDegrees;
using MusicTheory.RomanNumerals;
using MusicTheory.Modes;

namespace MusicTheory
{
    public class RomanCircle : Circle, IRoman<RomanCircle>
    {
        public RomanCircle(string name, float radius, Vector2 pos, Scale scale, ModeDegree mode, ScaleDegree scaleDegree, CircleType type) : base(name, radius, pos)
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
        public ModeDegree Mode { get; set; }
        public ScaleDegree CurrentScaleDegree { get; set; }
        public RomanNumeral RomanNumeral { get => CurrentScaleDegree.ToRoman(); }

        public enum CircleType { Seconds, Thirds, Fifths, }

        private string[] GetPointNames()
        {
            string[] temp = new string[Scale.Steps.Length];

            for (int i = 0; i < temp.Length; i++)
            {
                Triads.Triad triad = Scale.ScaleDegrees[i].GetTriadQuality(
                    Scale.ScaleDegrees[(i + 2) % Scale.ScaleDegrees.Length],
                    Scale.ScaleDegrees[(i + 4) % Scale.ScaleDegrees.Length]);

                temp[i] = Scale.ScaleDegrees[i].ToRoman().Name + triad.Name;
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

/*
 * 

        private void GetRomanPoints()
        {
            switch (Type)
            {
                case CircleType.Chromatic: Get12RomanPoints(); break;
                case CircleType.Fifths: Get12RomanPoints(); break;
                default: Get7RomanPoints(); break;
            }
        }

        private void Get7RomanPoints()
        {
            Points = new string[7];

            for (int i = 0; i < 7; i++)
            {
                Points[i] = Type == CircleType.Diatonic2_7 ?
                        MusicTheory.RomanNumeralToString(((DiatonicRomanNumeral)i).ToChromaticRoman()) + ((DiatonicRomanNumeral)i).TriadQuality() :
                        Type == CircleType.Diatonic3_6 ?
                        MusicTheory.RomanNumeralToString(((DiatonicRomanNumeral)(i * 2 % 7)).ToChromaticRoman()) + ((DiatonicRomanNumeral)i).TriadQuality() :
                        MusicTheory.RomanNumeralToString(((DiatonicRomanNumeral)(i * 4 % 7)).ToChromaticRoman()) + ((DiatonicRomanNumeral)i).TriadQuality();
            }
        }

        private void Get12RomanPoints()
        {
            Points = new string[12];

            for (int i = 0; i < 12; i++)
            {
                Points[i] = Type == CircleType.Chromatic ?
                        MusicTheory.RomanNumeralToString((ChromaticRomanNumeral)i) :
                        MusicTheory.RomanNumeralToString((ChromaticRomanNumeral)(i * 7 % 12));
            }
        }

        private void GetNamedPoints()
        {
            switch (Type)
            {
                case CircleType.Chromatic: Get12NamedPoints(); break;
                case CircleType.Fifths: Get12NamedPoints(); break;
                default: Get7NamedPoints(); break;
            }
        }

        private void Get7NamedPoints()
        {
            Points = new string[7];

            for (int i = 0; i < 7; i++)
            {
                Points[i] = Type == CircleType.Diatonic2_7 ?
                        MusicTheory.ChordName(((DiatonicRomanNumeral)i).ToChromaticRoman(), Key) :
                        Type == CircleType.Diatonic3_6 ?
                        MusicTheory.ChordName(((DiatonicRomanNumeral)(i * 2 % 7)).ToChromaticRoman(), Key) :
                        MusicTheory.ChordName(((DiatonicRomanNumeral)(i * 4 % 7)).ToChromaticRoman(), Key);
            }
        }

        private void Get12NamedPoints()
        {
            Points = new string[12];

            for (int i = 0; i < 12; i++)
            {
                Points[i] = Type == CircleType.Chromatic ?
                        MusicTheory.ChordName((ChromaticRomanNumeral)i, Key) :
                        MusicTheory.ChordName((ChromaticRomanNumeral)(i * 7 % 12), Key);
            }
        }

        public void TransposeOneSharp()
        {
            Key = (KeyOf)((int)(Key + 7) % 12);
            if (Points.Length == 7) Get7NamedPoints();
            else Get12NamedPoints();
            UpdatePointNames();
        }

        private void UpdatePointNames()
        {
            for (int i = 0; i < Points.Length; i++)
            {
                PointCards[i].SetTextString(Points[i]);
            }
            UpdateCenterCard();
        }

 * 
 */