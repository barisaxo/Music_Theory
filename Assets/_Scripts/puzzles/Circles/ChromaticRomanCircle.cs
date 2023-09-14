using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.RomanNumerals;

namespace MusicTheory
{
    public class ChromaticRomanCircle : Circle
    {
        public ChromaticRomanCircle(string name, float radius, Vector2 pos) : base(name, radius, pos) { }

        RomanNumeral RomanNumeral;
    }
}