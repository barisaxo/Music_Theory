namespace MusicTheory.ScaleDegrees.DegreeEnum
{
    [System.Serializable]
    public abstract class Degree : IMusicalElement
    {
        public Degree(DegreeEnum @enum) => DegreeEnum = @enum;
        public readonly DegreeEnum DegreeEnum;
        public int Id => DegreeEnum.Id;
        public string Name => DegreeEnum.Name;
    }

    public class _1 : Degree { public _1() : base(DegreeEnum._1) { } }
    public class _2 : Degree { public _2() : base(DegreeEnum._2) { } }
    public class _3 : Degree { public _3() : base(DegreeEnum._3) { } }
    public class _4 : Degree { public _4() : base(DegreeEnum._4) { } }
    public class _5 : Degree { public _5() : base(DegreeEnum._5) { } }
    public class _6 : Degree { public _6() : base(DegreeEnum._6) { } }
    public class _7 : Degree { public _7() : base(DegreeEnum._7) { } }

    public class DegreeEnum : Enumeration
    {
        public DegreeEnum() : base(0, "") { }
        public DegreeEnum(int id, string name) : base(id, name) { }

        public static DegreeEnum _1 = new(0, "1");
        public static DegreeEnum _2 = new(1, "2");
        public static DegreeEnum _3 = new(2, "3");
        public static DegreeEnum _4 = new(3, "4");
        public static DegreeEnum _5 = new(4, "5");
        public static DegreeEnum _6 = new(5, "6");
        public static DegreeEnum _7 = new(6, "7");

        public static explicit operator DegreeEnum(int i) => FindId<DegreeEnum>(i);
        public static implicit operator Degree(DegreeEnum e) => e switch
        {
            _ when e == _1 => new _1(),
            _ when e == _2 => new _2(),
            _ when e == _3 => new _3(),
            _ when e == _4 => new _4(),
            _ when e == _5 => new _5(),
            _ when e == _6 => new _6(),
            _ when e == _7 => new _7(),
            _ => throw new System.ArgumentOutOfRangeException(e.ToString())
        };

        public static explicit operator DegreeEnum(Intervals.QuantityEnum e) => e switch
        {
            _ when e == Intervals.QuantityEnum.Unison => _1,
            _ when e == Intervals.QuantityEnum.Second => _2,
            _ when e == Intervals.QuantityEnum.Third => _3,
            _ when e == Intervals.QuantityEnum.Fourth => _4,
            _ when e == Intervals.QuantityEnum.Fifth => _5,
            _ when e == Intervals.QuantityEnum.Sixth => _6,
            _ when e == Intervals.QuantityEnum.Seventh => _7,
            _ => throw new System.ArgumentOutOfRangeException(e.ToString())
        };
    }

}