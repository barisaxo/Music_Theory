using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Frequencies
{
    public static int GetHertz(this MusicTheory.Keys.Key note) => note.Id switch
    {
        0 => 262,
        1 => 277,
        2 => 294,
        3 => 311,
        4 => 330,
        5 => 349,
        6 => 370,
        7 => 392,
        8 => 415,
        9 => 440,
        10 => 466,
        11 => 247 * 2,
        _ => throw new System.ArgumentOutOfRangeException()
    };

}
