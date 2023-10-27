using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Frequencies
{
    public static float GetHertz(this MusicTheory.Keys.Key note) => note.Id switch
    {
        0 => 261.63f,
        1 => 277.18f,
        2 => 293.66f,
        3 => 311.13f,
        4 => 329.63f,
        5 => 349.23f,
        6 => 369.99f,
        7 => 392.00f,
        8 => 415.30f,
        9 => 440.00f,
        10 => 466.16f,
        11 => 493.88f,
        _ => throw new System.ArgumentOutOfRangeException()
    };

}
