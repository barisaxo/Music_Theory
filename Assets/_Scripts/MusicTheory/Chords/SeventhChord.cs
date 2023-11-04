using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicTheory.SeventhChords
{
    [System.Serializable]
    public abstract class SeventhChord : IMusicalElement
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
        public static SeventhChordEnum DominantSeventhSus = new(2, "7(sus)", nameof(DominantSeventhSus)) { };
        public static SeventhChordEnum MinorMajorSeventh = new(3, "-∆7", nameof(MinorMajorSeventh)) { };
        public static SeventhChordEnum HalfDiminishedSeventh = new(4, "ø7", nameof(HalfDiminishedSeventh)) { };
        public static SeventhChordEnum DiminishedSeventh = new(5, "º7", nameof(DiminishedSeventh)) { };

        public static implicit operator SeventhChord(SeventhChordEnum e) => e switch
        {
            _ when e == MajorSeventh => new MajorSeventh(),
            _ when e == MinorSeventh => new MinorSeventh(),
            _ when e == MinorMajorSeventh => new MinorMajorSeventh(),
            _ when e == DominantSeventh => new DominantSeventh(),
            _ when e == DominantSeventhSus => new DominantSeventhSus(),
            _ when e == HalfDiminishedSeventh => new HalfDiminishedSeventh(),
            _ when e == DiminishedSeventh => new DiminishedSeventh(),
            _ => throw new System.ArgumentOutOfRangeException(e.Id + " : " + e.ToString())
        };
    }

    public class MajorSeventh : SeventhChord { public MajorSeventh() : base(SeventhChordEnum.MajorSeventh) { } }
    public class MinorSeventh : SeventhChord { public MinorSeventh() : base(SeventhChordEnum.MinorSeventh) { } }
    public class DominantSeventh : SeventhChord { public DominantSeventh() : base(SeventhChordEnum.DominantSeventh) { } }
    public class DominantSeventhSus : SeventhChord { public DominantSeventhSus() : base(SeventhChordEnum.DominantSeventhSus) { } }
    public class MinorMajorSeventh : SeventhChord { public MinorMajorSeventh() : base(SeventhChordEnum.MinorMajorSeventh) { } }
    public class HalfDiminishedSeventh : SeventhChord { public HalfDiminishedSeventh() : base(SeventhChordEnum.HalfDiminishedSeventh) { } }
    public class DiminishedSeventh : SeventhChord { public DiminishedSeventh() : base(SeventhChordEnum.DiminishedSeventh) { } }

    public static class SeventhChordTones
    {
        public static Intervals.Interval[] ChordTonesAsIntervals(this SeventhChord chord)
        {
            Intervals.Interval[] temp = new Intervals.Interval[3];

            temp[0] = chord switch
            {
                MajorSeventh or DominantSeventh => new Intervals.M3(),
                MinorSeventh or MinorMajorSeventh or DiminishedSeventh or HalfDiminishedSeventh => new Intervals.mi3(),
                DominantSeventhSus => new Intervals.P4(),
                _ => throw new System.ArgumentOutOfRangeException(chord.Description)
            };

            temp[1] = chord switch
            {
                MajorSeventh or DominantSeventh or MinorSeventh or MinorMajorSeventh or DominantSeventhSus => new Intervals.P5(),
                DiminishedSeventh or HalfDiminishedSeventh => new Intervals.d5(),
                _ => throw new System.ArgumentOutOfRangeException(chord.Description)
            };

            temp[2] = chord switch
            {
                MajorSeventh or MinorMajorSeventh => new Intervals.M7(),
                MinorSeventh or DominantSeventh or DominantSeventhSus or HalfDiminishedSeventh => new Intervals.mi7(),
                DiminishedSeventh => new Intervals.d7(),
                _ => throw new System.ArgumentOutOfRangeException(chord.Description)
            };
            return temp;
        }
    }
}
