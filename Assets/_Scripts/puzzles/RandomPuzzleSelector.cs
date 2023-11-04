using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PuzzleSelector
{
    public static State WeightedRandomPuzzleState(this TheoryPuzzleData data) => Random.Range(0, 35) switch
    {
        < 4 => new Puzzle_State(new NotePuzzle(), RandPuzzleType),
        < 7 => new Puzzle_State(new StepsPuzzle(), RandPuzzleType),
        < 10 => new Puzzle_State(new TriadPuzzle(), RandPuzzleType),
        < 15 => new Puzzle_State(new InvertedTriadPuzzle(), RandPuzzleType),
        < 20 => new Puzzle_State(new ScalePuzzle(), RandPuzzleType),
        < 25 => new Puzzle_State(new ModePuzzle(), RandPuzzleType),
        < 30 => new Puzzle_State(new SeventhChordPuzzle(), RandPuzzleType),
        _ => new Puzzle_State(new InvertedSeventhChordPuzzle(), RandPuzzleType),
    };

    static PuzzleType RandPuzzleType => Random.value > .5f ? PuzzleType.Theory : PuzzleType.Aural;


}
