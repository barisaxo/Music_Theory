using MusicTheory.Scales;

namespace MusicTheory.Modes
{
    public enum ModeDegree { Prime, Second, Third, Fourth, Fifth, Sixth, Seventh }

    [System.Serializable]
    public abstract class Mode : IMusicalElement
    {
        public Mode(ScaleEnum parentScale, ModeDegreeEnum mode, string name) { ParentScale = parentScale; Enum = mode; Name = name; }
        public string Name { get; }
        public readonly ScaleEnum ParentScale;
        public readonly ModeDegreeEnum Enum;
        public int Id => Enum.Id;
    }

    public class Ionian : Mode { public Ionian() : base(ScaleEnum.Major, ModeDegreeEnum.Prime, nameof(Ionian)) { } }
    public class Dorian : Mode { public Dorian() : base(ScaleEnum.Major, ModeDegreeEnum.Second, nameof(Dorian)) { } }
    public class Phrygian : Mode { public Phrygian() : base(ScaleEnum.Major, ModeDegreeEnum.Third, nameof(Phrygian)) { } }
    public class Lydian : Mode { public Lydian() : base(ScaleEnum.Major, ModeDegreeEnum.Fourth, nameof(Lydian)) { } }
    public class MixoLydian : Mode { public MixoLydian() : base(ScaleEnum.Major, ModeDegreeEnum.Fifth, nameof(MixoLydian)) { } }
    public class Aeolian : Mode { public Aeolian() : base(ScaleEnum.Major, ModeDegreeEnum.Sixth, nameof(Aeolian)) { } }
    public class Locrian : Mode { public Locrian() : base(ScaleEnum.Major, ModeDegreeEnum.Seventh, nameof(Locrian)) { } }

    public class HarmonicI : Mode { public HarmonicI() : base(ScaleEnum.HarmonicMinor, ModeDegreeEnum.Prime, nameof(HarmonicI)) { } }
    public class HarmonicII : Mode { public HarmonicII() : base(ScaleEnum.HarmonicMinor, ModeDegreeEnum.Second, nameof(HarmonicII)) { } }
    public class HarmonicIII : Mode { public HarmonicIII() : base(ScaleEnum.HarmonicMinor, ModeDegreeEnum.Third, nameof(HarmonicIII)) { } }
    public class HarmonicIV : Mode { public HarmonicIV() : base(ScaleEnum.HarmonicMinor, ModeDegreeEnum.Fourth, nameof(HarmonicIV)) { } }
    public class HarmonicV : Mode { public HarmonicV() : base(ScaleEnum.HarmonicMinor, ModeDegreeEnum.Fifth, nameof(HarmonicV)) { } }
    public class HarmonicVI : Mode { public HarmonicVI() : base(ScaleEnum.HarmonicMinor, ModeDegreeEnum.Sixth, nameof(HarmonicVI)) { } }
    public class HarmonicVII : Mode { public HarmonicVII() : base(ScaleEnum.HarmonicMinor, ModeDegreeEnum.Seventh, nameof(HarmonicVII)) { } }

    public class JazzI : Mode { public JazzI() : base(ScaleEnum.JazzMinor, ModeDegreeEnum.Prime, nameof(JazzI)) { } }
    public class JazzII : Mode { public JazzII() : base(ScaleEnum.JazzMinor, ModeDegreeEnum.Second, nameof(JazzII)) { } }
    public class JazzIII : Mode { public JazzIII() : base(ScaleEnum.JazzMinor, ModeDegreeEnum.Third, nameof(JazzIII)) { } }
    public class JazzIV : Mode { public JazzIV() : base(ScaleEnum.JazzMinor, ModeDegreeEnum.Fourth, nameof(JazzIV)) { } }
    public class JazzV : Mode { public JazzV() : base(ScaleEnum.JazzMinor, ModeDegreeEnum.Fifth, nameof(JazzV)) { } }
    public class JazzVI : Mode { public JazzVI() : base(ScaleEnum.JazzMinor, ModeDegreeEnum.Sixth, nameof(JazzVI)) { } }
    public class JazzVII : Mode { public JazzVII() : base(ScaleEnum.JazzMinor, ModeDegreeEnum.Seventh, nameof(JazzVII)) { } }

    public class Diminished : Mode { public Diminished() : base(ScaleEnum.Diminished, ModeDegreeEnum.Prime, nameof(Diminished)) { } }
    public class Octatonic : Mode { public Octatonic() : base(ScaleEnum.Diminished, ModeDegreeEnum.Second, nameof(Octatonic)) { } }

    public class PentatonicMajor : Mode { public PentatonicMajor() : base(ScaleEnum.Pentatonic, ModeDegreeEnum.Prime, nameof(PentatonicMajor)) { } }
    public class PentatonicII : Mode { public PentatonicII() : base(ScaleEnum.Pentatonic, ModeDegreeEnum.Second, nameof(PentatonicII)) { } }
    public class PentatonicIII : Mode { public PentatonicIII() : base(ScaleEnum.Pentatonic, ModeDegreeEnum.Third, nameof(PentatonicIII)) { } }
    public class PentatonicIV : Mode { public PentatonicIV() : base(ScaleEnum.Pentatonic, ModeDegreeEnum.Fourth, nameof(PentatonicIV)) { } }
    public class PentatonicMinor : Mode { public PentatonicMinor() : base(ScaleEnum.Pentatonic, ModeDegreeEnum.Fifth, nameof(PentatonicMinor)) { } }

    public class Diminished6thI : Mode { public Diminished6thI() : base(ScaleEnum.Diminished6th, ModeDegreeEnum.Prime, nameof(Diminished6thI)) { } }
    public class Diminished6thII : Mode { public Diminished6thII() : base(ScaleEnum.Diminished6th, ModeDegreeEnum.Second, nameof(Diminished6thII)) { } }

    public class WholeTone : Mode { public WholeTone() : base(ScaleEnum.WholeTone, ModeDegreeEnum.Prime, nameof(WholeTone)) { } }

    public class Chromatic : Mode { public Chromatic() : base(ScaleEnum.Chromatic, ModeDegreeEnum.Prime, nameof(Chromatic)) { } }

    public class ModeDegreeEnum : Enumeration
    {
        public ModeDegreeEnum() : base(0, "") { }
        public ModeDegreeEnum(int id, string name) : base(id, name) { }
        //public static int Count => ListAll<ModeDegreeEnum>().Count;

        public static ModeDegreeEnum Prime = new(0, nameof(Prime));
        public static ModeDegreeEnum Second = new(1, nameof(Second));
        public static ModeDegreeEnum Third = new(2, nameof(Third));
        public static ModeDegreeEnum Fourth = new(3, nameof(Fourth));
        public static ModeDegreeEnum Fifth = new(4, nameof(Fifth));
        public static ModeDegreeEnum Sixth = new(5, nameof(Sixth));
        public static ModeDegreeEnum Seventh = new(6, nameof(Seventh));
    }
}