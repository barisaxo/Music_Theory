
using MusicTheory.ScaleDegrees.DegreeEnum;

namespace MusicTheory.ScaleDegrees
{
    [System.Serializable]
    public abstract class ScaleDegree : IMusicalElement
    {
        public ScaleDegree(ScaleDegreeEnum degree) { Enum = degree; }
        public readonly ScaleDegreeEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;
        public string Description => Enum.Description;

        public override string ToString() => Name;
        public override bool Equals(object obj) => obj is ScaleDegree e && e.Enum == Enum;
        public override int GetHashCode() => System.HashCode.Combine(Enum);
        //public static explicit operator int(ScaleDegree degree) => degree.Enum.Id;
        //public static implicit operator ScaleDegreeEnum(ScaleDegree degree) => degree.Enum;
    }

    public class _1 : ScaleDegree { public _1() : base(ScaleDegreeEnum._1) { } }
    public class b2 : ScaleDegree { public b2() : base(ScaleDegreeEnum.b2) { } }
    public class _2 : ScaleDegree { public _2() : base(ScaleDegreeEnum._2) { } }
    public class s2 : ScaleDegree { public s2() : base(ScaleDegreeEnum.s2) { } }
    public class b3 : ScaleDegree { public b3() : base(ScaleDegreeEnum.b3) { } }
    public class _3 : ScaleDegree { public _3() : base(ScaleDegreeEnum._3) { } }
    public class b4 : ScaleDegree { public b4() : base(ScaleDegreeEnum.b4) { } }
    public class P4 : ScaleDegree { public P4() : base(ScaleDegreeEnum.P4) { } }
    public class s4 : ScaleDegree { public s4() : base(ScaleDegreeEnum.s4) { } }
    public class b5 : ScaleDegree { public b5() : base(ScaleDegreeEnum.b5) { } }
    public class P5 : ScaleDegree { public P5() : base(ScaleDegreeEnum.P5) { } }
    public class s5 : ScaleDegree { public s5() : base(ScaleDegreeEnum.s5) { } }
    public class b6 : ScaleDegree { public b6() : base(ScaleDegreeEnum.b6) { } }
    public class _6 : ScaleDegree { public _6() : base(ScaleDegreeEnum._6) { } }
    public class d7 : ScaleDegree { public d7() : base(ScaleDegreeEnum.d7) { } }
    public class b7 : ScaleDegree { public b7() : base(ScaleDegreeEnum.b7) { } }
    public class _7 : ScaleDegree { public _7() : base(ScaleDegreeEnum._7) { } }

    public partial class ScaleDegreeEnum : Enumeration
    {
        public ScaleDegreeEnum() : base(0, "") { }
        private ScaleDegreeEnum(int id, string name, string desc, Degree degree, Quality quality) : base(id, name)
        {
            Description = desc; Degree = degree; Quality = quality;
        }

        public readonly Quality Quality;
        public readonly Degree Degree;
        public readonly string Description;

        public static ScaleDegreeEnum _1 = new(0, "1", "Tonic", new DegreeEnum._1(), new Perfect());
        public static ScaleDegreeEnum b2 = new(1, nameof(b2), "Minor Lateral Subdominant", new DegreeEnum._2(), new Minor());
        public static ScaleDegreeEnum _2 = new(2, "2", "Lateral Subdominant", new DegreeEnum._2(), new Major());
        public static ScaleDegreeEnum s2 = new(3, "♯2", "Augmented Lateral Subdominant", new DegreeEnum._2(), new Augmented());
        public static ScaleDegreeEnum b3 = new(3, nameof(b3), "Minor Mediant Tonic", new DegreeEnum._3(), new Minor());
        public static ScaleDegreeEnum _3 = new(4, "3", "Mediant Tonic", new DegreeEnum._3(), new Major());
        public static ScaleDegreeEnum b4 = new(4, nameof(b4), "Diminished Subdominant", new DegreeEnum._4(), new Diminished());
        public static ScaleDegreeEnum P4 = new(5, "4", "Subdominant", new DegreeEnum._4(), new Perfect());
        public static ScaleDegreeEnum s4 = new(6, "♯4", "Augmented Subdominant", new DegreeEnum._4(), new Augmented());
        public static ScaleDegreeEnum b5 = new(6, nameof(b5), "Diminished Dominant", new DegreeEnum._5(), new Diminished());
        public static ScaleDegreeEnum P5 = new(7, "5", "Dominant", new DegreeEnum._5(), new Perfect());
        public static ScaleDegreeEnum s5 = new(8, "♯5", "Augmented Dominant", new DegreeEnum._5(), new Augmented());
        public static ScaleDegreeEnum b6 = new(8, nameof(b6), "Minor Submediant Tonic", new DegreeEnum._6(), new Minor());
        public static ScaleDegreeEnum _6 = new(9, "6", "Submediant Tonic", new DegreeEnum._6(), new Major());
        public static ScaleDegreeEnum d7 = new(9, "º7", "Diminished Lateral Dominant", new DegreeEnum._7(), new Diminished());
        public static ScaleDegreeEnum b7 = new(10, nameof(b7), "Minor Lateral Dominant", new DegreeEnum._7(), new Minor());
        public static ScaleDegreeEnum _7 = new(11, "7", "Lateral Dominant", new DegreeEnum._7(), new Major());

        public static implicit operator ScaleDegree(ScaleDegreeEnum s) => s switch
        {
            _ when s == _1 => new _1(),
            _ when s == b2 => new b2(),
            _ when s == _2 => new _2(),
            _ when s == s2 => new s2(),
            _ when s == b3 => new b3(),
            _ when s == _3 => new _3(),
            _ when s == b4 => new b4(),
            _ when s == P4 => new P4(),
            _ when s == s4 => new s4(),
            _ when s == b5 => new b5(),
            _ when s == P5 => new P5(),
            _ when s == s5 => new s5(),
            _ when s == b6 => new b6(),
            _ when s == _6 => new _6(),
            _ when s == d7 => new d7(),
            _ when s == b7 => new b7(),
            _ when s == _7 => new _7(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

    }


}