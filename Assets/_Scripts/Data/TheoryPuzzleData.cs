using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TheoryPuzzleData
{
    //public int Hints { get; private set; } = 20;
    //public int HintsRemaining;
    //public int WrongAnswers;
    //public int FailedPuzzles;
    //public int SolvedPuzzles;
    //public int HighScore;

    private Dictionary<Puzzle, AnswerData[]> _stats;
    public Dictionary<Puzzle, AnswerData[]> Stats => _stats ??= new();

    public void LoadStatsData(Dictionary<Puzzle, AnswerData[]> stats) => _stats = stats;

    public void AddStat(Puzzle puzzle, AnswerData[] score)
    {
        if (!Stats.ContainsKey(puzzle))
        {
            Stats.TryAdd(puzzle, score);
        }
        else
        {
            Stats.TryGetValue(puzzle, out AnswerData[] data);
            AnswerData[] temp = data.MergeWith(score);
            Stats.Remove(puzzle);
            Stats.TryAdd(puzzle, temp);
        }

        foreach (KeyValuePair<Puzzle, AnswerData[]> stat in Stats)
        {
            foreach (AnswerData datum in stat.Value)
                Debug.Log(stat.Key.PuzzleType + " " + stat.Key.PuzzleGamut.GetType() + " " + datum.ToString());
        }
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
//public enum PuzzleGamut { Note, Step, Interval, Triad, InvertedTriad, Scale, Mode, SeventhChord, InvertedSeventhChord }
public enum AnswerData { Wrong, Skipped, Solved, Hinted }

public readonly struct Puzzle
{
    public readonly PuzzleType PuzzleType;

    public readonly object PuzzleGamut;

    public Puzzle(PuzzleType type, object gamut)
    {
        PuzzleType = type;
        PuzzleGamut = gamut;
    }
}


public static class ArrayUtilities
{
    public static T[] MergeWith<T>(this T[] t1, T[] t2)
    {
        List<T> temp = new();
        foreach (T datum in t1) { temp.Add(datum); }
        foreach (T datum in t2) { temp.Add(datum); }
        return temp.ToArray();
    }
}