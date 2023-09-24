using System;
namespace MusicTheory.Intervals
{
    public class Interval
    {
        public Interval(IntervalEnum @enum) { Enum = @enum; }
        public readonly IntervalEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;
        public string Discription => Enum.Description;
        public Quality Quality => Enum.Quality;
        public Quantity Quantity => Enum.Quantity;

        public static explicit operator IntervalEnum(Interval i) => i.Enum;
        public static explicit operator int(Interval i) => i.Enum.Id;
        public override int GetHashCode() => HashCode.Combine(Enum);


        public override bool Equals(object obj) => obj is Interval e && Enum == e.Enum;
    }

    public class P1 : Interval { public P1() : base(IntervalEnum.P1) { } }
    public class mi2 : Interval { public mi2() : base(IntervalEnum.mi2) { } }
    public class M2 : Interval { public M2() : base(IntervalEnum.M2) { } }
    public class A2 : Interval { public A2() : base(IntervalEnum.A2) { } }
    public class mi3 : Interval { public mi3() : base(IntervalEnum.mi3) { } }
    public class M3 : Interval { public M3() : base(IntervalEnum.M3) { } }
    public class d4 : Interval { public d4() : base(IntervalEnum.d4) { } }
    public class P4 : Interval { public P4() : base(IntervalEnum.P4) { } }
    public class A4 : Interval { public A4() : base(IntervalEnum.A4) { } }
    //public class TT : Interval { public TT() : base(IntervalEnum.TT) { } }
    public class d5 : Interval { public d5() : base(IntervalEnum.d5) { } }
    public class P5 : Interval { public P5() : base(IntervalEnum.P5) { } }
    public class A5 : Interval { public A5() : base(IntervalEnum.A5) { } }
    public class mi6 : Interval { public mi6() : base(IntervalEnum.mi6) { } }
    public class M6 : Interval { public M6() : base(IntervalEnum.M6) { } }
    public class d7 : Interval { public d7() : base(IntervalEnum.d7) { } }
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
                    switch (quantity)
                    {
                        case Unison:
                        case Octave:
                        case Third:
                        case Sixth:
                        case Seventh:
                            throw new ArgumentOutOfRangeException(quantity.Enum.Name + " should not be Augmented.");
                    }
                    break;

                case Diminished:
                    switch (quantity)
                    {
                        case Unison:
                        case Octave:
                        case Second:
                            throw new ArgumentOutOfRangeException(quantity.Enum.Name + " should not be Diminished.");
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
        public string Description => Quantity.Description + " " + Quality.Description;

        public static readonly IntervalEnum P1 = new(0, nameof(P1), new Perfect(), new Unison());
        public static readonly IntervalEnum mi2 = new(1, nameof(mi2), new Minor(), new Second());
        public static readonly IntervalEnum M2 = new(2, nameof(M2), new Major(), new Second());
        public static readonly IntervalEnum A2 = new(3, nameof(A2), new Augmented(), new Second());
        public static readonly IntervalEnum mi3 = new(3, nameof(mi3), new Minor(), new Third());
        public static readonly IntervalEnum M3 = new(4, nameof(M3), new Major(), new Third());
        public static readonly IntervalEnum d4 = new(4, nameof(d4), new Diminished(), new Fourth());
        public static readonly IntervalEnum P4 = new(5, nameof(P4), new Perfect(), new Fourth());
        public static readonly IntervalEnum A4 = new(6, nameof(A4), new Augmented(), new Fourth());
        //public static readonly IntervalEnum TT = new(6, nameof(TT), new Diminished(), new Fifth());
        public static readonly IntervalEnum d5 = new(6, nameof(d5), new Diminished(), new Fifth());
        public static readonly IntervalEnum P5 = new(7, nameof(P5), new Perfect(), new Fifth());
        public static readonly IntervalEnum A5 = new(8, nameof(A5), new Augmented(), new Fifth());
        public static readonly IntervalEnum mi6 = new(8, nameof(mi6), new Minor(), new Sixth());
        public static readonly IntervalEnum M6 = new(9, nameof(M6), new Major(), new Sixth());
        public static readonly IntervalEnum d7 = new(9, nameof(d7), new Diminished(), new Seventh());
        public static readonly IntervalEnum mi7 = new(10, nameof(mi7), new Minor(), new Seventh());
        public static readonly IntervalEnum M7 = new(11, nameof(M7), new Major(), new Seventh());
        public static readonly IntervalEnum P8 = new(12, nameof(P8), new Perfect(), new Octave());

        public static explicit operator IntervalEnum(int i) => FindId<IntervalEnum>(i);
        public static explicit operator IntervalEnum((Quality quality, Quantity quantity) i) => Find(i);

        public static IntervalEnum Find((Quality quality, Quantity quantity) i)
        {
            foreach (var e in ListAll<IntervalEnum>()) if (e.Quantity.Equals(i.quantity) && e.Quality.Equals(i.quality)) return e;
            throw new ArgumentOutOfRangeException(i.quality.Name + " " + i.quantity.Name);
        }

        public static implicit operator Interval(IntervalEnum i) => i switch
        {
            _ when i == P1 => new P1(),
            _ when i == mi2 => new mi2(),
            _ when i == M2 => new M2(),
            _ when i == A2 => new A2(),
            _ when i == mi3 => new mi3(),
            _ when i == M3 => new M3(),
            _ when i == d4 => new d4(),
            _ when i == P4 => new P4(),
            _ when i == A4 => new A4(),
            //_ when i == TT => new TT(),
            _ when i == d5 => new d5(),
            _ when i == P5 => new P5(),
            _ when i == A5 => new A5(),
            _ when i == mi6 => new mi6(),
            _ when i == M6 => new M6(),
            _ when i == d7 => new d7(),
            _ when i == mi7 => new mi7(),
            _ when i == M7 => new M7(),
            _ when i == P8 => new P8(),
            _ => throw new System.ArgumentOutOfRangeException()
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

        public override bool Equals(object obj) => obj is Quality e && e.Enum == Enum;
        public override int GetHashCode() => HashCode.Combine(Enum);
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

        public static explicit operator QualityEnum(ScaleDegrees.DegreeEnum.Quality quality) => quality switch
        {
            ScaleDegrees.DegreeEnum.Major => Major,
            ScaleDegrees.DegreeEnum.Minor => Minor,
            ScaleDegrees.DegreeEnum.Augmented => Augmented,
            ScaleDegrees.DegreeEnum.Diminished => Diminished,
            ScaleDegrees.DegreeEnum.Perfect => Perfect,
            _ => throw new ArgumentOutOfRangeException()
        };
    }



    public class Quantity
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
