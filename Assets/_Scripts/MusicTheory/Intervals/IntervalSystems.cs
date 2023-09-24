using System;
using MusicTheory.Steps;
using MusicTheory.ScaleDegrees;

namespace MusicTheory.Intervals
{
    public static class IntervalSystems
    {

        //public static IntervalEnum GetInterval(this ScaleDegree s1, ScaleDegree s2)
        //{
        //    if ((int)s1 > 12 || (int)s2 > 12)
        //        throw new ArgumentOutOfRangeException(nameof(ScaleDegree), "Scale degree must not be greater than 12");
        //    int bottom = (int)s1;
        //    int top = (int)s2;
        //    if (bottom > top) top += 12;
        //    return (IntervalEnum)(top - bottom);
        //}

        //public static IntervalEnum GetInterval(this ScaleDegree i1, Step step)
        //{
        //    int bottom = (int)i1;
        //    int top = (int)step;
        //    return (IntervalEnum)(bottom + top > 12 ?
        //        throw new ArgumentOutOfRangeException(nameof(ScaleDegree), "Scale degree must not be greater than 12") :
        //        bottom + top);
        //}

        //public static Quantity GetQuantity(this ScaleDegree left, ScaleDegree right) =>
        //    (QuantityEnum)((int)right - (int)left < (int)right ? (int)left + 7 : (int)left);

        public static Quantity Invert(this Quantity quantity) => quantity switch
        {
            Unison => new Octave(),
            Second => new Seventh(),
            Third => new Sixth(),
            Fourth => new Fifth(),
            Fifth => new Fourth(),
            Sixth => new Third(),
            Seventh => new Second(),
            Octave => new Unison(),
            _ => throw new ArgumentOutOfRangeException(nameof(Quantity), quantity.ToString())
        };

        public static Quality Invert(this Quality quality) => quality switch
        {
            Major => new Minor(),
            Minor => new Major(),
            Augmented => new Diminished(),
            Diminished => new Augmented(),
            Perfect => quality,
            _ => throw new ArgumentOutOfRangeException(nameof(Quality), quality.ToString())
        };

        public static Interval Invert(this Interval i) => IntervalEnum.Find((i.Quality.Invert(), i.Quantity.Invert()));
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