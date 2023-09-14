using MusicTheory.Scales;
using MusicTheory.ScaleDegrees;
using MusicTheory.Intervals;
using MusicTheory.Keys;


namespace MusicTheory.RomanNumerals
{
    public class RomanNumeral
    {
        public RomanNumeral(RomanEnum @enum) => Enum = @enum;
        public RomanEnum Enum;
        public string Name => Enum.Name;
        public static explicit operator RomanNumeral(ScaleDegree s) => new((RomanEnum)s);
        public static explicit operator int(RomanNumeral r) => r.Enum.Id;
        public static explicit operator RomanNumeral(int i) => Enumeration.FindId<RomanEnum>(i % 12);
    }

    public class I : RomanNumeral { public I() : base(RomanEnum.I) { } }
    public class bII : RomanNumeral { public bII() : base(RomanEnum.bII) { } }
    public class II : RomanNumeral { public II() : base(RomanEnum.II) { } }
    public class bIII : RomanNumeral { public bIII() : base(RomanEnum.bIII) { } }
    public class III : RomanNumeral { public III() : base(RomanEnum.III) { } }
    public class IV : RomanNumeral { public IV() : base(RomanEnum.IV) { } }
    public class bV : RomanNumeral { public bV() : base(RomanEnum.bV) { } }
    public class V : RomanNumeral { public V() : base(RomanEnum.V) { } }
    public class bVI : RomanNumeral { public bVI() : base(RomanEnum.bVI) { } }
    public class VI : RomanNumeral { public VI() : base(RomanEnum.VI) { } }
    public class bVII : RomanNumeral { public bVII() : base(RomanEnum.bVII) { } }
    public class VII : RomanNumeral { public VII() : base(RomanEnum.VII) { } }

    public class RomanEnum : Enumeration
    {
        public RomanEnum() : base(0, "") { }
        public RomanEnum(int id, string name) : base(id, name) { }

        public static RomanEnum I = new(0, nameof(I));
        public static RomanEnum bII = new(1, nameof(bII));
        public static RomanEnum II = new(2, nameof(II));
        public static RomanEnum bIII = new(3, nameof(bIII));
        public static RomanEnum III = new(4, nameof(III));
        public static RomanEnum IV = new(5, nameof(IV));
        public static RomanEnum bV = new(6, nameof(bV));
        public static RomanEnum V = new(7, nameof(V));
        public static RomanEnum bVI = new(8, nameof(bVI));
        public static RomanEnum VI = new(9, nameof(VI));
        public static RomanEnum bVII = new(10, nameof(bVII));
        public static RomanEnum VII = new(11, nameof(VII));

        public static explicit operator RomanEnum(ScaleDegree s) => FindId<RomanEnum>((int)s);
        public static explicit operator RomanEnum(int i) => FindId<RomanEnum>(i);
        public static implicit operator RomanNumeral(RomanEnum r) => r switch
        {
            _ when r == RomanEnum.I => new I(),
            _ when r == RomanEnum.bII => new bII(),
            _ when r == RomanEnum.II => new II(),
            _ when r == RomanEnum.bIII => new bIII(),
            _ when r == RomanEnum.III => new III(),
            _ when r == RomanEnum.IV => new IV(),
            _ when r == RomanEnum.bV => new bV(),
            _ when r == RomanEnum.V => new V(),
            _ when r == RomanEnum.bVI => new bVI(),
            _ when r == RomanEnum.VI => new VI(),
            _ when r == RomanEnum.bVII => new bVII(),
            _ when r == RomanEnum.VII => new VII(),
            _ => throw new System.ArgumentOutOfRangeException(r.Id.ToString())
        };
    }

    public static class RomanConventions
    {
        public static RomanNumeral[] DiatonicRomans(this RomanNumeral _) => new RomanNumeral[] { new I(), new II(), new III(), new IV(), new V(), new VI(), new VII() };

        public static RomanNumeral ToRoman(this ScaleDegree s) => (RomanNumeral)s;

        public static Key GetChordTone(this Scale s, int currentScaleDegree, ChordTone c, Key k)
        {
            UnityEngine.Debug.Log((Key)s.ScaleDegrees[(currentScaleDegree + (int)c) % s.ScaleDegrees.Length] + " " + k.Name);
            return ((Key)s.ScaleDegrees[(currentScaleDegree + (int)c) % s.ScaleDegrees.Length]).InverselyTransposed(k);
        }
    }

    public enum ChordTone { Root, Third, Fifth, Seventh, Ninth, Eleventh, Thirteenth }

}
