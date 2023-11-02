using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PuzzleSelector
{

    public static State WeightedRandomPuzzleState(this TheoryPuzzleData data) => Random.Range(0, 35) switch
    {
        < 4 => new Puzzle_State<KeyboardNoteName>(new NotePuzzle(), RandPuzzleType),
        < 7 => new Puzzle_State<MusicTheory.Steps.Step>(new StepsPuzzle(), RandPuzzleType),
        < 10 => new Puzzle_State<MusicTheory.Triads.Triad>(new TriadPuzzle(), RandPuzzleType),
        < 15 => new Puzzle_State<MusicTheory.Triads.Triad>(new InvertedTriadPuzzle(), RandPuzzleType),
        < 20 => new Puzzle_State<MusicTheory.Scales.Scale>(new ScalePuzzle(), RandPuzzleType),
        < 25 => new Puzzle_State<MusicTheory.Scales.Scale>(new ModePuzzle(), RandPuzzleType),
        < 30 => new Puzzle_State<MusicTheory.SeventhChords.SeventhChord>(new SeventhChordPuzzle(), RandPuzzleType),
        _ => new Puzzle_State<MusicTheory.SeventhChords.SeventhChord>(new InvertedSeventhChordPuzzle(), RandPuzzleType),
    };

    static PuzzleType RandPuzzleType => Random.value > .5f ? PuzzleType.Theory : PuzzleType.Aural;


}
