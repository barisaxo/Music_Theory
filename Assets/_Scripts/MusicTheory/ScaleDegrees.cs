using MusicTheory.Scales;
using UnityEngine.UIElements;

namespace MusicTheory.ScaleDegrees
{

    public class ScaleDegree
    {
        public ScaleDegree(ScaleDegreeEnum degree) { Enum = degree; }
        public ScaleDegreeEnum Enum { get; private set; }
        public int Id => Enum.Id;
        public string Name => Enum.Name;
        public string Description => Enum.Description;

        public override string ToString() => Name;
        public static explicit operator int(ScaleDegree degree) => degree.Enum.Id;
        public static implicit operator ScaleDegreeEnum(ScaleDegree key) => key.Enum;
        public static explicit operator ScaleDegree(int i) => Enumeration.FindId<ScaleDegreeEnum>(i % 12);
    }

    public class _1 : ScaleDegree { public _1() : base(ScaleDegreeEnum._1) { } }
    public class b2 : ScaleDegree { public b2() : base(ScaleDegreeEnum.b2) { } }
    public class _2 : ScaleDegree { public _2() : base(ScaleDegreeEnum._2) { } }
    public class s2 : ScaleDegree { public s2() : base(ScaleDegreeEnum.s2) { } }
    public class b3 : ScaleDegree { public b3() : base(ScaleDegreeEnum.b3) { } }
    public class _3 : ScaleDegree { public _3() : base(ScaleDegreeEnum._3) { } }
    public class P4 : ScaleDegree { public P4() : base(ScaleDegreeEnum.P4) { } }
    public class s4 : ScaleDegree { public s4() : base(ScaleDegreeEnum.s4) { } }
    //public class TT : ScaleDegree { public TT() : base(ScaleDegreeEnum.TT) { } }
    public class b5 : ScaleDegree { public b5() : base(ScaleDegreeEnum.b5) { } }
    public class P5 : ScaleDegree { public P5() : base(ScaleDegreeEnum.P5) { } }
    public class s5 : ScaleDegree { public s5() : base(ScaleDegreeEnum.s5) { } }
    public class b6 : ScaleDegree { public b6() : base(ScaleDegreeEnum.b6) { } }
    public class _6 : ScaleDegree { public _6() : base(ScaleDegreeEnum._6) { } }
    public class d7 : ScaleDegree { public d7() : base(ScaleDegreeEnum.d7) { } }
    public class b7 : ScaleDegree { public b7() : base(ScaleDegreeEnum.b7) { } }
    public class _7 : ScaleDegree { public _7() : base(ScaleDegreeEnum._7) { } }

    public class ScaleDegreeEnum : Enumeration
    {
        public ScaleDegreeEnum() : base(0, "") { }
        //public ScaleDegreeEnum(int id, string name) : base(id, name) { }
        private ScaleDegreeEnum(int id, string name, string desc) : base(id, name) { Description = desc; }

        public readonly string Description;

        public static ScaleDegreeEnum _1 = new(0, nameof(_1), "Tonic");
        public static ScaleDegreeEnum b2 = new(1, nameof(b2), "Minor Lateral Subdominant");
        public static ScaleDegreeEnum _2 = new(2, nameof(_2), "Lateral Subdominant");
        public static ScaleDegreeEnum s2 = new(3, nameof(s2), "Augmented Lateral Subdominant");
        public static ScaleDegreeEnum b3 = new(3, nameof(b3), "Minor Mediant Tonic");
        public static ScaleDegreeEnum _3 = new(4, nameof(_3), "Mediant Tonic");
        public static ScaleDegreeEnum P4 = new(5, nameof(P4), "Subdominant");
        public static ScaleDegreeEnum s4 = new(6, nameof(s4), "Augmented Subdominant");
        //public static ScaleDegreeEnum TT = new(6, nameof(TT));
        public static ScaleDegreeEnum b5 = new(6, nameof(b5), "Diminished Dominant");
        public static ScaleDegreeEnum P5 = new(7, nameof(P5), "Dominant");
        public static ScaleDegreeEnum s5 = new(8, nameof(s5), "Augmented Dominant");
        public static ScaleDegreeEnum b6 = new(8, nameof(b6), "Minor Submediant Tonic");
        public static ScaleDegreeEnum _6 = new(9, nameof(_6), "Submediant Tonic");
        public static ScaleDegreeEnum d7 = new(9, nameof(d7), "Diminished Lateral Dominant");
        public static ScaleDegreeEnum b7 = new(10, nameof(b7), "Minor Lateral Dominant");
        public static ScaleDegreeEnum _7 = new(11, nameof(_7), "Lateral Dominant");
        // public static ScaleDegreeEnum _8 = new(12, nameof(_1), "Tonic");

        public static implicit operator ScaleDegree(ScaleDegreeEnum s) => new(s);
    }


}