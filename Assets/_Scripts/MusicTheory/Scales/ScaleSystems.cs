
namespace MusicTheory.Arithmetic
{
    public static class ScaleSystems
    {
        //public static Steps.Step[] ShiftSteps(this Scales.Scale scale, Modes.ModeDegreeEnum mode)
        //{
        //    Steps.Step[] newSteps = new Steps.Step[scale.Steps.Length];

        //    for (int i = (int)mode; i < (int)mode + newSteps.Length; i++)
        //    {
        //        newSteps[i] = scale.Steps[i < newSteps.Length ? i : i - newSteps.Length];
        //    }

        //    return newSteps;
        //}

        public static int GetIndex(this Scales.Scale scale, Modes.Mode mode)
        {
            for (int i = 0; i < scale.Modes.Length; i++)
                if (mode.Equals(scale.Modes[i])) { return i; }
            return 0;
        }

        public static int GetIndex(this Scales.Scale scale, ScaleDegrees.ScaleDegree degree)
        {
            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (degree.Equals(scale.ScaleDegrees[i])) { return i; }
            return 0;
        }

        public static Keys.Key Root(this Scales.Scale scale, ScaleDegrees.ScaleDegree currentScaleDegree, Keys.Key keyOf)
        {
            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval());
        }

        public static Keys.Key Third(this Scales.Scale scale, ScaleDegrees.ScaleDegree currentScaleDegree, Keys.Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree.GetInterval(scale.ScaleDegrees[(x + 2) % scale.ScaleDegrees.Length]));
        }

        public static Keys.Key Fifth(this Scales.Scale scale, ScaleDegrees.ScaleDegree currentScaleDegree, Keys.Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree
                .GetInterval(scale.ScaleDegrees[(x + 4) % scale.ScaleDegrees.Length]));
        }

        public static Keys.Key Seventh(this Scales.Scale scale, ScaleDegrees.ScaleDegree currentScaleDegree, Keys.Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree.GetInterval(scale.ScaleDegrees[(x + 6) % scale.ScaleDegrees.Length]));
        }

        public static Keys.Key Ninth(this Scales.Scale scale, ScaleDegrees.ScaleDegree currentScaleDegree, Keys.Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree.GetInterval(scale.ScaleDegrees[(x + 1) % scale.ScaleDegrees.Length]));
        }

        public static Keys.Key Eleventh(this Scales.Scale scale, ScaleDegrees.ScaleDegree currentScaleDegree, Keys.Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree
                .GetInterval(scale.ScaleDegrees[(x + 3) % scale.ScaleDegrees.Length]));
        }

        public static Keys.Key Thirteenth(this Scales.Scale scale, ScaleDegrees.ScaleDegree currentScaleDegree, Keys.Key keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetKeyAbove(currentScaleDegree.AsInterval())
                .GetKeyAbove(currentScaleDegree
                .GetInterval(scale.ScaleDegrees[(x + 5) % scale.ScaleDegrees.Length]));
        }
    }

}