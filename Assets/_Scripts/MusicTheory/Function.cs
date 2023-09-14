namespace MusicTheory.Functions
{
    public class Function
    {




    }


    public class FunctionEnum : Enumeration
    {
        public FunctionEnum() : base(0, "") { }

    }


    public class ScaleDegreeFunctionEnum : Enumeration
    {
        public ScaleDegreeFunctionEnum() : base(0, "") { }
        public ScaleDegreeFunctionEnum(int id, string name) : base(id, name) { }


        public static ScaleDegreeFunctionEnum Tonic = new(0, nameof(Tonic));
        public static ScaleDegreeFunctionEnum LateralSubDominantMinor = new(0, nameof(LateralSubDominantMinor));
        public static ScaleDegreeFunctionEnum LateralSubDominant = new(0, nameof(LateralSubDominant));
        public static ScaleDegreeFunctionEnum LateralSubDominantAugmented = new(0, nameof(LateralSubDominantAugmented));
        public static ScaleDegreeFunctionEnum MediantTonicMinor = new(0, nameof(MediantTonicMinor));
        public static ScaleDegreeFunctionEnum MediantTonic = new(0, nameof(MediantTonic));
        public static ScaleDegreeFunctionEnum SubDominantDiminished = new(0, nameof(SubDominantDiminished));
        public static ScaleDegreeFunctionEnum SubDominant = new(0, nameof(SubDominant));
        public static ScaleDegreeFunctionEnum SubDominantAugmented = new(0, nameof(SubDominantAugmented));
        public static ScaleDegreeFunctionEnum Tritone = new(0, nameof(Tritone));
        public static ScaleDegreeFunctionEnum DominantDiminished = new(0, nameof(DominantDiminished));
        public static ScaleDegreeFunctionEnum Dominant = new(0, nameof(Dominant));
        public static ScaleDegreeFunctionEnum DominantAugmented = new(0, nameof(DominantAugmented));
        public static ScaleDegreeFunctionEnum SubMediantTonicMinor = new(0, nameof(SubMediantTonicMinor));
        public static ScaleDegreeFunctionEnum SubMediantTonic = new(0, nameof(SubMediantTonic));
        public static ScaleDegreeFunctionEnum LateralDominantMinor = new(0, nameof(LateralDominantMinor));
        public static ScaleDegreeFunctionEnum LateralDominant = new(0, nameof(LateralDominant));


    }

}