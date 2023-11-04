using System;
using MusicTheory.ScaleDegrees;

namespace MusicTheory.Keys
{
    [System.Serializable]
    public abstract class Key : IMusicalElement
    {
        public Key(KeyEnum key) { Enum = key; }
        public readonly KeyEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;

        public static int operator -(Key left, Key right) => left.Enum.Id - right.Enum.Id;
        public static int operator +(Key left, Key right) => left.Enum.Id + right.Enum.Id;
        public static bool operator ==(Key a, Key b) => a.Enum == b.Enum;
        public static bool operator !=(Key a, Key b) => a.Enum != b.Enum;
        public static bool operator ==(Key a, KeyEnum b) => a.Enum == b;
        public static bool operator !=(Key a, KeyEnum b) => a.Enum != b;
        public override bool Equals(object obj) => obj is Key e && e.Enum == Enum;
        public override int GetHashCode() => HashCode.Combine(Enum);
        public override string ToString() => Name;
    }

    public class C : Key { public C() : base(KeyEnum.C) { } }
    public class Cs : Key { public Cs() : base(KeyEnum.Cs) { } }
    public class Db : Key { public Db() : base(KeyEnum.Db) { } }
    public class D : Key { public D() : base(KeyEnum.D) { } }
    public class Ds : Key { public Ds() : base(KeyEnum.Ds) { } }
    public class Eb : Key { public Eb() : base(KeyEnum.Eb) { } }
    public class E : Key { public E() : base(KeyEnum.E) { } }
    public class Es : Key { public Es() : base(KeyEnum.Es) { } }
    public class Fb : Key { public Fb() : base(KeyEnum.Fb) { } }
    public class F : Key { public F() : base(KeyEnum.F) { } }
    public class Fs : Key { public Fs() : base(KeyEnum.Fs) { } }
    public class Gb : Key { public Gb() : base(KeyEnum.Gb) { } }
    public class G : Key { public G() : base(KeyEnum.G) { } }
    public class Gs : Key { public Gs() : base(KeyEnum.Gs) { } }
    public class Ab : Key { public Ab() : base(KeyEnum.Ab) { } }
    public class A : Key { public A() : base(KeyEnum.A) { } }
    public class As : Key { public As() : base(KeyEnum.As) { } }
    public class Bb : Key { public Bb() : base(KeyEnum.Bb) { } }
    public class B : Key { public B() : base(KeyEnum.B) { } }
    public class Bs : Key { public Bs() : base(KeyEnum.Bs) { } }
    public class Cb : Key { public Cb() : base(KeyEnum.Cb) { } }

    public class KeyEnum : Enumeration
    {
        public KeyEnum() : base(0, "") { }
        public KeyEnum(int id, string name, Letter letter, Accidental accidental) : base(id, name) { Letter = letter; Accidental = accidental; }
        public readonly Letter Letter;
        public readonly Accidental Accidental;

        public static implicit operator Key(KeyEnum key) => key switch
        {
            _ when key == C => new C(),
            _ when key == Cs => new Cs(),
            _ when key == Db => new Db(),
            _ when key == D => new D(),
            _ when key == Ds => new Ds(),
            _ when key == Eb => new Eb(),
            _ when key == E => new E(),
            _ when key == Es => new Es(),
            _ when key == Fb => new Fb(),
            _ when key == F => new F(),
            _ when key == Fs => new Fs(),
            _ when key == Gb => new Gb(),
            _ when key == G => new G(),
            _ when key == Gs => new Gs(),
            _ when key == Ab => new Ab(),
            _ when key == A => new A(),
            _ when key == As => new As(),
            _ when key == Bb => new Bb(),
            _ when key == B => new B(),
            _ when key == Bs => new Bs(),
            _ when key == Cb => new Cb(),
            _ => throw new ArgumentOutOfRangeException(key.Id.ToString())
        };

        public static KeyEnum C = new(0, nameof(C), new _C(), new Natural());
        public static KeyEnum Cs = new(1, "C♯", new _C(), new Sharp());
        public static KeyEnum Db = new(1, nameof(Db), new _D(), new Flat());
        public static KeyEnum D = new(2, nameof(D), new _D(), new Natural());
        public static KeyEnum Ds = new(3, "D♯", new _D(), new Sharp());
        public static KeyEnum Eb = new(3, nameof(Eb), new _E(), new Flat());
        public static KeyEnum E = new(4, nameof(E), new _E(), new Natural());
        public static KeyEnum Es = new(5, "E♯", new _E(), new Sharp());
        public static KeyEnum Fb = new(4, nameof(Fb), new _F(), new Flat());
        public static KeyEnum F = new(5, nameof(F), new _F(), new Natural());
        public static KeyEnum Fs = new(6, "F♯", new _F(), new Sharp());
        public static KeyEnum Gb = new(6, nameof(Gb), new _G(), new Flat());
        public static KeyEnum G = new(7, nameof(G), new _G(), new Natural());
        public static KeyEnum Gs = new(8, "G♯", new _G(), new Sharp());
        public static KeyEnum Ab = new(8, nameof(Ab), new _A(), new Flat());
        public static KeyEnum A = new(9, nameof(A), new _A(), new Natural());
        public static KeyEnum As = new(10, "A♯", new _A(), new Sharp());
        public static KeyEnum Bb = new(10, nameof(Bb), new _B(), new Flat());
        public static KeyEnum B = new(11, nameof(B), new _B(), new Natural());
        public static KeyEnum Bs = new(0, "B♯", new _B(), new Sharp());
        public static KeyEnum Cb = new(11, nameof(Cb), new _C(), new Flat());


        public static explicit operator KeyEnum((Letter letter, Accidental accidental) la) => Find(la);

        public static KeyEnum Find((Letter letter, Accidental accidental) la)
        {
            foreach (var e in All<KeyEnum>())
                if (e.Letter.Equals(la.letter) && e.Accidental.Equals(la.accidental))
                    return e;

            throw new ArgumentOutOfRangeException(la.ToString());
        }

    }

}