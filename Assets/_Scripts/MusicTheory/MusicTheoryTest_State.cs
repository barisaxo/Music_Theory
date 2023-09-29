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

        int numOfScaleDegrees = Enumeration.ListAll<ScaleDegreeEnum>().Count;

        for (int i = 0; i < numOfScaleDegrees; i++)
        {
            ScaleDegree degree = Enumeration.ListAll<ScaleDegreeEnum>()[i];
            Debug.Log(degree.Enum.Quality.Name + " " + degree.Enum.Degree.Name);
            Debug.Log(degree.AsInterval());
        }
    }

    private void TestAllIntervals()
    {
        int numOfKeys = Enumeration.ListAll<KeyEnum>().Count;
        int numOfIntervals = Enumeration.ListAll<IntervalEnum>().Count;

        for (int i = 0; i < numOfKeys; i++)
        {
            Key key = Enumeration.ListAll<KeyEnum>()[i];

            for (int ii = 0; ii < numOfIntervals; ii++)
            {
                Interval interval = Enumeration.ListAll<IntervalEnum>()[ii];

                Key newKey = key.GetKeyAbove(interval);

                Debug.Log("RESULT: " + key.Name + " +  " + interval.Name + " = " + newKey.Name);
            }
        }
    }

}
