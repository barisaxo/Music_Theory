using System;

namespace MusicTheory.Intervals
{
    [System.Serializable]
    public abstract class Interval : IMusicalElement
    {
        public Interval(IntervalEnum @enum) { Enum = @enum; }
        public readonly IntervalEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;
        public string Description => Enum.Description;
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
        public string Description => Quality.Description + " " + Quantity.Description;

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
        public static readonly IntervalEnum P8 = new(0, nameof(P8), new Perfect(), new Octave());

        public static explicit operator IntervalEnum(int i) => FindId<IntervalEnum>(i);
        public static explicit operator IntervalEnum((Quality quality, Quantity quantity) i) => Find(i);

        public static IntervalEnum Find((Quality quality, Quantity quantity) i)
        {
            foreach (var e in All<IntervalEnum>()) if (e.Quantity.Equals(i.quantity) && e.Quality.Equals(i.quality)) return e;
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


}
