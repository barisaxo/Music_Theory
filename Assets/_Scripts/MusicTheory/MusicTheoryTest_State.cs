using System;
using UnityEngine;
using MusicTheory.Keys;
using MusicTheory.Intervals;
using MusicTheory.ScaleDegrees;
using MusicTheory.Arithmetic;

public class MusicTheoryTest_State : State
{
    protected override void PrepareState(Action callback)
    {

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        //_ = new MusicTheory.Scales.Major();
        //TestAllIntervals();
        //TestScaleDegreeToInterval();
    }

    private void TestScaleDegreeToInterval()
    {
        Debug.Log(IntervalEnum.Find((new Perfect(), new Unison())));

        int numOfScaleDegrees = Enumeration.Length<ScaleDegreeEnum>();

        for (int i = 0; i < numOfScaleDegrees; i++)
        {
            ScaleDegree degree = Enumeration.All<ScaleDegreeEnum>()[i];
            Debug.Log(degree.Enum.Quality.Name + " " + degree.Enum.Degree.Name);
            Debug.Log(degree.AsInterval());
        }
    }

    private void TestAllIntervals()
    {
        int numOfKeys = Enumeration.Length<KeyEnum>();
        int numOfIntervals = Enumeration.Length<IntervalEnum>();

        for (int i = 0; i < numOfKeys; i++)
        {
            Key key = Enumeration.All<KeyEnum>()[i];

            for (int ii = 0; ii < numOfIntervals; ii++)
            {
                Interval interval = Enumeration.All<IntervalEnum>()[ii];

                Key newKey = key.GetKeyAbove(interval);

                Debug.Log("RESULT: " + key.Name + " +  " + interval.Name + " = " + newKey.Name);
            }
        }
    }

}
