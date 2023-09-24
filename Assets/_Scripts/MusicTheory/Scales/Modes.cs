using MusicTheory.Scales;

namespace MusicTheory.Modes
{
    public enum ModeDegree { Prime, Second, Third, Fourth, Fifth, Sixth, Seventh }

    public class Mode
    {
        public Mode(Scale parentScale, ModeDegreeEnum mode, string name) { ParentScale = parentScale; Enum = mode; Name = name; }//: base(parentScale.Enum, mode, parentScale.ShiftSteps(mode), parentScale.ScaleDegrees) { Name = name; }
        public readonly string Name;
        public readonly Scale ParentScale;
        public readonly ModeDegreeEnum Enum;
    }

    public class Ionian : Mode { public Ionian() : base(new Major(), ModeDegreeEnum.Prime, nameof(Ionian)) { } }
    public class Dorian : Mode { public Dorian() : base(new Major(), ModeDegreeEnum.Second, nameof(Dorian)) { } }
    public class Phrygian : Mode { public Phrygian() : base(new Major(), ModeDegreeEnum.Third, nameof(Phrygian)) { } }
    public class Lydian : Mode { public Lydian() : base(new Major(), ModeDegreeEnum.Fourth, nameof(Lydian)) { } }
    public class MixoLydian : Mode { public MixoLydian() : base(new Major(), ModeDegreeEnum.Fifth, nameof(MixoLydian)) { } }
    public class Aeolian : Mode { public Aeolian() : base(new Major(), ModeDegreeEnum.Sixth, nameof(Aeolian)) { } }
    public class Locrian : Mode { public Locrian() : base(new Major(), ModeDegreeEnum.Seventh, nameof(Locrian)) { } }

    public class HarmonicI : Mode { public HarmonicI() : base(new HarmonicMinor(), ModeDegreeEnum.Prime, nameof(HarmonicMinor)) { } }
    public class HarmonicII : Mode { public HarmonicII() : base(new HarmonicMinor(), ModeDegreeEnum.Second, nameof(HarmonicMinor)) { } }
    public class HarmonicIII : Mode { public HarmonicIII() : base(new HarmonicMinor(), ModeDegreeEnum.Third, nameof(HarmonicMinor)) { } }
    public class HarmonicIV : Mode { public HarmonicIV() : base(new HarmonicMinor(), ModeDegreeEnum.Fourth, nameof(HarmonicMinor)) { } }
    public class HarmonicV : Mode { public HarmonicV() : base(new HarmonicMinor(), ModeDegreeEnum.Fifth, nameof(HarmonicMinor)) { } }
    public class HarmonicVI : Mode { public HarmonicVI() : base(new HarmonicMinor(), ModeDegreeEnum.Sixth, nameof(HarmonicMinor)) { } }
    public class HarmonicVII : Mode { public HarmonicVII() : base(new HarmonicMinor(), ModeDegreeEnum.Seventh, nameof(HarmonicMinor)) { } }

    public class JazzI : Mode { public JazzI() : base(new JazzMinor(), ModeDegreeEnum.Prime, nameof(JazzMinor)) { } }
    public class JazzII : Mode { public JazzII() : base(new JazzMinor(), ModeDegreeEnum.Second, nameof(JazzMinor)) { } }
    public class JazzIII : Mode { public JazzIII() : base(new JazzMinor(), ModeDegreeEnum.Third, nameof(JazzMinor)) { } }
    public class JazzIV : Mode { public JazzIV() : base(new JazzMinor(), ModeDegreeEnum.Fourth, nameof(JazzMinor)) { } }
    public class JazzV : Mode { public JazzV() : base(new JazzMinor(), ModeDegreeEnum.Fifth, nameof(JazzMinor)) { } }
    public class JazzVI : Mode { public JazzVI() : base(new JazzMinor(), ModeDegreeEnum.Sixth, nameof(JazzMinor)) { } }
    public class JazzVII : Mode { public JazzVII() : base(new JazzMinor(), ModeDegreeEnum.Seventh, nameof(JazzMinor)) { } }

    public class Diminished : Mode { public Diminished() : base(new Scales.Diminished(), ModeDegreeEnum.Prime, nameof(Diminished)) { } }
    public class Octatonic : Mode { public Octatonic() : base(new Scales.Diminished(), ModeDegreeEnum.Second, nameof(Octatonic)) { } }

    public class PentatonicMajor : Mode { public PentatonicMajor() : base(new Scales.PentaTonic(), ModeDegreeEnum.Prime, nameof(PentatonicMajor)) { } }
    public class PentatonicII : Mode { public PentatonicII() : base(new Scales.PentaTonic(), ModeDegreeEnum.Seventh, nameof(PentatonicII)) { } }
    public class PentatonicIII : Mode { public PentatonicIII() : base(new Scales.PentaTonic(), ModeDegreeEnum.Third, nameof(PentatonicIII)) { } }
    public class PentatonicIV : Mode { public PentatonicIV() : base(new Scales.PentaTonic(), ModeDegreeEnum.Fourth, nameof(PentatonicIV)) { } }
    public class PentatonicMinor : Mode { public PentatonicMinor() : base(new Scales.PentaTonic(), ModeDegreeEnum.Fifth, nameof(PentatonicMinor)) { } }

    public class Diminished6th : Mode { public Diminished6th() : base(new Scales.Diminished6th(), ModeDegreeEnum.Prime, nameof(Diminished6th)) { } }
    public class Diminished6thII : Mode { public Diminished6thII() : base(new Scales.Diminished6th(), ModeDegreeEnum.Second, nameof(Diminished6th)) { } }

    public class WholeTone : Mode { public WholeTone() : base(new Scales.WholeTone(), ModeDegreeEnum.Prime, nameof(WholeTone)) { } }

    public class Chromatic : Mode { public Chromatic() : base(new Scales.Chromatic(), ModeDegreeEnum.Prime, nameof(Chromatic)) { } }

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