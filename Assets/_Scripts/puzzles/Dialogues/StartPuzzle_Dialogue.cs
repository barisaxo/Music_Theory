using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class StartPuzzle_Dialogue : Dialogue
{
    private const string _about =
        "Select difficulty:\n" +
        "Free: get unlimited hints\n" +
        "Easy: get 10 hints for every question\n" +
        "Hard: get 3 hints for every question\n" +
        "Challenge: see how far you can go with 20 hints";


    public override Dialogue Initiate()
    {
        FirstLine = GetLines();
        return this;
    }

    private Line GetLines()
    {
        return new Line(_about, Replies());
    }

    private Response[] Replies()
    {
        return new[]
        {
            new Response("Free", new NewPuzzleSetup_State(PuzzleDifficulty.Free)).FadeToNextState(),
            new Response("Easy", new NewPuzzleSetup_State(PuzzleDifficulty.Easy)).FadeToNextState(),
            new Response("Hard", new NewPuzzleSetup_State(PuzzleDifficulty.Hard)).FadeToNextState(),
            new Response("Challenge", new NewPuzzleSetup_State(PuzzleDifficulty.Challenge)).FadeToNextState()
        };
    }




}
public class WelcomeDialogue : Dialogue
{
    public override Dialogue Initiate()
    {
        FirstLine = new Line("Welcome!" +
            "\nDue to WebGL security measures, audio playback may act unexpectedly, but should work as intended after first interaction." +
            "\n(tap anywhere to continue)", new StartPuzzle_Dialogue());
        return this;
    }

}