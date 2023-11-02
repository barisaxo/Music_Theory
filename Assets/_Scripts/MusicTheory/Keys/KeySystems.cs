using System;

namespace MusicTheory.Arithmetic
{
    public static class KeySystems
    {
        public static Keys.Key[] GetKeys(this Keys.Key key, Scales.Scale scale, Modes.Mode mode)
        {
            Keys.Key[] keys = new Keys.Key[scale.ScaleDegrees.Length];
            keys[0] = key;

            for (int i = 1; i < keys.Length; i++)
                keys[i] = keys[i - 1].GetKeyAbove(scale.Steps[(i - 1 + scale.GetIndex(mode)) % keys.Length].AsInterval());

            return keys;
        }

        public static Triads.Triad GetTriadQuality(this Keys.Key root, Keys.Key third, Keys.Key fifth)
        {
            return (root.GetInterval(third), root.GetInterval(fifth)) switch
            {
                (Intervals.mi3, Intervals.P5) => new Triads.Minor(),
                (Intervals.A2, Intervals.P5) => new Triads.Minor(),

                (Intervals.M3, Intervals.P5) => new Triads.Major(),
                (Intervals.d4, Intervals.P5) => new Triads.Major(),

                (Intervals.M3, Intervals.A5) => new Triads.Augmented(),
                (Intervals.M3, Intervals.mi6) => new Triads.Augmented(),
                (Intervals.d4, Intervals.mi6) => new Triads.Augmented(),
                (Intervals.d4, Intervals.A5) => new Triads.Augmented(),

                (Intervals.mi3, Intervals.d5) => new Triads.Diminished(),
                (Intervals.mi3, Intervals.A4) => new Triads.Diminished(),
                (Intervals.A2, Intervals.d5) => new Triads.Diminished(),
                (Intervals.A2, Intervals.A4) => new Triads.Diminished(),

                //(Intervals.M2, Intervals.M3) => new Triads.Secundal(),//d3 + d3
                //(Intervals.M2, Intervals.d4) => new Triads.Secundal(),//d3 + d3
                //(Intervals.M2, Intervals.P4) => new Triads.Secundal(),//d3 + mi3

                //(Intervals.mi3, Intervals.P4) => new Triads.Quartal(),//mi3 + d3
                //(Intervals.M3, Intervals.M6) => new Triads.Quartal(),//M3 + A3
                //(Intervals.M3, Intervals.d7) => new Triads.Quartal(),//M3 + A3
                //(Intervals.d4, Intervals.M6) => new Triads.Quartal(),//M3 + M3
                //(Intervals.d4, Intervals.d7) => new Triads.Quartal(),//M3 + M3
                //(Intervals.P4, Intervals.d7) => new Triads.Quartal(),//A3 + M3
                //(Intervals.P4, Intervals.M6) => new Triads.Quartal(),//A3 + M3
                //(Intervals.P4, Intervals.mi7) => new Triads.Quartal(),//A3 + A3

                _ => throw new ArgumentOutOfRangeException(root.Name + ", " + root.GetInterval(third) + ", " + third.Name + ", " + root.GetInterval(fifth) + ", " + fifth.Name)
            };
        }

        public static Keys.Key KeepFlatOrNatural(this Keys.Key key)
        {
            if (key.Enum.Accidental is Keys.Sharp)
                foreach (var keyEnum in Enumeration.All<Keys.KeyEnum>())
                    if (keyEnum.Letter.Id.Equals((key.Enum.Letter.Id + 1) % 12) && key.Id == keyEnum.Id)
                        return keyEnum;
            return key;
        }

        public static Keys.Key GetKeyAbove(this Keys.Key key, Intervals.Interval interval)
        {
            if (interval is Intervals.P1 || interval is Intervals.P8) return key;

            int id = (key.Id + interval.Id) % 12;

            Keys.Letter letter = (Keys.LetterEnum)((key.Enum.Letter.Id + interval.Quantity.Id) % 7);
            Keys.Key newKey = Enumeration.FindId<Keys.KeyEnum>(id);

            foreach (var e in Enumeration.All<Keys.KeyEnum>())
            {
                if (e.Id.Equals(id) &&
                    MathF.Abs(e.Letter.Id - letter.Id) <=
                    MathF.Abs(newKey.Enum.Letter.Id - letter.Id))
                    newKey = e;
            }

            return newKey;
        }

        public static Intervals.Interval GetInterval(this Keys.Key left, Keys.Key right)
        {
            Intervals.Interval newInterval = new Intervals.P8();
            Intervals.Quantity quantity = left.GetQuantity(right);
            int id = (right.Id + 12 - left.Id) % 12;

            foreach (var interval in Enumeration.All<Intervals.IntervalEnum>())
            {
                if (interval.Id.Equals(id) &&
                    MathF.Abs(interval.Quantity.Id - quantity.Id) <
                    MathF.Abs(newInterval.Quantity.Id - quantity.Id))
                    newInterval = interval;
            }

            return newInterval;
        }
    }

}