using System;

namespace MusicTheory.Intervals
{
    [System.Serializable]
    public abstract class Quantity : IMusicalElement
    {
        public Quantity(QuantityEnum @enum) { Enum = @enum; }
        public readonly QuantityEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;
        public string Description => Enum.Description;
        public static implicit operator QuantityEnum(Quantity q) => q.Enum;
        public static explicit operator int(Quantity q) => q.Id;
        public override bool Equals(object obj) => obj is Quantity e && e.Enum == Enum;
        public override int GetHashCode() => HashCode.Combine(Enum);
    }

    public class Unison : Quantity { public Unison() : base(QuantityEnum.Unison) { } }
    public class Second : Quantity { public Second() : base(QuantityEnum.Second) { } }
    public class Third : Quantity { public Third() : base(QuantityEnum.Third) { } }
    public class Fourth : Quantity { public Fourth() : base(QuantityEnum.Fourth) { } }
    public class Fifth : Quantity { public Fifth() : base(QuantityEnum.Fifth) { } }
    public class Sixth : Quantity { public Sixth() : base(QuantityEnum.Sixth) { } }
    public class Seventh : Quantity { public Seventh() : base(QuantityEnum.Seventh) { } }
    public class Octave : Quantity { public Octave() : base(QuantityEnum.Octave) { } }

    public class QuantityEnum : Enumeration
    {
        public QuantityEnum() : base(0, "") { }
        public QuantityEnum(int id, string name, string desc) : base(id, name) { Description = desc; }
        public readonly string Description;

        public static readonly QuantityEnum Unison = new(0, "1", nameof(Unison));
        public static readonly QuantityEnum Second = new(1, "2", nameof(Second));
        public static readonly QuantityEnum Third = new(2, "3", nameof(Third));
        public static readonly QuantityEnum Fourth = new(3, "4", nameof(Fourth));
        public static readonly QuantityEnum Fifth = new(4, "5", nameof(Fifth));
        public static readonly QuantityEnum Sixth = new(5, "6", nameof(Sixth));
        public static readonly QuantityEnum Seventh = new(6, "7", nameof(Seventh));
        public static readonly QuantityEnum Octave = new(7, "8", nameof(Octave));

        public static implicit operator Quantity(QuantityEnum e) => e switch
        {
            _ when e == Unison => new Unison(),
            _ when e == Second => new Second(),
            _ when e == Third => new Third(),
            _ when e == Fourth => new Fourth(),
            _ when e == Fifth => new Fifth(),
            _ when e == Sixth => new Sixth(),
            _ when e == Seventh => new Seventh(),
            _ when e == Octave => new Octave(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        public static explicit operator QuantityEnum(int i) => i switch
        {
            _ when i == 0 => new Unison(),
            _ when i == 1 => new Second(),
            _ when i == 2 => new Third(),
            _ when i == 3 => new Fourth(),
            _ when i == 4 => new Fifth(),
            _ when i == 5 => new Sixth(),
            _ when i == 6 => new Seventh(),
            _ when i == 7 => new Octave(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        public static explicit operator QuantityEnum(ScaleDegrees.DegreeEnum.Degree degree) => degree switch
        {
            ScaleDegrees.DegreeEnum._1 => Unison,
            ScaleDegrees.DegreeEnum._2 => Second,
            ScaleDegrees.DegreeEnum._3 => Third,
            ScaleDegrees.DegreeEnum._4 => Fourth,
            ScaleDegrees.DegreeEnum._5 => Fifth,
            ScaleDegrees.DegreeEnum._6 => Sixth,
            ScaleDegrees.DegreeEnum._7 => Seventh,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
