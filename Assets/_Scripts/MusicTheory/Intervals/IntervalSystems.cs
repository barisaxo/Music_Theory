using System;
using MusicTheory.Steps;
using MusicTheory.ScaleDegrees;

namespace MusicTheory.Arithmetic
{
    public static class IntervalSystems
    {
        public static ScaleDegree AsScaleDegree(this Intervals.Interval i)
        {
            ScaleDegrees.DegreeEnum.QualityEnum quality = (ScaleDegrees.DegreeEnum.QualityEnum)i.Quality.Enum;
            ScaleDegrees.DegreeEnum.DegreeEnum degree = (ScaleDegrees.DegreeEnum.DegreeEnum)i.Quantity.Enum;
            return (quality, degree).FindExactMatch();
        }

        public static Intervals.Quantity Invert(this Intervals.Quantity quantity) => quantity switch
        {
            Intervals.Unison => new Intervals.Octave(),
            Intervals.Second => new Intervals.Seventh(),
            Intervals.Third => new Intervals.Sixth(),
            Intervals.Fourth => new Intervals.Fifth(),
            Intervals.Fifth => new Intervals.Fourth(),
            Intervals.Sixth => new Intervals.Third(),
            Intervals.Seventh => new Intervals.Second(),
            Intervals.Octave => new Intervals.Unison(),
            _ => throw new ArgumentOutOfRangeException(nameof(Intervals.Quantity), quantity.ToString())
        };

        public static Intervals.Quality Invert(this Intervals.Quality quality) => quality switch
        {
            Intervals.Major => new Intervals.Minor(),
            Intervals.Minor => new Intervals.Major(),
            Intervals.Augmented => new Intervals.Diminished(),
            Intervals.Diminished => new Intervals.Augmented(),
            Intervals.Perfect => quality,
            _ => throw new ArgumentOutOfRangeException(nameof(Intervals.Quality), quality.ToString())
        };

        public static Intervals.Interval Invert(this Intervals.Interval i) => Intervals.IntervalEnum.Find((i.Quality.Invert(), i.Quantity.Invert()));

        public static RomanNumerals.RomanNumeral ToRoman(this Intervals.Interval interval) =>
       interval switch
       {
           Intervals.P1 => new RomanNumerals.I(),
           Intervals.M2 => new RomanNumerals.II(),
           Intervals.M3 => new RomanNumerals.III(),
           Intervals.P4 => new RomanNumerals.IV(),
           Intervals.P5 => new RomanNumerals.V(),
           Intervals.M6 => new RomanNumerals.VI(),
           Intervals.M7 => new RomanNumerals.VII(),
           Intervals.mi2 => new RomanNumerals.bII(),
           Intervals.mi3 => new RomanNumerals.bIII(),
           Intervals.d4 => new RomanNumerals.III(),
           Intervals.d5 => new RomanNumerals.bV(),
           Intervals.mi6 => new RomanNumerals.bVI(),
           Intervals.d7 => new RomanNumerals.dVII(),
           Intervals.mi7 => new RomanNumerals.bVII(),
           Intervals.A2 => new RomanNumerals.sII(),
           Intervals.A4 => new RomanNumerals.sIV(),
           Intervals.A5 => new RomanNumerals.sV(),
           _ => throw new System.ArgumentOutOfRangeException(interval.Name)
       };
    }
}



//public static Interval GetInterval(int i) => ((IntervalEnum)i).ToClass();

//public static Interval ToClass(this IntervalEnum ie) =>
//    (int)ie > 12 ? throw new ArgumentOutOfRangeException(nameof(IntervalEnum), "Interval must not be greater than 12") : new Interval(ie);

//public static Interval Invert(this Interval i) => i switch
//{
//    P1 => new P8(),
//    mi2 => new M7(),
//    M2 => new mi7(),
//    mi3 => new M6(),
//    M3 => new mi6(),
//    P4 => new P5(),
//    TT => i,
//    P5 => new P4(),
//    mi6 => new M3(),
//    M6 => new mi3(),
//    mi7 => new M2(),
//    M7 => new mi2(),
//    P8 => new P1(),
//    _ => throw new ArgumentOutOfRangeException(nameof(IntervalEnum), "Interval must not be greater than 12")
//};