using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class NewPuzzleSetup_State : State
{
    public NewPuzzleSetup_State(PuzzleDifficulty difficulty)
    {
        PuzzleDifficulty = difficulty;
    }

    readonly PuzzleDifficulty PuzzleDifficulty;

    protected override void PrepareState(Action callback)
    {
        DataManager.Io.TheoryPuzzleData.PuzzleDifficulty = PuzzleDifficulty;
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        SetStateDirectly(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));
    }
}
