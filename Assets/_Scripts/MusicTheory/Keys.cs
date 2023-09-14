using System;
using MusicTheory.Chords;
using MusicTheory.ScaleDegrees;

namespace MusicTheory.Keys
{
    public abstract class Key
    {
        public Key(KeyEnum key) { Enum = key; }
        public readonly KeyEnum Enum;
        public string Name => Enum.Name;

        public static int operator -(Key left, Key right) => left.Enum.Id - right.Enum.Id;
        public static int operator +(Key left, Key right) => left.Enum.Id + right.Enum.Id;
        public static bool operator ==(Key a, Key b) => a.Enum == b.Enum;
        public static bool operator !=(Key a, Key b) => a.Enum != b.Enum;
        public static bool operator ==(Key a, KeyEnum b) => a.Enum == b;
        public static bool operator !=(Key a, KeyEnum b) => a.Enum != b;
        public override bool Equals(object obj) => obj is Key e && Enum == e.Enum;
        public override int GetHashCode() => HashCode.Combine(Enum);
        public static implicit operator KeyEnum(Key key) => key.Enum;
        public static explicit operator int(Key key) => key.Enum.Id;
        public static explicit operator Key(int i) => Enumeration.FindId<KeyEnum>(i);
        public static explicit operator Key(ScaleDegree s) => (Key)(int)s;
        public override string ToString() => Name;
    }

    public class C : Key
    {
        public C() : base(KeyEnum.C) { }
        public static int operator +(C Left, int Right) => Left.Enum.Id + Right;
        public static int operator +(int Left, C Right) => Left + KeyEnum.C.Id;
    }
    public class Db : Key { public Db() : base(KeyEnum.Db) { } }
    public class D : Key { public D() : base(KeyEnum.D) { } }
    public class Eb : Key { public Eb() : base(KeyEnum.Eb) { } }
    public class E : Key { public E() : base(KeyEnum.E) { } }
    public class F : Key { public F() : base(KeyEnum.F) { } }
    public class Gb : Key { public Gb() : base(KeyEnum.Gb) { } }
    public class G : Key { public G() : base(KeyEnum.G) { } }
    public class Ab : Key { public Ab() : base(KeyEnum.Ab) { } }
    public class A : Key { public A() : base(KeyEnum.A) { } }
    public class Bb : Key { public Bb() : base(KeyEnum.Bb) { } }
    public class B : Key { public B() : base(KeyEnum.B) { } }

    public class KeyEnum : Enumeration
    {
        public KeyEnum() : base(0, "") { }
        public KeyEnum(int id, string name) : base(id, name) { }

        public static implicit operator Key(KeyEnum k) => k switch
        {
            _ when k == KeyEnum.C => new C(),
            _ when k == KeyEnum.Db => new Db(),
            _ when k == KeyEnum.D => new D(),
            _ when k == KeyEnum.Eb => new Eb(),
            _ when k == KeyEnum.E => new E(),
            _ when k == KeyEnum.F => new F(),
            _ when k == KeyEnum.Gb => new Gb(),
            _ when k == KeyEnum.G => new G(),
            _ when k == KeyEnum.Ab => new Ab(),
            _ when k == KeyEnum.A => new A(),
            _ when k == KeyEnum.Bb => new Bb(),
            _ when k == KeyEnum.B => new B(),
            _ => throw new ArgumentOutOfRangeException(k.Id.ToString())
        };

        public static readonly KeyEnum C = new(0, nameof(C));
        public static readonly KeyEnum Db = new(1, nameof(Db));
        public static readonly KeyEnum D = new(2, nameof(D));
        public static readonly KeyEnum Eb = new(3, nameof(Eb));
        public static readonly KeyEnum E = new(4, nameof(E));
        public static readonly KeyEnum F = new(5, nameof(F));
        public static readonly KeyEnum Gb = new(6, nameof(Gb));
        public static readonly KeyEnum G = new(7, nameof(G));
        public static readonly KeyEnum Ab = new(8, nameof(Ab));
        public static readonly KeyEnum A = new(9, nameof(A));
        public static readonly KeyEnum Bb = new(10, nameof(Bb));
        public static readonly KeyEnum B = new(11, nameof(B));
    }

    public static class KeySystems
    {
        public static string EnharmonicNoteName(this Key note, Key key)
        {
            switch (note)
            {
                case Gb: switch (key) { case A: case B: case D: case E: case G: return "F♯"; } break;
                case Db: switch (key) { case A: case B: case D: case E: return "C♯"; } break;
                case Eb: switch (key) { case B: case E: return "D♯"; } break;
                case Ab: switch (key) { case B: case E: return "G♯"; } break;
                case Bb: switch (key) { case B: return "A♯"; } break;
                case B: switch (key) { case Gb: return "Cb"; } break;
            }
            return note.ToString();
        }

        public static Key InverselyTransposed(this Key key, Key tKey) => (Key)((tKey - key) < 0 ? tKey - key + 12 : tKey - key);
    }


}



