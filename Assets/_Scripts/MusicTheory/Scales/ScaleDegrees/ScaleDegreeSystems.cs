//using MusicTheory.Intervals;

namespace MusicTheory.ScaleDegrees
{
    public static class ScaleDegreeSystems
    {
        public static Intervals.Interval AsInterval(this ScaleDegree scaleDegree) =>
            Intervals.IntervalEnum.Find(
                ((Intervals.QualityEnum)scaleDegree.Enum.Quality,
                (Intervals.QuantityEnum)scaleDegree.Enum.Degree)
                );



        public static Intervals.Quantity GetQuantity(this ScaleDegree left, ScaleDegree right)
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

        public static Intervals.Interval GetInterval(this ScaleDegree left, ScaleDegree right)
        {
            Intervals.Quantity quantity = left.GetQuantity(right);
            int id = (right.Id + 12 - left.Id) % 12;
            UnityEngine.Debug.Log(left.Id + " - " + right.Id + " = " + id + " : " + quantity.Name);

            if ((quantity is Intervals.Third || quantity is Intervals.Unison) && id == 2) quantity = new Intervals.Second();

            foreach (var interval in Enumeration.ListAll<Intervals.IntervalEnum>())
            {
                if (interval.Id.Equals(id) && interval.Quantity.Equals(quantity)) return interval;
            }
            throw new System.ArgumentOutOfRangeException(left.Name + ", " + right.Name);
        }

        public static RomanNumerals.RomanNumeral ToRoman(this ScaleDegree scaleDegree) =>
            scaleDegree switch
            {
                _1 => new RomanNumerals.I(),
                _2 => new RomanNumerals.II(),
                _3 => new RomanNumerals.III(),
                P4 => new RomanNumerals.IV(),
                P5 => new RomanNumerals.V(),
                _6 => new RomanNumerals.VI(),
                _7 => new RomanNumerals.VII(),
                b2 => new RomanNumerals.bII(),
                b3 => new RomanNumerals.bIII(),
                b4 => new RomanNumerals.III(),
                b5 => new RomanNumerals.bV(),
                b6 => new RomanNumerals.bVI(),
                d7 => new RomanNumerals.dVII(),
                b7 => new RomanNumerals.bVII(),
                s2 => new RomanNumerals.sII(),
                s4 => new RomanNumerals.sIV(),
                s5 => new RomanNumerals.sV(),
                _ => throw new System.ArgumentOutOfRangeException(scaleDegree.Name)
            };
    }
}