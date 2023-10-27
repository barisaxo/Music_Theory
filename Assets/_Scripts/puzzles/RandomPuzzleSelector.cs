using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomPuzzleSelector
{

    public static State GetRandomPuzzleState() => Random.Range(0, 8) switch
    {
        0 => new NoteIdentificationAuralPuzzle_State(),
        1 => new NoteIdentificationDescriptionPuzzle_State(),
        2 => new IntervalAuralPuzzle_State(),
        3 => new IntervalDescriptionPuzzle_State(),
        4 => new TriadAuralPuzzle_State(),
        5 => new TriadDescriptionPuzzle_State(),
        6 => new TriadInversionAuralPuzzle_State(),
        7 => new TriadInversionDescriptionPuzzle_State(),
        //8 => new KeyboardTest_State(),
    };





}
