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

        public Key ScrollKey(Interval delta)
        {
            this.RotateCounterClockwise();
            CurrentKey = (Key)(((int)CurrentKey + (int)delta) % 12);
            foreach (var key in Enumeration.ListAll<KeyEnum>())
            {
                if (key.Letter.Equals(((Key)(((int)CurrentKey + 1) % 12)).Enum.Letter) && key.Id == CurrentKey.Id)
                {
                    if (key.Accidental is Sharp)
                    {
                        foreach (var k in Enumeration.ListAll<KeyEnum>())
                        {
                            if (k.Letter.Equals(((Key)(((int)key + 1) % 12)).Enum.Letter) && key.Id == key.Id)
                            {
                                return CurrentKey = k;
                            }
                        }
                    }
                    return CurrentKey = key;
                }
            }
            return CurrentKey;
        }

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
                Key fifth = ((Key)(((int)CurrentKey + (i * 7)) % 12));

                if (fifth.Enum.Accidental is Sharp)
                {
                    foreach (var key in Enumeration.ListAll<KeyEnum>())
                    {
                        if (key.Letter.Equals(((Key)(((int)fifth + 1) % 12)).Enum.Letter) && key.Id == fifth.Id)
                        {
                            fifth = key;
                            break;
                        }
                    }
                }

                temp[i] = fifth.Name;
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