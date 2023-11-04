
namespace MusicTheory.Steps
{
    [System.Serializable]
    public abstract class Step : IMusicalElement
    {
        public Step(StepEnum @enum) { Enum = @enum; }
        public readonly StepEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;

        //public static explicit operator Step(int i) => new((StepEnum)i);
        public static explicit operator StepEnum(Step i) => i.Enum;
        public static explicit operator int(Step i) => i.Enum.Id;
    }

    public class Half : Step { public Half() : base(StepEnum.Half) { } }
    public class Whole : Step { public Whole() : base(StepEnum.Whole) { } }
    public class Augmented : Step { public Augmented() : base(StepEnum.Augmented) { } }

    public class StepEnum : Enumeration
    {
        public StepEnum() : base(0, "") { }
        public StepEnum(int id, string name) : base(id, name) { }

        public static readonly StepEnum Half = new(1, nameof(Half));
        public static readonly StepEnum Whole = new(2, nameof(Whole));
        public static readonly StepEnum Augmented = new(3, nameof(Augmented));

        public static implicit operator Step(StepEnum e) => e switch
        {
            _ when e == Half => new Half(),
            _ when e == Whole => new Whole(),
            _ when e == Augmented => new Augmented(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        public static explicit operator StepEnum(int i) => FindId<StepEnum>(i);
    }


}