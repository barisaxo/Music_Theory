using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TheoryPuzzleData
{
    //public int Hints { get; private set; } = 20;
    public int CurrentHintsRemaining;
    public int TotalHintsUsed;
    public int TotalWrongAnswers;
    public int TotalFailedPuzzles;
    //public int SolvedPuzzles;
    //public int HighScore;

    public PuzzleStat[] Stats = new PuzzleStat[] { };

    public void LoadStatsData(PuzzleStat[] stats) => Stats = stats;

    public void AddStat(PuzzleStat puzzleStat)
    {
        bool containsStat = false;

        foreach (PuzzleStat stat in Stats)
        {
            if (stat.Specs.Equals(puzzleStat.Specs))
            {
                //Debug.Log("Specs equal eachother");
                containsStat = true;
                stat.AddStats(puzzleStat);
                break;
            }
        }

        if (!containsStat)
        {
            Stats = Stats.MergeWith(puzzleStat);
        }

        //foreach (PuzzleStat stat in Stats)
        //{
        //    Debug.Log("Puzzle type: " + stat.Specs.PuzzleType + ", Gamut Type: " + stat.Specs.GamutType +
        //        ", Hints: " + stat.HintsUsed + ", Wrong: " + stat.WrongAnswers + ", Failed: " + stat.Failed +
        //        ", Solved: " + stat.Solved);
        //}
    }

    //public string GetHintsRemaining => "hints remaining: " + PuzzleDifficulty switch
    //{
    //    PuzzleDifficulty.Free => "∞",
    //    _ => HintsRemaining.ToString()
    //};

    private PuzzleDifficulty _difficulty;
    public PuzzleDifficulty PuzzleDifficulty
    {
        get => _difficulty;
        set
        {
            _difficulty = value;
            //Hints = value switch
            //{
            //    PuzzleDifficulty.Challenge => 20,
            //    PuzzleDifficulty.Easy => 10,
            //    PuzzleDifficulty.Hard => 3,
            //    _ => 9001,
            //};
            //HintsRemaining = Hints;
        }
    }

    //public void ResetHints()
    //{
    //    Hints = PuzzleDifficulty switch
    //    {
    //        PuzzleDifficulty.Challenge => HintsRemaining,
    //        PuzzleDifficulty.Easy => 10,
    //        PuzzleDifficulty.Hard => 3,
    //        _ => 9001,
    //    };
    //    HintsRemaining = Hints;
    //}

}

public enum PuzzleDifficulty { Free, Easy, Hard, Challenge }
public enum PuzzleType { Aural, Theory }
public enum AnswerData { Wrong, Skipped, Solved, Hinted }
