namespace MusicTheory.ScaleDegrees.DegreeEnum
{
    [System.Serializable]
    public abstract class Quality : IMusicalElement
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

        public static explicit operator QualityEnum(Intervals.QualityEnum e) => e switch
        {
            _ when e == Intervals.QualityEnum.Major => new Major(),
            _ when e == Intervals.QualityEnum.Minor => new Minor(),
            _ when e == Intervals.QualityEnum.Augmented => new Augmented(),
            _ when e == Intervals.QualityEnum.Diminished => new Diminished(),
            _ when e == Intervals.QualityEnum.Perfect => new Perfect(),
            _ => throw new System.ArgumentOutOfRangeException()
        };
    }
}