namespace MusicTheory.Triads
{
    public abstract class Triad
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

        public static TriadEnum Secundal = new(3, "<size=60%><font-weight=\"100\"><voffset=0.5em>2</voffset></font-weight><size=100%>", nameof(Secundal)) { };
        public static TriadEnum Quartal = new(3, "<size=60%><font-weight=\"100\"><voffset=0.5em>4</voffset></font-weight><size=100%>", nameof(Quartal)) { };
    }

    public class Major : Triad { public Major() : base(TriadEnum.Major) { } }
    public class Minor : Triad { public Minor() : base(TriadEnum.Minor) { } }
    public class Augmented : Triad { public Augmented() : base(TriadEnum.Augmented) { } }
    public class Diminished : Triad { public Diminished() : base(TriadEnum.Diminished) { } }
    public class Secundal : Triad { public Secundal() : base(TriadEnum.Secundal) { } }
    public class Quartal : Triad { public Quartal() : base(TriadEnum.Quartal) { } }
}