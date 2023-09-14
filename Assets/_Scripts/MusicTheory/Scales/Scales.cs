using System;
using MusicTheory.Scales;
using MusicTheory.ScaleDegrees;
using MusicTheory.Steps;

namespace MusicTheory.Scales
{
    public abstract class Scale
    {
        public Scale(ScaleEnum @enum, ModeDegreeEnum mode, Step[] steps, ScaleDegree[] degrees)
        {
            Enum = @enum;
            // Mode = mode;
            Steps = steps;
            ScaleDegrees = degrees;
        }

        public ScaleEnum Enum { get; protected set; }
        // public ModeDegree Mode { get; protected set; } = ModeDegreeEnum.Prime;
        public Step[] Steps { get; protected set; }
        public ScaleDegree[] ScaleDegrees { get; protected set; }
        public string Name => Enum.Name;
        public int Id => Enum.Id;
        public static Scale operator ++(Scale a) => (Scale)((a.Id + 1) % Enumeration.ListAll<ScaleEnum>().Count);
        // public static explicit operator Scale(Scale a, int i) => (Scale)(a.Id + i % Enumeration.ListAll<ScaleEnum>().Count);
        // public static int operator +(Scale a, int b) => a.Id + b % Enumeration.ListAll<ScaleEnum>().Count;
        // public static int operator -(Scale a, int b) => a.Id - b % Enumeration.ListAll<ScaleEnum>().Count;
        public static explicit operator Scale(int i) => Enumeration.FindId<ScaleEnum>(i);
    }

    #region SCALES
    public class Major : Scale
    {
        public Major() : base(
           @enum: ScaleEnum.Major,
           mode: ModeDegreeEnum.Prime,
           steps: new Step[] { new Whole(), new Whole(), new Half(), new Whole(), new Whole(), new Whole(), new Half() },
           degrees: new ScaleDegree[] { new _1(), new _2(), new _3(), new P4(), new P5(), new _6(), new _7() })
        { }
    }

    public class JazzMinor : Scale
    {
        public JazzMinor() : base(

            @enum: ScaleEnum.JazzMinor,
            mode: ModeDegreeEnum.Prime,
            steps: new Step[] { new Whole(), new Half(), new Whole(), new Whole(), new Whole(), new Whole(), new Half() },
            degrees: new ScaleDegree[] { new _1(), new _2(), new b3(), new P4(), new P5(), new _6(), new _7() })
        { }
    }

    public class HarmonicMinor : Scale
    {
        public HarmonicMinor() : base(
            @enum: ScaleEnum.HarmonicMinor,
            mode: ModeDegreeEnum.Prime,
            steps: new Step[] { new Whole(), new Half(), new Whole(), new Whole(), new Half(), new Augmented(), new Half() },
            degrees: new ScaleDegree[] { new _1(), new _2(), new b3(), new P4(), new P5(), new b6(), new _7() })
        { }
    }

    public class WholeTone : Scale
    {
        public WholeTone() : base(
            @enum: ScaleEnum.WholeTone,
            mode: ModeDegreeEnum.Prime,
            steps: new Step[] { new Whole(), new Whole(), new Whole(), new Whole(), new Whole(), new Whole() },
            degrees: new ScaleDegree[] { new _1(), new _2(), new _3(), new b5(), new b6(), new b7() })
        { }
    }

    public class OctaTonic : Scale
    {
        public OctaTonic() : base(
            @enum: ScaleEnum.OctaTonic,
            mode: ModeDegreeEnum.Prime,
            steps: new Step[] { new Whole(), new Half(), new Whole(), new Half(), new Whole(), new Half(), new Whole(), new Half() },
            degrees: new ScaleDegree[] { new _1(), new _2(), new b3(), new P4(), new b5(), new b6(), new _6(), new _7() })
        { }
    }

    public class Chromatic : Scale
    {
        public Chromatic() : base(
            @enum: ScaleEnum.Chromatic,
            mode: ModeDegreeEnum.Prime,
            steps: new[] { new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), },
            degrees: new ScaleDegree[] { new _1(), new b2(), new _2(), new b3(), new _3(), new P4(), new b5(), new P5(), new b6(), new _6(), new b7(), new _7() })
        { }
    }

    public class PentaTonic : Scale
    {
        public PentaTonic() : base(
            @enum: ScaleEnum.PentaTonic,
            mode: ModeDegreeEnum.Prime,
            steps: new Step[] { new Whole(), new Whole(), new Augmented(), new Whole(), new Augmented() },
            degrees: new ScaleDegree[] { new _1(), new _2(), new _3(), new P5(), new _6() })
        { }
    }
    #endregion SCALES

    public class ScaleEnum : Enumeration
    {
        public ScaleEnum() : base(0, "") { }
        public ScaleEnum(int id, string name) : base(id, name) { }

        public static ScaleEnum Major = new(0, nameof(Major));
        public static ScaleEnum JazzMinor = new(1, nameof(JazzMinor));
        public static ScaleEnum HarmonicMinor = new(2, nameof(HarmonicMinor));
        public static ScaleEnum WholeTone = new(3, nameof(WholeTone));
        public static ScaleEnum OctaTonic = new(4, nameof(OctaTonic));
        public static ScaleEnum Chromatic = new(5, nameof(Chromatic));
        public static ScaleEnum PentaTonic = new(6, nameof(PentaTonic));

        public static implicit operator Scale(ScaleEnum k) => k switch
        {
            _ when k == ScaleEnum.Major => new Major(),
            _ when k == ScaleEnum.JazzMinor => new JazzMinor(),
            _ when k == ScaleEnum.HarmonicMinor => new HarmonicMinor(),
            _ when k == ScaleEnum.OctaTonic => new OctaTonic(),
            _ when k == ScaleEnum.WholeTone => new WholeTone(),
            _ when k == ScaleEnum.Chromatic => new Chromatic(),
            _ when k == ScaleEnum.PentaTonic => new PentaTonic(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

}