using MusicTheory.Scales;
using MusicTheory.Intervals;
using MusicTheory.Keys;

namespace MusicTheory.Chords
{
    public class Chord
    {
        string Symbol;
        Key RootNote;
        FamilyEnum Family;
        // Tonality Tonality;
        Shell Shell;
        ChordTone[] ChordTones;
        Extension[] Extensions;
    }

    public class ChordTone
    {
        public ChordTone(ChordToneEnum @enum) { Enum = @enum; }
        public ChordToneEnum Enum { get; private set; }
    }

    public class Root : ChordTone { public Root() : base(ChordToneEnum.Root) { } }
    public class mi3 : ChordTone { public mi3() : base(ChordToneEnum.mi3) { } }
    public class M3 : ChordTone { public M3() : base(ChordToneEnum.M3) { } }
    public class Sus4 : ChordTone { public Sus4() : base(ChordToneEnum.Sus4) { } }
    public class TT : ChordTone { public TT() : base(ChordToneEnum.TT) { } }
    public class b5 : ChordTone { public b5() : base(ChordToneEnum.b5) { } }
    public class P5 : ChordTone { public P5() : base(ChordToneEnum.P5) { } }
    public class S5 : ChordTone { public S5() : base(ChordToneEnum.S5) { } }
    public class mi6 : ChordTone { public mi6() : base(ChordToneEnum.mi6) { } }
    public class M6 : ChordTone { public M6() : base(ChordToneEnum.M6) { } }
    public class dim7 : ChordTone { public dim7() : base(ChordToneEnum.dim7) { } }
    public class mi7 : ChordTone { public mi7() : base(ChordToneEnum.mi7) { } }
    public class M7 : ChordTone { public M7() : base(ChordToneEnum.M7) { } }
    public class b9 : ChordTone { public b9() : base(ChordToneEnum.b9) { } }
    public class _9 : ChordTone { public _9() : base(ChordToneEnum.Nine) { } }
    public class S9 : ChordTone { public S9() : base(ChordToneEnum.S9) { } }
    public class _11 : ChordTone { public _11() : base(ChordToneEnum.Eleven) { } }
    public class S11 : ChordTone { public S11() : base(ChordToneEnum.S11) { } }
    public class b13 : ChordTone { public b13() : base(ChordToneEnum.b13) { } }
    public class _13 : ChordTone { public _13() : base(ChordToneEnum.Thirteen) { } }

    public class ChordToneEnum : Enumeration
    {
        public ChordToneEnum() : base(0, "") { }
        public ChordToneEnum(int id, string name) : base(id, name) { }

        public static ChordToneEnum Root = new(0, nameof(Root));
        public static ChordToneEnum mi3 = new(3, nameof(mi3));
        public static ChordToneEnum M3 = new(4, nameof(M3));
        public static ChordToneEnum Sus4 = new(5, nameof(Sus4));
        public static ChordToneEnum TT = new(6, nameof(TT));
        public static ChordToneEnum b5 = new(6, nameof(b5));
        public static ChordToneEnum P5 = new(7, nameof(P5));
        public static ChordToneEnum S5 = new(8, nameof(S5));
        public static ChordToneEnum mi6 = new(8, nameof(mi6));
        public static ChordToneEnum M6 = new(9, nameof(M6));
        public static ChordToneEnum dim7 = new(9, nameof(dim7));
        public static ChordToneEnum mi7 = new(10, nameof(mi7));
        public static ChordToneEnum M7 = new(11, nameof(M7));
        public static ChordToneEnum b9 = new(1, nameof(b9));
        public static ChordToneEnum Nine = new(2, nameof(Nine));
        public static ChordToneEnum S9 = new(3, nameof(S9));
        public static ChordToneEnum Eleven = new(5, nameof(Eleven));
        public static ChordToneEnum S11 = new(6, nameof(S11));
        public static ChordToneEnum b13 = new(8, nameof(b13));
        public static ChordToneEnum Thirteen = new(9, nameof(Thirteen));
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
            M3_mi7,//Dominant
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
