using MusicTheory.Scales;
using MusicTheory.Keys;
using MusicTheory.Intervals;

namespace MusicTheory
{
    public static class ChordSystems
    {
        public static Key Root(this Scale s, int currentScaleDegree, Key keyOf)
        {
            return s.ScaleDegrees[currentScaleDegree].Above(keyOf);
        }

        public static Key Third(this Scale s, int currentScaleDegree, Key keyOf)
        {
            UnityEngine.Debug.Log("Third " + (currentScaleDegree + 2) % s.ScaleDegrees.Length + " " + keyOf);
            return s.ScaleDegrees[(currentScaleDegree + 2) % s.ScaleDegrees.Length].Above(keyOf);
        }

        public static Key Fifth(this Scale s, int currentScaleDegree, Key keyOf)
        {
            return s.ScaleDegrees[(currentScaleDegree + 4) % s.ScaleDegrees.Length].Above(keyOf);
        }

        public static Key Seventh(this Scale s, int currentScaleDegree, Key keyOf)
        {
            return s.ScaleDegrees[(currentScaleDegree + 6) % s.ScaleDegrees.Length].Above(keyOf);
        }

        public static Key Ninth(this Scale s, int currentScaleDegree, Key keyOf)
        {
            return s.ScaleDegrees[(currentScaleDegree + 1) % s.ScaleDegrees.Length].Above(keyOf);
        }

        public static Key Eleventh(this Scale s, int currentScaleDegree, Key keyOf)
        {
            return s.ScaleDegrees[(currentScaleDegree + 3) % s.ScaleDegrees.Length].Above(keyOf);
        }

        public static Key Thirteenth(this Scale s, int currentScaleDegree, Key keyOf)
        {
            return s.ScaleDegrees[(currentScaleDegree + 5) % s.ScaleDegrees.Length].Above(keyOf);
        }
    }


}