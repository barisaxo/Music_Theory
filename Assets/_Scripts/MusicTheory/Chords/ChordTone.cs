namespace MusicTheory.Chords
{
    [System.Serializable]
    public abstract class ChordTone : IMusicalElement
    {
        public ChordTone(ChordToneEnum @enum) { Enum = @enum; }
        public ChordToneEnum Enum { get; private set; }
        public string Name => Enum.Name;
        public int Id => Enum.Id;
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
}