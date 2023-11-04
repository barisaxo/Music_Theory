using System;
using MusicTheory.ScaleDegrees;
using MusicTheory.Steps;
using MusicTheory.Modes;

namespace MusicTheory.Scales
{
    [Serializable]
    public class Scale : IMusicalElement
    {
        public Scale(ScaleEnum @enum, Mode[] modes, Step[] steps, ScaleDegree[] degrees)
        {
            Enum = @enum;
            Modes = modes;
            Steps = steps;
            ScaleDegrees = degrees;
        }

        public ScaleEnum Enum { get; protected set; }
        public Mode[] Modes { get; protected set; }
        public Step[] Steps { get; protected set; }
        public ScaleDegree[] ScaleDegrees { get; protected set; }

        public string Description => Enum.Description;
        public string Name => Enum.Name;
        public int Id => Enum.Id;
        public static Scale operator ++(Scale a) => (Scale)((a.Id + 1) % Enumeration.Length<ScaleEnum>());
        public static explicit operator Scale(int i) => Enumeration.FindId<ScaleEnum>(i);
    }

    #region SCALES

    public class Major : Scale
    {
        public Major() : base(
           @enum: ScaleEnum.Major,
           modes: new Mode[] { new Ionian(), new Dorian(), new Phrygian(), new Lydian(), new MixoLydian(), new Aeolian(), new Locrian() },
           steps: new Step[] { new Whole(), new Whole(), new Half(), new Whole(), new Whole(), new Whole(), new Half() },
           degrees: new ScaleDegree[] { new _1(), new _2(), new _3(), new P4(), new P5(), new _6(), new _7(), })
        { }
    }

    public class JazzMinor : Scale
    {
        public JazzMinor() : base(

            @enum: ScaleEnum.JazzMinor,
            modes: new Mode[] { new JazzI(), new JazzII(), new JazzIII(), new JazzIV(), new JazzV(), new JazzVI(), new JazzVII() },
            steps: new Step[] { new Whole(), new Half(), new Whole(), new Whole(), new Whole(), new Whole(), new Half() },
            degrees: new ScaleDegree[] { new _1(), new _2(), new b3(), new P4(), new P5(), new _6(), new _7() })
        { }
    }

    public class HarmonicMinor : Scale
    {
        public HarmonicMinor() : base(
            @enum: ScaleEnum.HarmonicMinor,
            modes: new Mode[] { new HarmonicI(), new HarmonicII(), new HarmonicIII(), new HarmonicIV(), new HarmonicV(), new HarmonicVI(), new HarmonicVII() },
            steps: new Step[] { new Whole(), new Half(), new Whole(), new Whole(), new Half(), new Augmented(), new Half() },
            degrees: new ScaleDegree[] { new _1(), new _2(), new b3(), new P4(), new P5(), new b6(), new _7() })
        { }
    }

    public class WholeTone : Scale
    {
        public WholeTone() : base(
            @enum: ScaleEnum.WholeTone,
            modes: new Mode[] { new Modes.WholeTone() },
            steps: new Step[] { new Whole(), new Whole(), new Whole(), new Whole(), new Whole(), new Whole() },
            degrees: new ScaleDegree[] { new _1(), new _2(), new _3(), new s4(), new s5(), new b7() })
        { }
    }

    public class Diminished : Scale
    {
        public Diminished() : base(
            @enum: ScaleEnum.Diminished,
            modes: new Mode[] { new Modes.Diminished(), new Octatonic() },
            steps: new Step[] { new Whole(), new Half(), new Whole(), new Half(), new Whole(), new Half(), new Whole(), new Half() },
            degrees: new ScaleDegree[] { new _1(), new _2(), new b3(), new P4(), new b5(), new b6(), new _6(), new _7() })
        { }
    }

    public class Diminished6th : Scale
    {
        public Diminished6th() : base(
           @enum: ScaleEnum.Diminished6th,
           modes: new Mode[] { new Modes.Diminished6thI(), new Diminished6thII() },
           steps: new Step[] { new Whole(), new Whole(), new Half(), new Whole(), new Half(), new Half(), new Whole(), new Half() },
           degrees: new ScaleDegree[] { new _1(), new _2(), new _3(), new P4(), new P5(), new b6(), new _6(), new _7(), })
        { }
    }

    public class Chromatic : Scale
    {
        public Chromatic() : base(
            @enum: ScaleEnum.Chromatic,
            modes: new Mode[] { new Modes.Chromatic() },
            steps: new[] { new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), },
            degrees: new ScaleDegree[] { new _1(), new b2(), new _2(), new b3(), new _3(), new P4(), new b5(), new P5(), new b6(), new _6(), new b7(), new _7() })
        { }
    }

    public class Pentatonic : Scale
    {
        public Pentatonic() : base(
            @enum: ScaleEnum.Pentatonic,
            modes: new Mode[] { new PentatonicMajor(), new PentatonicII(), new PentatonicIII(), new PentatonicIV(), new PentatonicMinor() },
            steps: new Step[] { new Whole(), new Whole(), new Augmented(), new Whole(), new Augmented() },
            degrees: new ScaleDegree[] { new _1(), new _2(), new _3(), new P5(), new _6() })
        { }
    }

    #endregion SCALES

    public class ScaleEnum : Enumeration
    {
        public ScaleEnum() : base(0, "") { }
        public ScaleEnum(int id, string name, string desc) : base(id, name) { Description = desc; }
        public readonly string Description;
        //public static int Count => ListAll<ScaleEnum>().Count;

        public static ScaleEnum Major = new(0, "∆", nameof(Major));
        public static ScaleEnum JazzMinor = new(1, "∆-", nameof(JazzMinor));
        public static ScaleEnum HarmonicMinor = new(2, "∆±", nameof(HarmonicMinor));
        public static ScaleEnum WholeTone = new(3, "+", nameof(WholeTone));
        public static ScaleEnum Diminished = new(4, "º", nameof(Diminished));
        public static ScaleEnum Diminished6th = new(5, "º6", nameof(Diminished6th));
        public static ScaleEnum Chromatic = new(6, "∞", nameof(Chromatic));
        public static ScaleEnum Pentatonic = new(7, "ε", nameof(Pentatonic));

        public static implicit operator Scale(ScaleEnum s) => s switch
        {
            _ when s == Major => new Major(),
            _ when s == JazzMinor => new JazzMinor(),
            _ when s == HarmonicMinor => new HarmonicMinor(),
            _ when s == Diminished => new Diminished(),
            _ when s == WholeTone => new WholeTone(),
            _ when s == Diminished6th => new Diminished6th(),
            _ when s == Chromatic => new Chromatic(),
            _ when s == Pentatonic => new Pentatonic(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

}