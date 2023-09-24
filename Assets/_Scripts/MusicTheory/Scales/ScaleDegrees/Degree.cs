namespace MusicTheory.ScaleDegrees.DegreeEnum
{
    public abstract class Degree
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

    }

    public class Quality
    {
        public Quality(QualityEnum @enum) { Enum = @enum; }
        public readonly QualityEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;
        public string Description => Enum.Description;
        public static implicit operator QualityEnum(Quality q) => q.Enum;
        public static explicit operator int(Quality q) => q.Id;
    }

    public class Major : Quality { public Major() : base(QualityEnum.Major) { } }
    public class Minor : Quality { public Minor() : base(QualityEnum.Minor) { } }
    public class Augmented : Quality { public Augmented() : base(QualityEnum.Augmented) { } }
    public class Diminished : Quality { public Diminished() : base(QualityEnum.Diminished) { } }
    public class Perfect : Quality { public Perfect() : base(QualityEnum.Perfect) { } }

    public class QualityEnum : Enumeration
    {
        public QualityEnum() : base(0, "") { }
        public QualityEnum(int id, string name, string desc) : base(id, name) { Description = desc; }
        public readonly string Description;

        public static readonly QualityEnum Major = new(0, "M", nameof(Major));
        public static readonly QualityEnum Minor = new(1, "mi", nameof(Minor));
        public static readonly QualityEnum Augmented = new(2, "+", nameof(Augmented));
        public static readonly QualityEnum Diminished = new(3, "º", nameof(Diminished));
        public static readonly QualityEnum Perfect = new(4, "P", nameof(Perfect));

        public static implicit operator Quality(QualityEnum e) => e switch
        {
            _ when e == Major => new Major(),
            _ when e == Minor => new Minor(),
            _ when e == Augmented => new Augmented(),
            _ when e == Diminished => new Diminished(),
            _ when e == Perfect => new Perfect(),
            _ => throw new System.ArgumentOutOfRangeException()
        };
    }
}