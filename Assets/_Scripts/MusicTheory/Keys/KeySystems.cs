using System;

namespace MusicTheory.Keys
{
    public static class KeySystems
    {
        public static Key GetKeyAbove(this Key key, Intervals.Interval interval)
        {
            if (interval is Intervals.P1 || interval is Intervals.P8) return key;

            int newKeyId = (key.Id + interval.Id) % 12;

            Letter letter = (LetterEnum)((key.Enum.Letter.Id + interval.Quantity.Id) % 7);

            foreach (var e in Enumeration.ListAll<KeyEnum>())
            {
                if (e.Letter.Equals(letter))
                {
                    if (e.Id == newKeyId)
                    {
                        return e;
                    }
                }
            }

            letter = (LetterEnum)((letter.Id + 1) % 7);
            foreach (var e in Enumeration.ListAll<KeyEnum>())
            {
                if (e.Letter.Equals(letter))
                {
                    if (e.Id == newKeyId)
                    {
                        return e;
                    }
                }
            }

            letter = (LetterEnum)((letter.Id + 7 - 2) % 7);
            foreach (var e in Enumeration.ListAll<KeyEnum>())
            {
                if (e.Letter.Equals(letter))
                {
                    if (e.Id == newKeyId)
                    {
                        return e;
                    }
                }
            }

            throw new ArgumentOutOfRangeException(key.Name, interval.Name);
        }

        //public static Key GetMinor2ndAbove(this Key key)
        //{
        //    return key switch
        //    {
        //        A => new Bb(),
        //        As => new B(),
        //        Bb => new Cb(),
        //        B => new C(),
        //        Bs => new Cs(),
        //        C => new Db(),
        //        Cs => new D(),
        //        Db => new D(),
        //        D => new Eb(),
        //        Ds => new E(),
        //        Eb => new Fb(),
        //        E => new F(),
        //        Es => new Fs(),
        //        Fb => new F(),
        //        F => new Gb(),
        //        Fs => new G(),
        //        Gb => new G(),
        //        G => new Ab(),
        //        Gs => new A(),
        //        _ => throw new NotImplementedException()
        //    };
        //}

        //public static Key GetMajor2ndAbove(this Key key)
        //{
        //    return key switch
        //    {
        //        A => new B(),
        //        As => new Bs(),
        //        Bb => new C(),
        //        B => new Cs(),
        //        Bs => new D(),
        //        C => new D(),
        //        Cs => new Ds(),
        //        Db => new Eb(),
        //        D => new E(),
        //        Ds => new Es(),
        //        Eb => new F(),
        //        E => new Fs(),
        //        Es => new G(),
        //        Fb => new Gb(),
        //        F => new G(),
        //        Fs => new Gs(),
        //        Gb => new Ab(),
        //        G => new A(),
        //        Gs => new As(),
        //        _ => throw new NotImplementedException()
        //    };
        //}

        //public static Key GetAugmented2ndAbove(this Key key)
        //{
        //    return key switch
        //    {
        //        A => new Bs(),
        //        As => new C(),
        //        Bb => new Cs(),
        //        B => new Cs(),//TODO pick up from here
        //        Bs => new D(),
        //        C => new D(),
        //        Cs => new Ds(),
        //        Db => new Eb(),
        //        D => new E(),
        //        Ds => new Es(),
        //        Eb => new F(),
        //        E => new Fs(),
        //        Es => new G(),
        //        Fb => new Gb(),
        //        F => new G(),
        //        Fs => new Gs(),
        //        Gb => new Ab(),
        //        G => new A(),
        //        Gs => new As(),
        //        _ => throw new NotImplementedException()
        //    };
        //}

        //public static string EnharmonicNoteName(this Key note, Key key)
        //{
        //    switch (note)
        //    {
        //        case Gb: switch (key) { case A: case B: case D: case E: case G: return "F♯"; } break;
        //        case Db: switch (key) { case A: case B: case D: case E: return "C♯"; } break;
        //        case Eb: switch (key) { case B: case E: return "D♯"; } break;
        //        case Ab: switch (key) { case B: case E: return "G♯"; } break;
        //        case Bb: switch (key) { case B: return "A♯"; } break;
        //        case B: switch (key) { case Gb: return "Cb"; } break;
        //            //case Es: switch (key) { case C: case D:  }break;
        //    }
        //    return note.ToString();
        //}



        //public static Key InverselyTransposed(this Key key, Key tKey) => (Key)((tKey - key) < 0 ? tKey - key + 12 : tKey - key);
    }

}

//public interface INominal
//{
//    public string Name { get; }
//}

//public interface ICardinal
//{
//    public int Id { get; }
//}

//public interface IOrdinal
//{
//    public int Id { get; }
//}