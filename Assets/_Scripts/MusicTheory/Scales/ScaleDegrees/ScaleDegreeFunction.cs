namespace MusicTheory.ScaleDegrees
{
    [System.Serializable]
    public abstract class Function : IMusicalElement
    {
        public Function(FunctionEnum @enum) { Enum = @enum; }
        public readonly FunctionEnum Enum;
        public string Name => Enum.Name;
        public int Id => Enum.Id;
    }

    public class FunctionEnum : Enumeration
    {
        public FunctionEnum() : base(0, "") { }
        public FunctionEnum(int id, string name) : base(id, name) { }

        public static FunctionEnum Tonic = new(0, nameof(Tonic));
        public static FunctionEnum LateralSubDominantMinor = new(1, nameof(LateralSubDominantMinor));
        public static FunctionEnum LateralSubDominant = new(2, nameof(LateralSubDominant));
        public static FunctionEnum LateralSubDominantAugmented = new(3, nameof(LateralSubDominantAugmented));
        public static FunctionEnum MediantTonicMinor = new(3, nameof(MediantTonicMinor));
        public static FunctionEnum MediantTonic = new(4, nameof(MediantTonic));
        public static FunctionEnum SubDominantDiminished = new(4, nameof(SubDominantDiminished));
        public static FunctionEnum SubDominant = new(5, nameof(SubDominant));
        public static FunctionEnum SubDominantAugmented = new(6, nameof(SubDominantAugmented));
        public static FunctionEnum Tritone = new(6, nameof(Tritone));
        public static FunctionEnum DominantDiminished = new(6, nameof(DominantDiminished));
        public static FunctionEnum Dominant = new(7, nameof(Dominant));
        public static FunctionEnum DominantAugmented = new(8, nameof(DominantAugmented));
        public static FunctionEnum SubMediantTonicMinor = new(8, nameof(SubMediantTonicMinor));
        public static FunctionEnum SubMediantTonic = new(9, nameof(SubMediantTonic));
        public static FunctionEnum LateralDominantMinor = new(10, nameof(LateralDominantMinor));
        public static FunctionEnum LateralDominant = new(11, nameof(LateralDominant));
    }
}