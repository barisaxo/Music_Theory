//using System;
//using UnityEngine;

//namespace MusicTheory
//{
//    /// <summary>
//    /// Music terms and conversions thereof.
//    /// </summary>
//    public static class MusicTheory
//    {
//        public static int GetTempo(this Genre genre) => genre switch
//        {
//            // Genre.Shuffle => 90,
//            // Genre.ChaCha => 115,
//            // Genre.GuitarTrio => 115,
//            // Genre.Bossa => 140,
//            // Genre.FunkyBlues => 105,
//            // Genre.JazzBalladMelodic => 123,
//            Genre.Stax => 90,
//            Genre.Bolero => 125,
//            Genre.Rock => 115,
//            Genre.Tango => 130,
//            Genre.BlueNote => 120,
//            _ => 90,
//        };





//        //public static string Flat => "♭";//Doesn't exist in default font =(
//        public static string ToRomanString(this HarmonicFunction hf) => RomanNumeralGroupOfHarmonicFunction(hf);
//        public static string RomanNumeralGroupOfHarmonicFunction(HarmonicFunction hf) => hf switch
//        {
//            HarmonicFunction.Predominant => "II  IV",
//            HarmonicFunction.Dominant => "V  VII",
//            HarmonicFunction.Tonic => "I  III  VI",
//            _ => "?"
//        };

//        public static string String(this Key key) => KeyToString(key);
//        public static string KeyToString(this Key key) => key switch
//        {
//            Key.C => "C",
//            Key.G => "G",
//            Key.D => "D",
//            Key.A => "A",
//            Key.E => "E",
//            Key.B => "B",
//            Key.Gb => "Gb",
//            Key.Db => "Db",
//            Key.Ab => "Ab",
//            Key.Eb => "Eb",
//            Key.Bb => "Bb",
//            Key.F => "F",
//            _ => "?"
//        };

//        public static string TriadQuality(this DiatonicRomanNumeral d) => d switch
//        {
//            DiatonicRomanNumeral.II => "-",
//            DiatonicRomanNumeral.III => "-",
//            DiatonicRomanNumeral.VI => "-",
//            DiatonicRomanNumeral.VII => "º",
//            _ => ""
//        };

//        public static int Count(this ChordQuality cq) => Enum.GetNames(typeof(ChordQuality)).Length;
//        public static string String(this ChordQuality quality) => QualityString(quality);
//        public static string QualityString(ChordQuality quality) => quality switch
//        {
//            ChordQuality.Maj => "",
//            // ChordQuality.Maj6 => "6",
//            // ChordQuality.Maj7 => "∆7",
//            // ChordQuality.Maj9 => "∆9",
//            // ChordQuality.Maj7s11 => "∆7(#11)",

//            ChordQuality.Min => "_",
//            // ChordQuality.Min7 => "-7",
//            // ChordQuality.Min9 => "-9",
//            // ChordQuality.Min11 => "-11",

//            // ChordQuality.Min6 => "-6",
//            // ChordQuality.Min69 => "-6/9",
//            // ChordQuality.MinM7 => "-∆7",
//            // ChordQuality.MinM9 => "-∆9",

//            ChordQuality.Dom7 => "7",
//            // ChordQuality.Dom9 => "9",
//            // ChordQuality.Dom13 => "13",
//            // ChordQuality.Dom7Sus => "7Sus",

//            // ChordQuality.Dom7s11 => "7(♯11)",
//            // ChordQuality.Dom7Alt => "7Alt",

//            // ChordQuality.Dom7b9 => "7(b9)",
//            // ChordQuality.Dom7s9 => "7(#9)",
//            // ChordQuality.Dom7Susb9 => "7Sus(b9)",

//            ChordQuality.Dim => "o",
//            // ChordQuality.Dim7 => "o7",

//            ChordQuality.Min7b5 => "-7(b5)",
//            // ChordQuality.Min9b5 => "ø9",

//            // ChordQuality.Aug => "+",
//            // ChordQuality.AugMaj7 => "∆7(#5)",

//            _ => "?",
//        };



//        public static string String(this ChromaticRomanNumeral chord) => RomanNumeralToString(chord);
//        public static string RomanNumeralToString(ChromaticRomanNumeral chord) => chord switch
//        {
//            ChromaticRomanNumeral.I => "I",
//            ChromaticRomanNumeral.III => "III",
//            ChromaticRomanNumeral.VI => "VI",
//            ChromaticRomanNumeral.II => "II",
//            ChromaticRomanNumeral.IV => "IV",
//            ChromaticRomanNumeral.V => "V",
//            ChromaticRomanNumeral.VII => "VII",
//            ChromaticRomanNumeral.bII => "bII",
//            ChromaticRomanNumeral.bIII => "bIII",
//            ChromaticRomanNumeral.bV => "bV",
//            ChromaticRomanNumeral.bVI => "bVI",
//            ChromaticRomanNumeral.bVII => "bVII",
//            _ => "?",
//        };



//        public static string String(this MusicalScale scale, ChromaticRomanNumeral numeral) =>
//            ModeToString(scale, numeral);
//        public static string ModeToString(MusicalScale scale, ChromaticRomanNumeral numeral) => scale switch
//        {
//            MusicalScale.Major => numeral switch
//            {
//                ChromaticRomanNumeral.I => nameof(GregorianMode.Ionian),
//                ChromaticRomanNumeral.II => nameof(GregorianMode.Dorian),
//                ChromaticRomanNumeral.III => nameof(GregorianMode.Phrygian),
//                ChromaticRomanNumeral.IV => nameof(GregorianMode.Lydian),
//                ChromaticRomanNumeral.V => nameof(GregorianMode.Mixolydian),
//                ChromaticRomanNumeral.VI => nameof(GregorianMode.Aeolian),
//                ChromaticRomanNumeral.VII => nameof(GregorianMode.Locrian),
//                _ => "?"
//            },

//            _ => numeral switch
//            {
//                ChromaticRomanNumeral.I => "1st Mode of -∆",
//                ChromaticRomanNumeral.II => "2nd Mode of -∆",
//                ChromaticRomanNumeral.bIII => "3rd Mode of -∆",
//                ChromaticRomanNumeral.IV => "4th Mode of -∆",
//                ChromaticRomanNumeral.V => "5th Mode of -∆",
//                ChromaticRomanNumeral.VI => "6th Mode of -∆",
//                ChromaticRomanNumeral.VII => "7th Mode of -∆",
//                _ => "?"
//            }
//        };

//        public static string String(this DiatonicFunction function) => FunctionToString(function);
//        public static string FunctionToString(DiatonicFunction function) => function switch
//        {
//            DiatonicFunction.Tonic => nameof(DiatonicFunction.Tonic),
//            DiatonicFunction.LateralPredominant => nameof(DiatonicFunction.Predominant),
//            DiatonicFunction.MediantTonic => nameof(DiatonicFunction.Tonic),
//            DiatonicFunction.Predominant => nameof(DiatonicFunction.Predominant),
//            DiatonicFunction.Dominant => nameof(DiatonicFunction.Dominant),
//            DiatonicFunction.SubmediantTonic => nameof(DiatonicFunction.Tonic),
//            DiatonicFunction.LateralDominant => nameof(DiatonicFunction.Dominant),
//            _ => "?"
//        };


//        public static HarmonicFunction ToFunction(this ChromaticRomanNumeral numeral) =>
//            NumeralToFunction(numeral);
//        public static HarmonicFunction NumeralToFunction(ChromaticRomanNumeral numeral) => numeral switch
//        {
//            ChromaticRomanNumeral.I => HarmonicFunction.Tonic,
//            ChromaticRomanNumeral.II => HarmonicFunction.Predominant,
//            ChromaticRomanNumeral.III => HarmonicFunction.Tonic,
//            ChromaticRomanNumeral.IV => HarmonicFunction.Predominant,
//            ChromaticRomanNumeral.V => HarmonicFunction.Dominant,
//            ChromaticRomanNumeral.VI => HarmonicFunction.Tonic,
//            ChromaticRomanNumeral.VII => HarmonicFunction.Dominant,
//            ChromaticRomanNumeral.bII => HarmonicFunction.Secondary,
//            ChromaticRomanNumeral.bIII => HarmonicFunction.Secondary,
//            ChromaticRomanNumeral.bV => HarmonicFunction.Secondary,
//            ChromaticRomanNumeral.bVI => HarmonicFunction.Secondary,
//            ChromaticRomanNumeral.bVII => HarmonicFunction.Secondary,
//            _ => HarmonicFunction.Secondary,
//        };



//        public static ChordQuality ToQuality(this DiatonicRomanNumeral chord, Extension extension) =>
//           ChordToQuality(DiatonicToChromaticRoman(chord), extension);
//        public static ChordQuality ChordToQuality(DiatonicRomanNumeral chord, Extension extension) =>
//            ChordToQuality(DiatonicToChromaticRoman(chord), extension);


//        public static ChordQuality ToQuality(this ChromaticRomanNumeral chord, Extension extension) =>
//           ChordToQuality(chord, extension);
//        public static ChordQuality ChordToQuality(ChromaticRomanNumeral chord, Extension extension) =>
//            extension switch
//            {
//                Extension.Triad => chord switch
//                {
//                    ChromaticRomanNumeral.I => ChordQuality.Maj,
//                    ChromaticRomanNumeral.II => ChordQuality.Min,
//                    ChromaticRomanNumeral.III => ChordQuality.Min,
//                    ChromaticRomanNumeral.IV => ChordQuality.Maj,
//                    ChromaticRomanNumeral.V => ChordQuality.Dom7,
//                    ChromaticRomanNumeral.VI => ChordQuality.Min,
//                    ChromaticRomanNumeral.VII => ChordQuality.Min7b5,
//                    _ => ChordQuality.Maj,

//                },
//                Extension.Seventh => chord switch
//                {//TODO make ∆7 && -7 audio clips
//                    ChromaticRomanNumeral.I => ChordQuality.Maj,
//                    ChromaticRomanNumeral.II => ChordQuality.Min,
//                    ChromaticRomanNumeral.III => ChordQuality.Min,
//                    ChromaticRomanNumeral.IV => ChordQuality.Maj,
//                    ChromaticRomanNumeral.V => ChordQuality.Dom7,
//                    ChromaticRomanNumeral.VI => ChordQuality.Min,
//                    ChromaticRomanNumeral.VII => ChordQuality.Min7b5,
//                    _ => ChordQuality.Maj,

//                },
//                Extension.Jazz => chord switch
//                {//TODO make jazz audio clips
//                    ChromaticRomanNumeral.I => ChordQuality.Maj,
//                    ChromaticRomanNumeral.II => ChordQuality.Min,
//                    ChromaticRomanNumeral.III => ChordQuality.Min,
//                    ChromaticRomanNumeral.IV => ChordQuality.Maj,
//                    ChromaticRomanNumeral.V => ChordQuality.Dom7,
//                    ChromaticRomanNumeral.VI => ChordQuality.Min,
//                    ChromaticRomanNumeral.VII => ChordQuality.Min7b5,
//                    _ => ChordQuality.Maj,
//                },
//                _ => ChordQuality.Maj,
//            };




//        public static Key RootNote(this DiatonicRomanNumeral diatonicChord, Key key) =>
//            RootNote(DiatonicToChromaticRoman(diatonicChord), key);
//        public static Key RootNote(this ChromaticRomanNumeral romanNumeral, Key key)
//        {
//            return romanNumeral switch
//            {
//                ChromaticRomanNumeral.I => key,
//                ChromaticRomanNumeral.bII => KeyPlusInterval(1),
//                ChromaticRomanNumeral.II => KeyPlusInterval(2),
//                ChromaticRomanNumeral.bIII => KeyPlusInterval(3),
//                ChromaticRomanNumeral.III => KeyPlusInterval(4),
//                ChromaticRomanNumeral.IV => KeyPlusInterval(5),
//                ChromaticRomanNumeral.bV => KeyPlusInterval(6),
//                ChromaticRomanNumeral.V => KeyPlusInterval(7),
//                ChromaticRomanNumeral.bVI => KeyPlusInterval(8),
//                ChromaticRomanNumeral.VI => KeyPlusInterval(9),
//                ChromaticRomanNumeral.bVII => KeyPlusInterval(10),
//                ChromaticRomanNumeral.VII => KeyPlusInterval(11),
//                _ => key,
//            };

//            Key KeyPlusInterval(int interval) => (key + interval) > KeyCount() - 1 ?
//                (Key)(key + interval - KeyCount()) :
//                key + interval;
//            Key KeyCount() => (Key)Enum.GetNames(typeof(Key)).Length;
//        }



//        public static ChromaticRomanNumeral ToChromaticRoman(this DiatonicRomanNumeral dc) =>
//            DiatonicToChromaticRoman(dc);
//        public static ChromaticRomanNumeral DiatonicToChromaticRoman(DiatonicRomanNumeral dc) => dc switch
//        {
//            DiatonicRomanNumeral.I => ChromaticRomanNumeral.I,
//            DiatonicRomanNumeral.II => ChromaticRomanNumeral.II,
//            DiatonicRomanNumeral.III => ChromaticRomanNumeral.III,
//            DiatonicRomanNumeral.IV => ChromaticRomanNumeral.IV,
//            DiatonicRomanNumeral.V => ChromaticRomanNumeral.V,
//            DiatonicRomanNumeral.VI => ChromaticRomanNumeral.VI,
//            DiatonicRomanNumeral.VII => ChromaticRomanNumeral.VII,
//            _ => ChromaticRomanNumeral.bII,
//        };



//        public static ChromaticRomanNumeral ToChromaticRoman(this DiatonicFunction dc) =>
//            DiatonicFunctionToChromaticRoman(dc);
//        public static ChromaticRomanNumeral DiatonicFunctionToChromaticRoman(DiatonicFunction dc) => dc switch
//        {
//            DiatonicFunction.Tonic => ChromaticRomanNumeral.I,
//            DiatonicFunction.LateralPredominant => ChromaticRomanNumeral.II,
//            DiatonicFunction.MediantTonic => ChromaticRomanNumeral.II,
//            DiatonicFunction.Predominant => ChromaticRomanNumeral.IV,
//            DiatonicFunction.Dominant => ChromaticRomanNumeral.V,
//            DiatonicFunction.SubmediantTonic => ChromaticRomanNumeral.VI,
//            DiatonicFunction.LateralDominant => ChromaticRomanNumeral.VII,
//            _ => ChromaticRomanNumeral.bII
//        };



//        public static DiatonicFunction ToDiatonicFunction(this DiatonicRomanNumeral drn) =>
//            DiatonicRomanToDiatonicFunction(drn);
//        public static DiatonicFunction DiatonicRomanToDiatonicFunction(DiatonicRomanNumeral drn) => drn switch
//        {
//            DiatonicRomanNumeral.II => DiatonicFunction.LateralPredominant,
//            DiatonicRomanNumeral.III => DiatonicFunction.MediantTonic,
//            DiatonicRomanNumeral.IV => DiatonicFunction.Predominant,
//            DiatonicRomanNumeral.V => DiatonicFunction.Dominant,
//            DiatonicRomanNumeral.VI => DiatonicFunction.SubmediantTonic,
//            DiatonicRomanNumeral.VII => DiatonicFunction.LateralDominant,
//            _ => DiatonicFunction.Tonic,
//        };



//        public static DiatonicFunction ToDiatonicFunction(this HarmonicFunction hf) =>
//            HarmonicToDiatonicFunction(hf);
//        public static DiatonicFunction HarmonicToDiatonicFunction(HarmonicFunction hf) => hf switch
//        {
//            HarmonicFunction.Dominant => DiatonicFunction.Dominant,
//            HarmonicFunction.Predominant => DiatonicFunction.Predominant,
//            _ => DiatonicFunction.Tonic,
//        };



//        public static HarmonicFunction ToHarmonicFunction(this DiatonicFunction df) =>
//            DiatonicToHarmonicFunction(df);
//        public static HarmonicFunction DiatonicToHarmonicFunction(DiatonicFunction df) => df switch
//        {
//            DiatonicFunction.Dominant => HarmonicFunction.Dominant,
//            DiatonicFunction.LateralDominant => HarmonicFunction.Dominant,
//            DiatonicFunction.LateralPredominant => HarmonicFunction.Predominant,
//            DiatonicFunction.Predominant => HarmonicFunction.Predominant,
//            _ => HarmonicFunction.Tonic,
//        };


//        public static Key InverselyTransposed(this Key key, Key tKey) =>
//            (Key)(((int)tKey - (int)key) < 0 ? (int)tKey - (int)key + 12 : (int)tKey - (int)key);

//        public static Key Transposed(this Key key, Key tKey) =>
//            (Key)(((int)key - (int)tKey) < 0 ? (int)key - (int)tKey + 12 : (int)key - (int)tKey);

//        public static string EnharmonicNoteName(this Key note, Key key)
//        {
//            switch (note)
//            {
//                case Key.Gb:
//                    switch (key) { case Key.A: case Key.B: case Key.D: case Key.E: case Key.G: return "F♯"; }
//                    break;

//                case Key.Db:
//                    switch (key) { case Key.A: case Key.B: case Key.D: case Key.E: return "C♯"; }
//                    break;

//                case Key.Eb:
//                    switch (key) { case Key.B: case Key.E: return "D♯"; }
//                    break;

//                case Key.Ab:
//                    switch (key) { case Key.B: case Key.E: return "G♯"; }
//                    break;

//                case Key.Bb:
//                    switch (key) { case Key.B: return "A♯"; }
//                    break;

//                case Key.B:
//                    switch (key) { case Key.Gb: return "Cb"; }
//                    break;
//            }
//            return note.ToString();
//        }




//        public static string ToChordName(this ChromaticRomanNumeral numeral, Key key) => ChordName(numeral, key);
//        public static string ChordName(ChromaticRomanNumeral numeral, Key key)
//        {
//            string chordName = string.Empty;
//            switch (key)
//            {
//                case Key.D:
//                    chordName = numeral switch
//                    {
//                        ChromaticRomanNumeral.III => "F♯",
//                        ChromaticRomanNumeral.VII => "C♯",
//                        _ => KeyToString(RootNote(numeral, key)),
//                    }; break;

//                case Key.E:
//                    chordName += numeral switch
//                    {
//                        ChromaticRomanNumeral.II => "F♯",
//                        ChromaticRomanNumeral.III => "G♯",
//                        ChromaticRomanNumeral.VI => "C♯",
//                        ChromaticRomanNumeral.VII => "D♯",
//                        _ => KeyToString(RootNote(numeral, key)),
//                    }; break;

//                case Key.G:
//                    chordName += numeral switch
//                    {
//                        ChromaticRomanNumeral.VII => "F♯",
//                        _ => KeyToString(RootNote(numeral, key)),
//                    }; break;

//                case Key.A:
//                    chordName = numeral switch
//                    {
//                        ChromaticRomanNumeral.III => "C♯",
//                        ChromaticRomanNumeral.VI => "F♯",
//                        ChromaticRomanNumeral.VII => "G♯",
//                        _ => KeyToString(RootNote(numeral, key)),
//                    }; break;

//                case Key.B:
//                    chordName = numeral switch
//                    {
//                        ChromaticRomanNumeral.II => "C♯",
//                        ChromaticRomanNumeral.III => "D♯",
//                        ChromaticRomanNumeral.V => "F♯",
//                        ChromaticRomanNumeral.VI => "G♯",
//                        ChromaticRomanNumeral.VII => "A♯",
//                        _ => KeyToString(RootNote(numeral, key)),
//                    }; break;

//                case Key.Gb:
//                    chordName = numeral switch
//                    {
//                        ChromaticRomanNumeral.IV => "Cb",
//                        _ => KeyToString(RootNote(numeral, key)),
//                    }; break;

//                default: chordName = KeyToString(RootNote(numeral, key)); break;
//            }

//            //chordName += numeral switch
//            //{
//            //    ChromaticRomanNumeral.II => "-",
//            //    ChromaticRomanNumeral.III => "-",
//            //    ChromaticRomanNumeral.VI => "-",
//            //    ChromaticRomanNumeral.VII => "ø",
//            //    _ => "",
//            //};


//            return chordName;
//        }

//        //internal static DiatonicFunction GetDiatonicFunction()
//        //{

//        //}
//        // public static RegionalMode ToRegion(this Bard2D.Boards.ShipType shipType) => shipType switch
//        // {
//        //     Bard2D.Boards.ShipType.Aeolian => RegionalMode.Aeolian,
//        //     Bard2D.Boards.ShipType.Dorian => RegionalMode.Dorian,
//        //     Bard2D.Boards.ShipType.Phrygian => RegionalMode.Phrygian,
//        //     Bard2D.Boards.ShipType.Ionian => RegionalMode.Ionian,
//        //     Bard2D.Boards.ShipType.Locrian => RegionalMode.Locrian,
//        //     Bard2D.Boards.ShipType.Lydian => RegionalMode.Lydian,
//        //     Bard2D.Boards.ShipType.Mixolydian => RegionalMode.MixoLydian,
//        //     _ => 0,
//        // };

//        // public static Color RegionalColor(this RegionalMode rm) => rm switch
//        // {
//        //     RegionalMode.Ionian => Assets.Rb,
//        //     RegionalMode.Dorian => Assets.Rg,
//        //     RegionalMode.MixoLydian => Assets.Bg,
//        //     RegionalMode.Lydian => Assets.Gr,
//        //     RegionalMode.Phrygian => Assets.Br,
//        //     RegionalMode.Aeolian => Assets.M,
//        //     RegionalMode.Locrian => Assets.Gb,
//        //     _ => Color.grey,
//        // };

//        public static int Count(this RegionalMode _) => Enum.GetNames(typeof(RegionalMode)).Length;
//        public static RegionalMode RandomMode(this RegionalMode _) => (RegionalMode)UnityEngine.Random.Range(0, RegionalMode.Locrian.Count());
//        public static RegionalMode RandomMode() => (RegionalMode)UnityEngine.Random.Range(0, RegionalMode.Locrian.Count());

//        public static int Count(this Genre genre) => Enum.GetNames(typeof(Genre)).Length;

//        // public static AudioClip RegionalTonality(this RegionalMode mode) => mode switch
//        // {
//        //     // RegionalMode.Ionian => Assets.CM7Q,
//        //     RegionalMode.Dorian => Assets.DMinSoWhat,
//        //     RegionalMode.Phrygian => Assets.E7Alt,
//        //     RegionalMode.Lydian => Assets.Fdim,
//        //     RegionalMode.MixoLydian => Assets.G7,
//        //     RegionalMode.Aeolian => Assets.AmM9,
//        //     RegionalMode.Locrian => Assets.Bm7b5,
//        //     _ => Assets.CM7Q,
//        // };




//        public static int Count(this Key _) => Enum.GetNames(typeof(Key)).Length;
//        public static Key RandomKey(this Key _) => (Key)UnityEngine.Random.Range(0, Key.A.Count());
//        public static Key RandomKey() => (Key)UnityEngine.Random.Range(0, Key.A.Count());



//    }

//    public enum DiatonicRomanNumeral { I, II, III, IV, V, VI, VII, };
//    public enum ChromaticRomanNumeral { I, bII, II, bIII, III, IV, bV, V, bVI, VI, bVII, VII, };
//    public enum SecondaryRomanNumeral { bII, bIII, bV, bVI, bVII }

//    public enum HarmonicFunction { Tonic, Predominant, Dominant, Secondary };
//    public enum DiatonicFunction
//    {
//        Tonic,
//        LateralPredominant,
//        MediantTonic,
//        Predominant,
//        Dominant,
//        SubmediantTonic,
//        LateralDominant
//    };

//    //public enum Tonality { Maj, Min, Dom, Sus, TonMin, Min7b5, Alt, Dim, Aug, Maj7, Min7 }

//    public enum Extension { Triad, Seventh, Jazz, Random, Mix }

//    public enum MusicalScale { Major, JazzMinor, Random,/* Jazz, HarmonicMinor, Mix, Diminished, Augmented, Chromatic, */ }

//    public enum MinorMode { First, Second, Third, Fourth, Fifth, Sixth, Seventh }

//    public enum GregorianMode { Ionian, Dorian, Phrygian, Lydian, Mixolydian, Aeolian, Locrian }

//    public enum RegionalMode { Ionian, Dorian, Phrygian, Lydian, MixoLydian, Aeolian, Locrian }

//    public enum Rune { Ionian, Dorian, Phrygian, Lydian, Mixolydian, Aeolian, Locrian }

//    public enum ChordQuality
//    {
//        Maj, Min, Dom7, Min7b5, Dim
//        // Maj, Maj6, Maj7, Maj9,
//        // Aug, AugMaj7, Maj7s11,
//        // Min, Min7, Min9, Min11,
//        // Min6, Min69, MinM7, MinM9,
//        // Dom7, Dom9, Dom13, Dom7Sus,
//        // Dom7s11, Dom7Alt, Dom7b9, Dom7s9,
//        // Dom7Susb9,
//        // Dim, Dim7,
//        // Min7b5, Min9b5,
//    }

//    public enum Key { C, Db, D, Eb, E, F, Gb, G, Ab, A, Bb, B }

//    public enum Genre
//    {/* Rock,Jazz, Reggae, Bossa, Blues */
//        // Shuffle,
//        // ChaCha,
//        // GuitarTrio,
//        // Bossa,
//        // FunkyBlues,
//        // JazzBalladMelodic,
//        Stax,
//        Bolero,
//        Rock,
//        Tango,
//        BlueNote,
//    }
//    public enum Instrument { Chords, Bass, Drums }




//}