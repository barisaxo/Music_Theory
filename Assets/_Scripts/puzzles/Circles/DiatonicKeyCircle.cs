using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Keys;
using MusicTheory.Intervals;
using MusicTheory.ScaleDegrees;

namespace MusicTheory
{
    public class DiatonicKeyCircle : Circle, IKey<DiatonicKeyCircle>
    {
        public DiatonicKeyCircle(string name, float radius, Vector2 pos, Key key, Scales.Scale scale, ScaleDegree degree, CircleType type) : base(name, radius, pos)
        {
            Type = type;
            CurrentKey = key;
            Scale = scale;
            CurrentScaleDegree = degree;
            PointNames = GetPointNames();
            this.DrawCircle();
        }

        public enum CircleType { Seconds, Thirds, Fifths, }
        public readonly CircleType Type;

        private Scales.Scale _scale;
        public Scales.Scale Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                //todo set texts;
            }
        }

        private ScaleDegree _scaleDegree;
        public ScaleDegree CurrentScaleDegree
        {
            get => _scaleDegree;
            set
            {
                _scaleDegree = value;
                //todo update texts;
            }
        }

        private Key _key = new C();
        public Key CurrentKey
        {
            get => _key;
            set { _key = value; }
        }


        private string[] GetPointNames()
        {
            int x = 0;

            for (int i = 0; i < Scale.ScaleDegrees.Length; i++)
                if (CurrentScaleDegree.Equals(Scale.ScaleDegrees[i])) { x = i; break; }

            string[] temp = new string[Scale.ScaleDegrees.Length];

            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = CurrentKey.GetKeyAbove(Scale.ScaleDegrees[(x + i) % Scale.ScaleDegrees.Length].AsInterval()).Name;
            }

            return temp;
        }


        public Key ScrollKey(Interval delta)
        {
            this.RotateCounterClockwise();
            return CurrentKey;
        }
    }
}