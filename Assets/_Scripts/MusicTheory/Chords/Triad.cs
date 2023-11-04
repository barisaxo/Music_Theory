namespace MusicTheory.Triads
{
    [System.Serializable]
    public abstract class Triad : IMusicalElement
    {
        public Triad(TriadEnum @enum) { Enum = @enum; }
        public readonly TriadEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;
        public string Description => Enum.Description;
    }

    public class TriadEnum : Enumeration
    {
        public TriadEnum() : base(0, "") { }
        public TriadEnum(int id, string name, string desc) : base(id, name) { Description = desc; }
        public readonly string Description;

        public static TriadEnum Major = new(0, "", nameof(Major)) { };
        public static TriadEnum Minor = new(1, "-", nameof(Minor)) { };
        public static TriadEnum Augmented = new(2, "+", nameof(Augmented)) { };
        public static TriadEnum Diminished = new(3, "º", nameof(Diminished)) { };

        //public static TriadEnum Secundal = new(3, "<size=60%><font-weight=\"100\"><voffset=0.5em>2</voffset></font-weight><size=100%>", nameof(Secundal)) { };
        //public static TriadEnum Quartal = new(3, "<size=60%><font-weight=\"100\"><voffset=0.5em>4</voffset></font-weight><size=100%>", nameof(Quartal)) { };

        public static implicit operator Triad(TriadEnum e) => e switch
        {
            _ when e == TriadEnum.Major => new Major(),
            _ when e == TriadEnum.Minor => new Minor(),
            _ when e == TriadEnum.Augmented => new Augmented(),
            _ when e == TriadEnum.Diminished => new Diminished(),
            _ => throw new System.ArgumentOutOfRangeException(e.ToString())
        };
    }

    public class Major : Triad { public Major() : base(TriadEnum.Major) { } }
    public class Minor : Triad { public Minor() : base(TriadEnum.Minor) { } }
    public class Augmented : Triad { public Augmented() : base(TriadEnum.Augmented) { } }
    public class Diminished : Triad { public Diminished() : base(TriadEnum.Diminished) { } }
    //public class Secundal : Triad { public Secundal() : base(TriadEnum.Secundal) { } }
    //public class Quartal : Triad { public Quartal() : base(TriadEnum.Quartal) { } }

    public static class TriadChordTones
    {
        public static Intervals.Interval[] ChordTonesAsIntervals(this Triad triad)
        {
            Intervals.Interval[] temp = new Intervals.Interval[2];

            temp[0] = triad switch
            {
                Major or Augmented => new Intervals.M3(),
                Minor or Diminished => new Intervals.mi3(),
                _ => throw new System.ArgumentOutOfRangeException(triad.Description)
            };

            temp[1] = triad switch
            {
                Major or Minor => new Intervals.P5(),
                Augmented => new Intervals.A5(),
                Diminished => new Intervals.d5(),
                _ => throw new System.ArgumentOutOfRangeException(triad.Description)
            };
            return temp;
        }
    }
}