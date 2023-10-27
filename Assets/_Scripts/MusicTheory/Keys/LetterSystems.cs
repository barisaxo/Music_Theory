

namespace MusicTheory.Keys
{
    public static class LetterSystems
    {
        public static Letter GetLetter(this Key key, ScaleDegrees.ScaleDegree degree) =>
            (LetterEnum)(key.Enum.Letter.Id + degree.Id);

    }
}
