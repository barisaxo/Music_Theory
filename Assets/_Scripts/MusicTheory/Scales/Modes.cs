namespace MusicTheory.Scales
{
    public enum ModeDegree { Prime, Second, Third, Fourth, Fifth, Sixth, Seventh }

    public class Mode : Scale
    {
        public Mode(Scale parentScale, ModeDegreeEnum mode, string name) : base(parentScale.Enum, mode, parentScale.ShiftSteps(mode), parentScale.ShiftDegrees(mode)) { }
    }

    public class Ionian : Mode
    {
        public Ionian() : base(new Major(), ModeDegreeEnum.Prime, nameof(Ionian)) { }
    }

    public class Dorian : Mode
    {
        public Dorian() : base(new Major(), ModeDegreeEnum.Second, nameof(Dorian)) { }
    }

    public class Phrygian : Mode
    {
        public Phrygian() : base(new Major(), ModeDegreeEnum.Third, nameof(Phrygian)) { }
    }

    public class Lydian : Mode { public Lydian() : base(new Major(), ModeDegreeEnum.Fourth, nameof(Lydian)) { } }

    public class MixoLydian : Mode { public MixoLydian() : base(new Major(), ModeDegreeEnum.Fifth, nameof(MixoLydian)) { } }

    public class Aeolian : Mode { public Aeolian() : base(new Major(), ModeDegreeEnum.Sixth, nameof(Aeolian)) { } }

    public class Locrian : Mode { public Locrian() : base(new Major(), ModeDegreeEnum.Seventh, nameof(Locrian)) { } }





    public class ModeDegreeEnum : Enumeration
    {
        public ModeDegreeEnum() : base(0, "") { }
        public ModeDegreeEnum(int id, string name) : base(id, name) { }

        public static ModeDegreeEnum Prime = new(0, nameof(Prime));
        public static ModeDegreeEnum Second = new(1, nameof(Second));
        public static ModeDegreeEnum Third = new(2, nameof(Third));
        public static ModeDegreeEnum Fourth = new(3, nameof(Fourth));
        public static ModeDegreeEnum Fifth = new(4, nameof(Fifth));
        public static ModeDegreeEnum Sixth = new(5, nameof(Sixth));
        public static ModeDegreeEnum Seventh = new(6, nameof(Seventh));
    }
}