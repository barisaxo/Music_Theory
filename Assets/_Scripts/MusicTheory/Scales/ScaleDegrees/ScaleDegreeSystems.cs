

namespace MusicTheory.Arithmetic
{
    public static class ScaleDegreeSystems
    {
        public static Intervals.Interval AsInterval(this ScaleDegrees.ScaleDegree scaleDegree) =>
            Intervals.IntervalEnum.Find(
                ((Intervals.QualityEnum)scaleDegree.Enum.Quality,
                (Intervals.QuantityEnum)scaleDegree.Enum.Degree)
                );

        public static Intervals.Interval AsInterval(this Steps.Step step) =>
         step switch
         {
             Steps.Half => new Intervals.mi2(),
             Steps.Whole => new Intervals.M2(),
             Steps.Augmented => new Intervals.A2(),

             _ => throw new System.ArgumentOutOfRangeException()
         };

        public static Intervals.Quantity GetQuantity(this ScaleDegrees.ScaleDegree left, ScaleDegrees.ScaleDegree right)
        {
            return ((right.Enum.Degree.Id + 7 - left.Enum.Degree.Id) % 7) switch
            {
                0 => Intervals.QuantityEnum.Unison,
                1 => Intervals.QuantityEnum.Second,
                2 => Intervals.QuantityEnum.Third,
                3 => Intervals.QuantityEnum.Fourth,
                4 => Intervals.QuantityEnum.Fifth,
                5 => Intervals.QuantityEnum.Sixth,
                6 => Intervals.QuantityEnum.Seventh,
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }

        public static Intervals.Quantity GetQuantity(this Keys.Key left, Keys.Key right)
        {
            //UnityEngine.Debug.Log(left.Name + " " + right.Name);
            return ((right.Enum.Letter.Id + 7 - left.Enum.Letter.Id) % 7) switch
            {
                0 => Intervals.QuantityEnum.Unison,
                1 => Intervals.QuantityEnum.Second,
                2 => Intervals.QuantityEnum.Third,
                3 => Intervals.QuantityEnum.Fourth,
                4 => Intervals.QuantityEnum.Fifth,
                5 => Intervals.QuantityEnum.Sixth,
                6 => Intervals.QuantityEnum.Seventh,
                _ => throw new System.ArgumentOutOfRangeException(left.Id.ToString() + " " + right.Id.ToString())
            };
        }

        public static Intervals.Interval GetInterval(this ScaleDegrees.ScaleDegree left, ScaleDegrees.ScaleDegree right)
        {
            Intervals.Interval newInterval = new Intervals.P1();
            Intervals.Quantity quantity = left.GetQuantity(right);
            int id = (right.Id + 12 - left.Id) % 12;

            foreach (var interval in Enumeration.All<Intervals.IntervalEnum>())
            {
                if (interval.Id.Equals(id) &&
                    System.MathF.Abs(interval.Quantity.Id - quantity.Id) <
                    System.MathF.Abs(newInterval.Quantity.Id - quantity.Id))
                    newInterval = interval;
            }

            return newInterval;
        }


        public static ScaleDegrees.ScaleDegreeEnum FindExactMatch(this (ScaleDegrees.DegreeEnum.QualityEnum quality, ScaleDegrees.DegreeEnum.DegreeEnum degree) i)
        {
            foreach (var e in Enumeration.All<ScaleDegrees.ScaleDegreeEnum>()) if (e.Degree.DegreeEnum == i.degree && e.Quality == i.quality) return e;
            throw new System.ArgumentOutOfRangeException(i.ToString());
        }

        public static RomanNumerals.RomanNumeral ToRoman(this ScaleDegrees.ScaleDegree scaleDegree) =>
            scaleDegree switch
            {
                ScaleDegrees._1 => new RomanNumerals.I(),
                ScaleDegrees._2 => new RomanNumerals.II(),
                ScaleDegrees._3 => new RomanNumerals.III(),
                ScaleDegrees.P4 => new RomanNumerals.IV(),
                ScaleDegrees.P5 => new RomanNumerals.V(),
                ScaleDegrees._6 => new RomanNumerals.VI(),
                ScaleDegrees._7 => new RomanNumerals.VII(),
                ScaleDegrees.b2 => new RomanNumerals.bII(),
                ScaleDegrees.b3 => new RomanNumerals.bIII(),
                ScaleDegrees.b4 => new RomanNumerals.III(),
                ScaleDegrees.b5 => new RomanNumerals.bV(),
                ScaleDegrees.b6 => new RomanNumerals.bVI(),
                ScaleDegrees.d7 => new RomanNumerals.dVII(),
                ScaleDegrees.b7 => new RomanNumerals.bVII(),
                ScaleDegrees.s2 => new RomanNumerals.sII(),
                ScaleDegrees.s4 => new RomanNumerals.sIV(),
                ScaleDegrees.s5 => new RomanNumerals.sV(),
                _ => throw new System.ArgumentOutOfRangeException(scaleDegree.Name)
            };

        public static Triads.Triad GetTriadQuality(this ScaleDegrees.ScaleDegree root, ScaleDegrees.ScaleDegree third, ScaleDegrees.ScaleDegree fifth)
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

                //(Intervals.M2, Intervals.M3) => new Triads.Secundal(),
                //(Intervals.M2, Intervals.d4) => new Triads.Secundal(),

                //(Intervals.M3, Intervals.M6) => new Triads.Quartal(),
                //(Intervals.P4, Intervals.mi7) => new Triads.Quartal(),
                //(Intervals.P4, Intervals.M6) => new Triads.Quartal(),
                //(Intervals.mi3, Intervals.P4) => new Triads.Quartal(),
                //(Intervals.M2, Intervals.P4) => new Triads.Quartal(),
                _ => throw new System.ArgumentOutOfRangeException(root.Name + ", " + root.GetInterval(third) + ", " + third.Name + ", " + root.GetInterval(fifth) + ", " + fifth.Name)
            };
        }

    }
}