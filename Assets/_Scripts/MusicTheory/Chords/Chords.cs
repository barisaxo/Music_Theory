using MusicTheory.Scales;
using MusicTheory.ScaleDegrees;
using MusicTheory.Intervals;
using MusicTheory.Keys;

namespace MusicTheory.Chords
{
    [System.Serializable]
    public abstract class Chord : IMusicalElement
    {
        //public Chord(Key key, Scale scale, ScaleDegree rootScaleDegree)
        //{
        //    //RootNote = scale.ScaleDegrees[(int)rootScaleDegree].;
        //}
        public Chord(ChordEnum @enum) { Enum = @enum; }
        public ChordEnum Enum;
        ChordTone[] ChordTones;
        Extension[] Extensions;
        public int Id => Enum.Id;
        public string Name => Enum.Name;
    }

    public class Major : Chord { public Major() : base(ChordEnum.Major) { } }

    public class ChordEnum : Enumeration
    {
        public ChordEnum() : base(0, "") { }
        public ChordEnum(int id, string name) : base(id, name) { }

        public static ChordEnum Major = new(0, nameof(Major));
        public static ChordEnum Major6 = new(0, nameof(Major6));
        public static ChordEnum Major69 = new(0, nameof(Major69));
        public static ChordEnum Major7 = new(0, nameof(Major7));
        public static ChordEnum Major7S11 = new(0, nameof(Major7S11));

        public static ChordEnum Minor = new(0, nameof(Minor));
        public static ChordEnum Minor7 = new(0, nameof(Minor7));
        public static ChordEnum Minor9 = new(0, nameof(Minor9));
        public static ChordEnum Minor11 = new(0, nameof(Minor11));
        public static ChordEnum Minor13 = new(0, nameof(Minor13));

        public static ChordEnum _7 = new(0, nameof(_7));
        public static ChordEnum _9 = new(0, nameof(_9));
        public static ChordEnum _13 = new(0, nameof(_13));
        public static ChordEnum _7Sus = new(0, nameof(_7Sus));
        public static ChordEnum _9Sus = new(0, nameof(_9Sus));
        public static ChordEnum _13Sus = new(0, nameof(_13Sus));

        public static ChordEnum Minor6 = new(0, nameof(Minor6));
        public static ChordEnum Minor69 = new(0, nameof(Minor69));
        public static ChordEnum MinorMajor7 = new(0, nameof(MinorMajor7));

        public static ChordEnum Minor7b5 = new(0, nameof(Minor7b5));
        public static ChordEnum Minor9b5 = new(0, nameof(Minor9b5));

        public static ChordEnum _7S11 = new(0, nameof(_7S11));
        public static ChordEnum _7Alt = new(0, nameof(_7Alt));
        public static ChordEnum _7b9Sus = new(0, nameof(_7b9Sus));
        public static ChordEnum _7b9 = new(0, nameof(_7b9));
        public static ChordEnum _7S9 = new(0, nameof(_7S9));
        public static ChordEnum _7b13 = new(0, nameof(_7b13));
        public static ChordEnum _9b5 = new(0, nameof(_9b5));
        public static ChordEnum _9S5 = new(0, nameof(_9S5));
        public static ChordEnum _13b9 = new(0, nameof(_13b9));

        public static ChordEnum Aug = new(0, nameof(Aug));
        public static ChordEnum Dim = new(0, nameof(Dim));
        public static ChordEnum Dim7 = new(0, nameof(Dim7));
        public static ChordEnum Dim7Maj7 = new(0, nameof(Dim7Maj7));
    }


    public class Family
    {
        public enum Families { Major, Minor, Dominant, Diminished, TonicMinor, Minor7b5, Altered }
    }

    public class FamilyEnum : Enumeration
    {
        public readonly string Symbol;

        public FamilyEnum() : base(0, "") { }
        public FamilyEnum(int id, string name) : base(id, name) { }
        public FamilyEnum(int id, string name, string symbol) : base(id, name) { Symbol = symbol; }

        public static readonly FamilyEnum Major = new(0, nameof(Major), "∆7");
        public static readonly FamilyEnum Minor = new(1, nameof(Minor), "-7");
        public static readonly FamilyEnum Dominant = new(2, nameof(Dominant), "7");
        public static readonly FamilyEnum Diminished = new(3, nameof(Diminished), "º7");
        public static readonly FamilyEnum TonicMinor = new(4, nameof(TonicMinor), "-∆7");
        public static readonly FamilyEnum Minor7b5 = new(5, nameof(Minor7b5), "-7(b5)");
        public static readonly FamilyEnum Altered = new(6, nameof(Altered), "7(ALT)");
    }


    public enum Quality
    {
        // Maj, _6, _69, Maj7, Maj9, Maj7S11, Maj13,
        // Min, Min7, Min11, Min13,
        // _7, _9, _13,
        // _7Sus, _9Sus, _11, _13Sus,
        // Min6, Min69, MinM7, MinM9, MinM11, MinM13,
        // Min7b5, Min9b5, Min11b5, Min13b5,
        // _7Alt, _7b9, _7S9, _9b5, _9S5, _7S11, _9S11, _7b13, _13b9, _13S9, C7_b13Sus, _13s11, _7Susb9, _13Susb9

    }

    // public class Tonality
    // {
    //     public enum Tonalities { Major, Minor, Dominant, Diminished, TonicMinor, Minor7b5, Altered }
    // }

    public class Shell
    {
        public enum Shells
        {
            M3_M6, M3_M7,//Major
            mi3_mi7,//Minor
            M3_mi7, M3_s5,//Dominant
            mi3_M6, mi3_M7,//Tonic Minor
            mi3_d5_mi7,//-7(b5)
            mi3_d5_d7,//º7
        }
    }

    public class Extension
    {
        public enum Extensions { b5, s5, Min7, _7, Maj7, b9, _9, s9, _11, s11, b13, _13, }
    }



    //public class QualityEnum : Enumeration
    //{
    //    public string Symbol { get; private set; }
    //    public QualityEnum() : base(0, "") { }
    //    public QualityEnum(int id, string name) : base(id, name) { }
    //    public QualityEnum(int id, string name, string desc) : base(id, name) { Symbol = desc; }

    //    public static QualityEnum Major = new(0, nameof(Major), "");
    //    public static QualityEnum Minor = new(1, nameof(Minor), "-");
    //    public static QualityEnum Augmented = new(2, nameof(Augmented), "+");
    //    public static QualityEnum Diminished = new(3, nameof(Diminished), "º");

    //    public static QualityEnum Major6 = new(0, nameof(Major6), "6");
    //    public static QualityEnum Minor6 = new(0, nameof(Minor6), "-6");

    //    public static QualityEnum Major69 = new(0, nameof(Major69), "6/9");
    //    public static QualityEnum Minor69 = new(0, nameof(Minor69), "-6/9");

    //    public static QualityEnum Major7 = new(4, nameof(Major7), "∆7");
    //    public static QualityEnum Minor7 = new(5, nameof(Minor7), "-7");
    //    public static QualityEnum Dominant7 = new(6, nameof(Dominant7), "7");

    //    public static QualityEnum Major9 = new(4, nameof(Major9), "∆9");
    //    public static QualityEnum Minor9 = new(5, nameof(Minor9), "-9");
    //    public static QualityEnum Dominant9 = new(6, nameof(Dominant9), "9");

    //    public static QualityEnum Major7Sharp11 = new(4, nameof(Major7Sharp11), "∆7(#11)");
    //    public static QualityEnum Minor11 = new(5, nameof(Minor11), "-11");
    //    public static QualityEnum TritoneSub = new(12, nameof(TritoneSub), "7(#11)");

    //    public static QualityEnum Sus = new(0, nameof(Sus), "7(SUS)");

    //    public static QualityEnum Diminished7 = new(7, nameof(Diminished7), "º7");

    //    public static QualityEnum TonicMinor7 = new(9, nameof(TonicMinor7), "-∆7");
    //    public static QualityEnum HalfDiminished = new(8, nameof(HalfDiminished), "ø");
    //    public static QualityEnum AlteredDominant = new(10, nameof(AlteredDominant), "7(ALT)");
    //    public static QualityEnum DiminishedDominant = new(11, nameof(DiminishedDominant), "7(b9)");

    //}

}


// public enum ChordTones
// {
//     Root = 0,
//     mi3 = 3,
//     M3 = 4,
//     Sus4 = 5,
//     b5 = 6,
//     P5 = 7,
//     S5 = 8,
//     dim7 = 9,
//     mi7 = 10,
//     M7 = 11,
//     b9 = 1,
//     Nine = 2,
//     S9 = 3,
//     Eleven = 5,
//     S11 = 6,
//     b13 = 8,
//     Thirteen = 9,
// }
