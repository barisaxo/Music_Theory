using System;

namespace MusicTheory.Intervals
{
    public class Interval
    {
        public Interval(IntervalEnum @enum) { Enum = @enum; }
        public readonly IntervalEnum Enum;
        public int HalfSteps => Enum.Id;
        public string Name => Enum.Name;
        public Quality Quality => Enum.Quality;
        public Quantity Quantity => Enum.Quantity;

        public static explicit operator Interval(int i) => new((IntervalEnum)i);
        public static explicit operator IntervalEnum(Interval i) => i.Enum;
        public static explicit operator int(Interval i) => i.Enum.Id;
    }

    public class P1 : Interval { public P1() : base(IntervalEnum.P1) { } }
    public class mi2 : Interval { public mi2() : base(IntervalEnum.mi2) { } }
    public class M2 : Interval { public M2() : base(IntervalEnum.M2) { } }
    public class mi3 : Interval { public mi3() : base(IntervalEnum.mi3) { } }
    public class M3 : Interval { public M3() : base(IntervalEnum.M3) { } }
    public class P4 : Interval { public P4() : base(IntervalEnum.P4) { } }
    public class TT : Interval { public TT() : base(IntervalEnum.TT) { } }
    public class P5 : Interval { public P5() : base(IntervalEnum.P5) { } }
    public class mi6 : Interval { public mi6() : base(IntervalEnum.mi6) { } }
    public class M6 : Interval { public M6() : base(IntervalEnum.M6) { } }
    public class mi7 : Interval { public mi7() : base(IntervalEnum.mi7) { } }
    public class M7 : Interval { public M7() : base(IntervalEnum.M7) { } }
    public class P8 : Interval { public P8() : base(IntervalEnum.P8) { } }

    public class IntervalEnum : Enumeration
    {
        public IntervalEnum() : base(0, "") { }
        public IntervalEnum(int id, string name, Quality quality, Quantity quantity) : base(id, name)
        {
            switch (quality)
            {
                case Perfect:
                    switch (quantity)
                    {
                        case Second:
                        case Third:
                        case Sixth:
                        case Seventh:
                            throw new ArgumentOutOfRangeException(quantity.Enum.Name + " can not be Perfect.");
                    }
                    break;

                case Augmented:
                case Diminished:
                    switch (quantity)
                    {
                        case Unison:
                        case Octave:
                            throw new ArgumentOutOfRangeException(quantity.Enum.Name + " can not be " + quality.Enum.Name + ".");
                    }
                    break;

                case Major:
                case Minor:
                    switch (quantity)
                    {
                        case Fourth:
                        case Fifth:
                        case Unison:
                        case Octave:
                            throw new ArgumentOutOfRangeException(quantity.Enum.Name + " can not be " + quality.Enum.Name + ".");
                    }
                    break;
            }
            Quality = quality; Quantity = quantity;
        }

        public readonly Quality Quality;
        public readonly Quantity Quantity;

        public static readonly IntervalEnum P1 = new(0, nameof(P1), new Perfect(), new Unison());
        public static readonly IntervalEnum mi2 = new(1, nameof(mi2), new Minor(), new Second());
        public static readonly IntervalEnum M2 = new(2, nameof(M2), new Major(), new Second());
        public static readonly IntervalEnum mi3 = new(3, nameof(mi3), new Minor(), new Third());
        public static readonly IntervalEnum M3 = new(4, nameof(M3), new Major(), new Third());
        public static readonly IntervalEnum P4 = new(5, nameof(P4), new Perfect(), new Fourth());
        public static readonly IntervalEnum TT = new(6, nameof(TT), new Diminished(), new Fifth());
        public static readonly IntervalEnum P5 = new(7, nameof(P5), new Perfect(), new Fifth());
        public static readonly IntervalEnum mi6 = new(8, nameof(mi6), new Minor(), new Sixth());
        public static readonly IntervalEnum M6 = new(9, nameof(M6), new Major(), new Sixth());
        public static readonly IntervalEnum mi7 = new(10, nameof(mi7), new Minor(), new Seventh());
        public static readonly IntervalEnum M7 = new(11, nameof(M7), new Major(), new Seventh());
        public static readonly IntervalEnum P8 = new(12, nameof(P8), new Perfect(), new Octave());

        public static implicit operator Interval(IntervalEnum i) => new(i);
        public static explicit operator IntervalEnum(int i) => FindId<IntervalEnum>(i);
        public static explicit operator IntervalEnum((Quality quality, Quantity quantity) i) => Find<IntervalEnum>(i);

        public static IntervalEnum Find<T>((Quality quality, Quantity quantity) i) where T : Enumeration, new()
        {
            foreach (var e in ListAll<IntervalEnum>()) if (e.Quantity == i.quantity && e.Quality == i.quality) return e;
            throw new ArgumentOutOfRangeException(i.ToString());
        }
    }

    public class QualityEnum : Enumeration
    {
        public QualityEnum() : base(0, "") { }
        public QualityEnum(int id, string name) : base(id, name) { }

        public static readonly QualityEnum Major = new(0, nameof(Major));
        public static readonly QualityEnum Minor = new(1, nameof(Minor));
        public static readonly QualityEnum Augmented = new(2, nameof(Augmented));
        public static readonly QualityEnum Diminished = new(3, nameof(Diminished));
        public static readonly QualityEnum Perfect = new(4, nameof(Perfect));
    }

    public class Quality
    {
        public Quality(QualityEnum @enum) { Enum = @enum; }
        public readonly QualityEnum Enum;
        public static implicit operator QualityEnum(Quality q) => q.Enum;
    }

    public class Major : Quality { public Major() : base(QualityEnum.Major) { } }
    public class Minor : Quality { public Minor() : base(QualityEnum.Minor) { } }
    public class Augmented : Quality { public Augmented() : base(QualityEnum.Augmented) { } }
    public class Diminished : Quality { public Diminished() : base(QualityEnum.Diminished) { } }
    public class Perfect : Quality { public Perfect() : base(QualityEnum.Perfect) { } }

    public class QuantityEnum : Enumeration
    {
        public QuantityEnum() : base(0, "") { }
        public QuantityEnum(int id, string name) : base(id, name) { }
        public static readonly QuantityEnum Unison = new(0, nameof(Unison));
        public static readonly QuantityEnum Second = new(1, nameof(Second));
        public static readonly QuantityEnum Third = new(2, nameof(Third));
        public static readonly QuantityEnum Fourth = new(3, nameof(Fourth));
        public static readonly QuantityEnum Fifth = new(4, nameof(Fifth));
        public static readonly QuantityEnum Sixth = new(5, nameof(Sixth));
        public static readonly QuantityEnum Seventh = new(6, nameof(Seventh));
        public static readonly QuantityEnum Octave = new(7, nameof(Octave));
    }

    public class Quantity
    {
        public Quantity(QuantityEnum @enum) { Enum = @enum; }
        public readonly QuantityEnum Enum;
        public static implicit operator QuantityEnum(Quantity q) => q.Enum;
    }

    public class Unison : Quantity { public Unison() : base(QuantityEnum.Unison) { } }
    public class Second : Quantity { public Second() : base(QuantityEnum.Second) { } }
    public class Third : Quantity { public Third() : base(QuantityEnum.Third) { } }
    public class Fourth : Quantity { public Fourth() : base(QuantityEnum.Fourth) { } }
    public class Fifth : Quantity { public Fifth() : base(QuantityEnum.Fifth) { } }
    public class Sixth : Quantity { public Sixth() : base(QuantityEnum.Sixth) { } }
    public class Seventh : Quantity { public Seventh() : base(QuantityEnum.Seventh) { } }
    public class Octave : Quantity { public Octave() : base(QuantityEnum.Octave) { } }
}
