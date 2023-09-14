using System;
using UnityEngine;
using MusicTheory.Keys;
using MusicTheory.Intervals;


namespace MusicTheory
{
    public class ChromaticKeyCircle : Circle, IKey<ChromaticKeyCircle>
    {
        public ChromaticKeyCircle(string name, float radius, Vector2 pos, Key key, CircleType type) : base(name, radius, pos)
        {
            CurrentKey = key;
            Type = type;
            PointNames = GetPointNames();
            this.DrawCircle();
        }

        public Key CurrentKey { get; set; }
        public CircleType Type;

        public void ScrollKey(Interval delta) { CurrentKey = (Key)(((int)CurrentKey + (int)delta) % 12); }

        private string[] GetPointNames() => Type switch
        {
            CircleType.Chromatic => GetChromaticNames(),
            CircleType.Fifths => GetFifthsNames(),
            _ => throw new ArgumentOutOfRangeException(nameof(CircleType), Type + " is not a valid circle type.")
        };

        private string[] GetFifthsNames()
        {
            string[] temp = new string[12];

            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = ((Key)(((int)CurrentKey + (i * 7)) % 12)).ToString();
            }

            return temp;
        }

        private string[] GetChromaticNames()
        {
            string[] temp = new string[12];

            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = ((Key)(((int)CurrentKey + i) % 12)).ToString();
            }

            return temp;
        }

        public enum CircleType { Fifths, Chromatic }
    }
}