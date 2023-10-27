using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicTheory.SeventhChords
{
    public abstract class SeventhChord
    {
        public SeventhChord(SeventhChordEnum @enum) { Enum = @enum; }
        public readonly SeventhChordEnum Enum;
        public int Id => Enum.Id;
        public string Name => Enum.Name;
        public string Description => Enum.Description;
    }

    public class SeventhChordEnum : Enumeration
    {
        public SeventhChordEnum() : base(0, "") { }
        public SeventhChordEnum(int id, string name, string desc) : base(id, name) { Description = desc; }
        public readonly string Description;

        public static SeventhChordEnum MajorSeventh = new(0, "∆7", nameof(MajorSeventh)) { };
        public static SeventhChordEnum MinorSeventh = new(1, "-7", nameof(MinorSeventh)) { };
        public static SeventhChordEnum DominantSeventh = new(2, "7", nameof(DominantSeventh)) { };
        public static SeventhChordEnum MinorMajorSeventh = new(3, "-∆7", nameof(MinorMajorSeventh)) { };
        public static SeventhChordEnum HalfDiminishedSeventh = new(4, "ø7", nameof(HalfDiminishedSeventh)) { };
        public static SeventhChordEnum DiminishedSeventh = new(5, "º7", nameof(DiminishedSeventh)) { };

        public static implicit operator SeventhChord(SeventhChordEnum e) => e switch
        {
            _ when e == MajorSeventh => new MajorSeventh(),
            _ when e == MinorSeventh => new MinorSeventh(),
            _ when e == MinorMajorSeventh => new MinorMajorSeventh(),
            _ when e == DominantSeventh => new DominantSeventh(),
            _ when e == HalfDiminishedSeventh => new HalfDiminishedSeventh(),
            _ when e == DiminishedSeventh => new DiminishedSeventh(),
            _ => throw new System.ArgumentOutOfRangeException(e.Id + " : " + e.ToString())
        };
    }

    public class MajorSeventh : SeventhChord { public MajorSeventh() : base(SeventhChordEnum.MajorSeventh) { } }
    public class MinorSeventh : SeventhChord { public MinorSeventh() : base(SeventhChordEnum.MinorSeventh) { } }
    public class DominantSeventh : SeventhChord { public DominantSeventh() : base(SeventhChordEnum.DominantSeventh) { } }
    public class MinorMajorSeventh : SeventhChord { public MinorMajorSeventh() : base(SeventhChordEnum.MinorMajorSeventh) { } }
    public class HalfDiminishedSeventh : SeventhChord { public HalfDiminishedSeventh() : base(SeventhChordEnum.HalfDiminishedSeventh) { } }
    public class DiminishedSeventh : SeventhChord { public DiminishedSeventh() : base(SeventhChordEnum.DiminishedSeventh) { } }
}