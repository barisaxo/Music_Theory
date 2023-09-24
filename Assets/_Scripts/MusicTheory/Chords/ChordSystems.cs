using MusicTheory.Scales;
using MusicTheory.Keys;
using MusicTheory.Intervals;
using MusicTheory.Chords;
using MusicTheory.Triads;
using MusicTheory.ScaleDegrees;

namespace MusicTheory
{
    public static class ChordSystems
    {
        public static Key Root(this Scale s, ScaleDegree currentScaleDegree, Key keyOf)
        {
            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval());
        }

        public static Key Third(this Scale s, ScaleDegree currentScaleDegree, Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < s.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(s.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree.GetInterval(s.ScaleDegrees[(x + 2) % s.ScaleDegrees.Length]));
        }

        public static Key Fifth(this Scale s, ScaleDegree currentScaleDegree, Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < s.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(s.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree
                .GetInterval(s.ScaleDegrees[(x + 4) % s.ScaleDegrees.Length]));
        }

        public static Key Seventh(this Scale s, ScaleDegree currentScaleDegree, Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < s.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(s.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree.GetInterval(s.ScaleDegrees[(x + 6) % s.ScaleDegrees.Length]));
        }

        public static Key Ninth(this Scale s, ScaleDegree currentScaleDegree, Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < s.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(s.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree.GetInterval(s.ScaleDegrees[(x + 1) % s.ScaleDegrees.Length]));
        }

        public static Key Eleventh(this Scale s, ScaleDegree currentScaleDegree, Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < s.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(s.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree
                .GetInterval(s.ScaleDegrees[(x + 3) % s.ScaleDegrees.Length]));
        }

        public static Key Thirteenth(this Scale s, ScaleDegree currentScaleDegree, Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < s.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(s.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree
                .GetInterval(s.ScaleDegrees[(x + 5) % s.ScaleDegrees.Length]));
        }


        public static Triad GetTriadQuality(this ScaleDegree root, ScaleDegree third, ScaleDegree fifth)
        {
            return (root.GetInterval(third), root.GetInterval(fifth)) switch
            {
                (Intervals.M3, Intervals.P5) => new Triads.Major(),
                (Intervals.mi3, Intervals.P5) => new Triads.Minor(),

                (Intervals.M3, Intervals.A5) => new Triads.Augmented(),
                (Intervals.M3, Intervals.mi6) => new Triads.Augmented(),
                (Intervals.d4, Intervals.mi6) => new Triads.Augmented(),
                (Intervals.d4, Intervals.A5) => new Triads.Augmented(),

                (Intervals.mi3, Intervals.d5) => new Triads.Diminished(),
                (Intervals.mi3, Intervals.A4) => new Triads.Diminished(),
                (Intervals.A2, Intervals.d5) => new Triads.Diminished(),
                (Intervals.A2, Intervals.A4) => new Triads.Diminished(),

                (Intervals.M2, Intervals.M3) => new Triads.Secundal(),
                (Intervals.M2, Intervals.d4) => new Triads.Secundal(),

                (Intervals.M3, Intervals.M6) => new Triads.Quartal(),
                (Intervals.P4, Intervals.mi7) => new Triads.Quartal(),
                (Intervals.P4, Intervals.M6) => new Triads.Quartal(),
                (Intervals.mi3, Intervals.P4) => new Triads.Quartal(),
                (Intervals.M2, Intervals.P4) => new Triads.Quartal(),
                _ => throw new System.ArgumentOutOfRangeException(root.Name + ", " + root.GetInterval(third) + ", " + third.Name + ", " + root.GetInterval(fifth) + ", " + fifth.Name)
            };
        }


        //public static Triad GetTriad(this (Interval third, Interval fifth) notes)
        //{
        //    return notes.fifth switch
        //    {
        //        Intervals.P5 => notes.third switch
        //        {
        //            Intervals.M3 => new Triads.Major(),
        //            Intervals.mi3 => new Triads.Minor(),
        //            _ => throw new System.ArgumentOutOfRangeException()
        //        },
        //        Intervals.TT => notes.third switch
        //        {
        //            Intervals.mi3 => new Triads.Diminished(),
        //            _ => throw new System.ArgumentOutOfRangeException()
        //        },
        //        Intervals.mi6 => notes.third switch
        //        {
        //            Intervals.M3 => new Triads.Augmented(),
        //            _ => throw new System.ArgumentOutOfRangeException()
        //        },
        //        _ => throw new System.ArgumentOutOfRangeException()
        //    };
        //}
    }


}