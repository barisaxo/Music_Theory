using System;
using UnityEngine;
using MusicTheory.Keys;
using MusicTheory.Intervals;
using MusicTheory.Arithmetic;

namespace MusicTheory
{
    public class ChromaticKeyCircle : Circle
    {
        public ChromaticKeyCircle(string name, float radius, Vector2 pos, Key key, CircleType type) : base(name, radius, pos)
        {
            Key = key;
            Type = type;
            PointNames = GetPointNames();
            this.DrawCircle();
        }

        public enum CircleType { Fifths, Chromatic }
        public CircleType Type;
        public Key Key { get; set; }
        public Key[] Fifths => GetFifths();

        private string[] GetPointNames() => Type switch
        {
            CircleType.Chromatic => GetChromaticNames(),
            CircleType.Fifths => GetFifthsNames(),
            _ => throw new ArgumentOutOfRangeException(nameof(CircleType), Type + " is not a valid circle type.")
        };

        public Key GetClickedKey(GameObject go)
        {
            for (int i = 0; i < PointCards.Length; i++)
                if (go.transform.IsChildOf(PointCards[i].TMP.gameObject.transform))
                    return Fifths[(i + Key.Id * 7) % 12];

            return Key;
        }

        private string[] GetFifthsNames()
        {
            string[] temp = new string[12];
            Key[] fifths = Fifths;

            for (int i = 0; i < temp.Length; i++)
                temp[i] = fifths[(i + Key.Id * 7) % fifths.Length].Name;

            return temp;
        }

        private Key[] GetFifths()
        {
            Key[] temp = new Key[12];
            Key newFifth = new C();

            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = newFifth;
                newFifth = newFifth.GetKeyAbove(new P5()).KeepFlatOrNatural();
            }

            return temp;
        }

        private string[] GetChromaticNames()
        {
            string[] temp = new string[12];


            //for (int i = 0; i < temp.Length; i++)
            //    temp[i] = ((Key)((i + Key.Id) % temp.Length)).Name;

            return temp;
        }

    }
}



//public Key ScrollKey(Interval delta)
//{
//    this.RotateCounterClockwise();
//    Key = Key.GetKeyAbove(delta);
//    if (Key.Enum.Accidental is Sharp)
//    {
//        foreach (var k in Enumeration.ListAll<KeyEnum>())
//        {
//            if (k.Letter.Equals(((Key)(((int)Key + 1) % 12)).Enum.Letter) && Key.Id == Key.Id)
//            {
//                return Key = k;
//            }
//        }
//    }
//    return Key;
//}
