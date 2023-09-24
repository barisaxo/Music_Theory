using System;
using MusicTheory.ScaleDegrees;
using MusicTheory.Steps;
using MusicTheory.Keys;
using MusicTheory.Modes;

namespace MusicTheory.Scales
{
    public static class ScaleSystems
    {
        public static Step[] ShiftSteps(this Scale scale, ModeDegreeEnum mode)
        {
            Step[] newSteps = new Step[scale.Steps.Length];

            for (int i = (int)mode; i < (int)mode + newSteps.Length; i++)
            {
                newSteps[i] = scale.Steps[i < newSteps.Length ? i : i - newSteps.Length];
            }

            return newSteps;
        }

        //public static ScaleDegree[] ShiftDegrees(this Scale scale, ModeDegreeEnum mode)
        //{
        //    Step[] newSteps = scale.ShiftSteps(mode);

        //    ScaleDegree[] newDegrees = new ScaleDegree[scale.ScaleDegrees.Length];
        //    newDegrees[0] = new _1();

        //    for (int i = 1; i < newDegrees.Length; i++)
        //    {
        //        newDegrees[i] = scale.ScaleDegrees[i].StepUp(newSteps[i - 1]);
        //    }

        //    return newDegrees;
        //}

        //public static Key Above(this ScaleDegree s, Key key)
        //{
        //    //switch (s)
        //    //{
        //    //    case 
        //    //}
        //    return new A();// (Key)(((int)key + (int)s) % 12);
        //}

        //public static ScaleDegree StepUp(this ScaleDegree sd, Step step)
        //{
        //    if ((int)sd > 11)
        //        throw new ArgumentOutOfRangeException(nameof(ScaleDegree), "Scale degree must not be greater than 11");

        //    return (ScaleDegree)((int)sd + (int)step);
        //}

        //public static Key GetNote(this ScaleDegree scaleDegree, Key key)
        //{
        //    return (Key)(key + scaleDegree);
        //}
    }

}