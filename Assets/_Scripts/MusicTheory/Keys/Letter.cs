namespace MusicTheory.Keys
{
    [System.Serializable]
    public abstract class Letter : IMusicalElement
    {
        public Letter(LetterEnum @enum) { Enum = @enum; }
        public readonly LetterEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;

        public override bool Equals(object obj) => obj is Letter e && Enum == e.Enum;
        public override int GetHashCode() => System.HashCode.Combine(Id, Name);
    }

    public class _C : Letter { public _C() : base(LetterEnum.C) { } }
    public class _D : Letter { public _D() : base(LetterEnum.D) { } }
    public class _E : Letter { public _E() : base(LetterEnum.E) { } }
    public class _F : Letter { public _F() : base(LetterEnum.F) { } }
    public class _G : Letter { public _G() : base(LetterEnum.G) { } }
    public class _A : Letter { public _A() : base(LetterEnum.A) { } }
    public class _B : Letter { public _B() : base(LetterEnum.B) { } }

    public class LetterEnum : Enumeration
    {
        public LetterEnum() : base(0, "") { }
        public LetterEnum(int id, string name) : base(id, name) { }

        public static LetterEnum A = new(0, nameof(A));
        public static LetterEnum B = new(1, nameof(B));
        public static LetterEnum C = new(2, nameof(C));
        public static LetterEnum D = new(3, nameof(D));
        public static LetterEnum E = new(4, nameof(E));
        public static LetterEnum F = new(5, nameof(F));
        public static LetterEnum G = new(6, nameof(G));

        public static explicit operator LetterEnum(int i) => FindId<LetterEnum>(i);
        public static implicit operator Letter(LetterEnum e) => e switch
        {
            _ when e == A => new _A(),
            _ when e == B => new _B(),
            _ when e == C => new _C(),
            _ when e == D => new _D(),
            _ when e == E => new _E(),
            _ when e == F => new _F(),
            _ when e == G => new _G(),
            _ => throw new System.ArgumentOutOfRangeException(e.ToString())
        };

    }
}