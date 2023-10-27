using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TheoryPuzzleData
{
    public int Hints { get; private set; } = 20;
    public int HintsRemaining;
    public int WrongAnswers;
    public int FailedPuzzles;
    public int SolvedPuzzles;
    public int HighScore;

    public string GetHintsRemaining => "hints remaining: " + PuzzleDifficulty switch
    {
        PuzzleDifficulty.Free => "∞",
        _ => HintsRemaining.ToString()
    };

    private PuzzleDifficulty _difficulty;
    public PuzzleDifficulty PuzzleDifficulty
    {
        get => _difficulty;
        set
        {
            _difficulty = value;
            Hints = value switch
            {
                PuzzleDifficulty.Challenge => 20,
                PuzzleDifficulty.Easy => 10,
                PuzzleDifficulty.Hard => 3,
                _ => 9001,
            };
            HintsRemaining = Hints;
        }
    }

    public void ResetHints()
    {
        Hints = PuzzleDifficulty switch
        {
            PuzzleDifficulty.Challenge => HintsRemaining,
            PuzzleDifficulty.Easy => 10,
            PuzzleDifficulty.Hard => 3,
            _ => 9001,
        };
        HintsRemaining = Hints;
    }

}
public enum PuzzleDifficulty { Free, Easy, Hard, Challenge }