namespace MusicTheory.ScaleDegrees.Accidental
{
    [System.Serializable]
    public abstract class Accidental : IMusicalElement
    {
        public Accidental(AccidentalEnum @enum) { Enum = @enum; }
        public readonly AccidentalEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;
        public string Description => Enum.Description;
    }

    public class Sharp : Accidental { public Sharp() : base(AccidentalEnum.Sharp) { } }
    public class Flat : Accidental { public Flat() : base(AccidentalEnum.Flat) { } }
    public class Natural : Accidental { public Natural() : base(AccidentalEnum.Natural) { } }

    public class AccidentalEnum : Enumeration
    {
        public AccidentalEnum() : base(0, "") { }
        public AccidentalEnum(int id, string name, string desc) : base(id, name) { Description = desc; }

        public readonly string Description;
        public static AccidentalEnum Sharp = new(1, "♯", nameof(Sharp));
        public static AccidentalEnum Flat = new(-1, "b", nameof(Flat));
        public static AccidentalEnum Natural = new(0, "", nameof(Natural));

        public static explicit operator AccidentalEnum(int i) => FindId<AccidentalEnum>(i);
        public static implicit operator Accidental(AccidentalEnum e) => e switch
        {
            _ when e == Sharp => new Sharp(),
            _ when e == Flat => new Flat(),
            _ when e == Natural => new Natural(),
            _ => throw new System.ArgumentOutOfRangeException(e.ToString())
        };
    }

}