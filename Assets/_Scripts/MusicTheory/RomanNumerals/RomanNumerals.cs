using MusicTheory.Scales;
using MusicTheory.ScaleDegrees;
using MusicTheory.Intervals;
using MusicTheory.Keys;


namespace MusicTheory.RomanNumerals
{
    [System.Serializable]
    public abstract class RomanNumeral : IMusicalElement
    {
        public RomanNumeral(RomanEnum @enum) => Enum = @enum;
        public RomanEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;
        public static explicit operator int(RomanNumeral r) => r.Enum.Id;
        public static explicit operator RomanNumeral(int i) => Enumeration.FindId<RomanEnum>(i);
    }

    public class I : RomanNumeral { public I() : base(RomanEnum.I) { } }
    public class bII : RomanNumeral { public bII() : base(RomanEnum.bII) { } }
    public class II : RomanNumeral { public II() : base(RomanEnum.II) { } }
    public class sII : RomanNumeral { public sII() : base(RomanEnum.sII) { } }
    public class bIII : RomanNumeral { public bIII() : base(RomanEnum.bIII) { } }
    public class III : RomanNumeral { public III() : base(RomanEnum.III) { } }
    public class IV : RomanNumeral { public IV() : base(RomanEnum.IV) { } }
    public class sIV : RomanNumeral { public sIV() : base(RomanEnum.sIV) { } }
    public class bV : RomanNumeral { public bV() : base(RomanEnum.bV) { } }
    public class V : RomanNumeral { public V() : base(RomanEnum.V) { } }
    public class sV : RomanNumeral { public sV() : base(RomanEnum.sV) { } }
    public class bVI : RomanNumeral { public bVI() : base(RomanEnum.bVI) { } }
    public class VI : RomanNumeral { public VI() : base(RomanEnum.VI) { } }
    public class dVII : RomanNumeral { public dVII() : base(RomanEnum.dVII) { } }
    public class bVII : RomanNumeral { public bVII() : base(RomanEnum.bVII) { } }
    public class VII : RomanNumeral { public VII() : base(RomanEnum.VII) { } }

    public class RomanEnum : Enumeration
    {
        public RomanEnum() : base(0, "") { }
        public RomanEnum(int id, string name) : base(id, name) { }

        public static RomanEnum I = new(0, nameof(I));
        public static RomanEnum bII = new(1, nameof(bII));
        public static RomanEnum II = new(2, nameof(II));
        public static RomanEnum sII = new(2, "♯II");
        public static RomanEnum bIII = new(3, nameof(bIII));
        public static RomanEnum III = new(4, nameof(III));
        public static RomanEnum IV = new(5, nameof(IV));
        public static RomanEnum sIV = new(5, "♯IV");
        public static RomanEnum bV = new(6, nameof(bV));
        public static RomanEnum V = new(7, nameof(V));
        public static RomanEnum sV = new(7, "♯V");
        public static RomanEnum bVI = new(8, nameof(bVI));
        public static RomanEnum VI = new(9, nameof(VI));
        public static RomanEnum dVII = new(9, "ºVII");
        public static RomanEnum bVII = new(10, nameof(bVII));
        public static RomanEnum VII = new(11, nameof(VII));

        public static explicit operator RomanEnum(int i) => FindId<RomanEnum>(i);
        public static implicit operator RomanNumeral(RomanEnum r) => r switch
        {
            _ when r == I => new I(),
            _ when r == bII => new bII(),
            _ when r == II => new II(),
            _ when r == sII => new sII(),
            _ when r == bIII => new bIII(),
            _ when r == III => new III(),
            _ when r == IV => new IV(),
            _ when r == sIV => new sIV(),
            _ when r == bV => new bV(),
            _ when r == V => new V(),
            _ when r == sV => new sV(),
            _ when r == bVI => new bVI(),
            _ when r == VI => new VI(),
            _ when r == dVII => new dVII(),
            _ when r == bVII => new bVII(),
            _ when r == VII => new VII(),
            _ => throw new System.ArgumentOutOfRangeException(r.Id.ToString())
        };
    }


}
