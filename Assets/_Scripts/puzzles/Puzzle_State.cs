using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_State : State
{
    Keyboard Keyboard;
    readonly IPuzzle Puzzle;
    readonly AudioParser AudioParser = new();
    readonly PuzzleType PuzzleType;
    bool ReadyForNextPuzzle = false;

    int WrongAnswers;
    int HintsUsed;
    int Skipped;
    int Failed;
    int Solved;
    int Errors;

    public Puzzle_State(IPuzzle puzzle, PuzzleType puzzleType)
    {
        Puzzle = puzzle;
        PuzzleType = puzzleType;
    }

    protected override void PrepareState(Action callback)
    {
        Keyboard = Puzzle.NumOfNotes == 1 ? new(Puzzle.NumOfNotes) :
            new(Puzzle.NumOfNotes, Puzzle.Notes[0]);

        //Data.TheoryPuzzleData.ResetHints();
        _ = Question;
        _ = Desc;
        _ = Hint;
        _ = Listen;
        _ = SubmitAnswer;
        _ = Skip;
        base.PrepareState(callback);
    }

    protected override void PreEngageState(Action callback)
    {
        Listen.GO.SetActive(false);
        SubmitAnswer.GO.SetActive(false);
        //Hint.GO.SetActive(Data.TheoryPuzzleData.PuzzleDifficulty != PuzzleDifficulty.Free);
        base.PreEngageState(callback);
    }

    protected override void EngageState()
    {
        if (PuzzleType == PuzzleType.Aural)
            Audio.KBAudio.PlayNotes(QuestionClips(), null, Puzzle.QuestionPlaybackMode);
    }

    protected override void DisengageState()
    {
        Data.TheoryPuzzleData.AddStat(
            new PuzzleStat(
                specs: new PuzzleSpec(PuzzleType, Puzzle),
                hints: HintsUsed,
                wrong: WrongAnswers,
                fail: Failed,
                solve: Solved,
                skipped: Skipped,
                errors: Errors));

        Audio.KBAudio.Stop();
        Question.SelfDestruct();
        SubmitAnswer.SelfDestruct();
        Keyboard.SelfDestruct();
        Desc.SelfDestruct();
        Hint.SelfDestruct();
        Listen?.SelfDestruct();
        Skip.SelfDestruct();
        Data.SaveTheoryPuzzleData();
    }

    protected override void Clicked(MouseAction action, Vector3 mousePos)
    {
        if (ReadyForNextPuzzle && action == MouseAction.LUp)
        {
            //FadeToState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));

            FadeToState(new Puzzle_State(new InvertedSeventhChordPuzzle(), PuzzleType.Theory));

            //FadeToState(new Puzzle_State<MusicTheory.SeventhChords.SeventhChord>(new InvertedSeventhChordPuzzle(), RandPuzzleType()));
            //PuzzleType RandPuzzleType() => UnityEngine.Random.value > .5f ? PuzzleType.Theory : PuzzleType.Aural;
            return;// Click.Up;
        }

        base.Clicked(action, mousePos);
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(Keyboard.Parent.transform))
        {
            KeyboardKey key = Keyboard.IdentifyKey(go);
            Keyboard.InteractWithKey(key);

            bool containsKey = true;
            foreach (KeyboardNoteName note in Puzzle.Notes) if (note == key.KeyboardNoteName) { containsKey = true; break; }
            if (!containsKey) Errors++;
        }

        else if (go.transform.IsChildOf(Question.GO.transform) && Puzzle.AllowPlayQuestion)
        {
            //if (DataManager.Io.TheoryPuzzleData.HintsRemaining <= 0) return;
            //DataManager.Io.TheoryPuzzleData.HintsRemaining--;
            HintsUsed++;
            Audio.KBAudio.PlayNotes(QuestionClips(), null, Puzzle.QuestionPlaybackMode);
        }

        else if (go.transform.IsChildOf(SubmitAnswer.GO.transform))
        {
            if (AllNotesCorrect())
            {
                //Data.TheoryPuzzleData.SolvedPuzzles++;
                Solved++;
                Question.SetTextString(Puzzle.Question).SetImageColor(Color.clear);
                Audio.KBAudio.PlayNotes(QuestionClips(), EndPuzzleCallback, Puzzle.AnswerPlaybackMode);
                Listen.GO.SetActive(false);
                SubmitAnswer.GO.SetActive(false);
                //Hint.GO.SetActive(false);
                Desc.GO.SetActive(false);
                Skip.GO.SetActive(false);
                return;
            }

            else
            {
                WrongAnswers++;
                //if (DataManager.Io.TheoryPuzzleData.PuzzleDifficulty == PuzzleDifficulty.Challenge &&
                //    DataManager.Io.TheoryPuzzleData.HintsRemaining == 0)
                //{
                //    SetStateDirectly(new DialogStart_State(new StartPuzzle_Dialogue()));
                //}

                //DataManager.Io.TheoryPuzzleData.HintsRemaining--;

                //if (DataManager.Io.TheoryPuzzleData.HintsRemaining < 0)
                //{
                //    DataManager.Io.TheoryPuzzleData.FailedPuzzles++;
                //    FadeToState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));
                //}
            }
        }

        else if (go.transform.IsChildOf(Listen.GO.transform))
        {
            HintsUsed++;
            Audio.KBAudio.PlayNotes(SelectedClips(), null, Puzzle.ListenPlaybackMode);
        }

        else if (go.transform.IsChildOf(Skip.GO.transform))
        {
            Skipped++;
            FadeToState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));
        }

        SubmitAnswer.GO.SetActive(AllNotesSelected());
        Listen.GO.SetActive(AllNotesSelected());
        //Hint.SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining);
    }

    void EndPuzzleCallback()
    {
        ReadyForNextPuzzle = true;
        Desc.GO.SetActive(true);
        Desc.SetTextString("tap anywhere to continue...");
    }

    bool AllNotesSelected()
    {
        foreach (var key in Keyboard.SelectedKeys) if (key == null) return false;
        return true;
    }

    bool AllNotesCorrect()
    {
        if (Puzzle.NumOfNotes == 1 && PuzzleType == PuzzleType.Theory)
            return Keyboard.SelectedKeys[0].Key == Puzzle.Notes[0].NoteNameToKey();

        bool[] answered = new bool[Puzzle.Notes.Length];

        foreach (var key in Keyboard.SelectedKeys)
            for (int i = 0; i < Puzzle.Notes.Length; i++)
                if (key.KeyboardNoteName == Puzzle.Notes[i])
                {
                    answered[i] = true;
                    break;
                }

        foreach (bool b in answered) if (!b) return false;
        return true;
    }

    AudioClip[] QuestionClips()
    {
        AudioClip[] clips = new AudioClip[Puzzle.Notes.Length];

        for (int i = 0; i < Puzzle.Notes.Length; i++)
            clips[i] = AudioParser.GetAudioClipFromKey(Puzzle.Notes[i]);

        return clips;
    }

    AudioClip[] SelectedClips()
    {
        List<KeyboardNoteName> unsortedKeys = new();
        List<KeyboardNoteName> sortedKeys = new();

        for (int i = 0; i < Keyboard.SelectedKeys.Length; i++)
            unsortedKeys.Add(Keyboard.SelectedKeys[i].KeyboardNoteName);

        while (unsortedKeys.Count > 0)
        {
            KeyboardNoteName lowKey = unsortedKeys[0];
            foreach (KeyboardNoteName key in unsortedKeys)
            {
                lowKey = key < lowKey ? key : lowKey;
            }
            sortedKeys.Add(lowKey);
            unsortedKeys.Remove(lowKey);
        }

        AudioClip[] clips = new AudioClip[sortedKeys.Count];

        for (int i = 0; i < sortedKeys.Count; i++)
            clips[i] = AudioParser.GetAudioClipFromKey(sortedKeys[i]);

        return clips;
    }

    private Card _answer;
    public Card SubmitAnswer => _answer ??= new Card(nameof(SubmitAnswer), null)
        .SetTextString(nameof(SubmitAnswer).SpaceAfterCap())
        .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.6f, .6f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .ScaleImageSizeToTMP(1.2f)
        .SetImagePosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .SetImageToUILayer();

    private Card _skip;
    public Card Skip => _skip ??= new Card(nameof(Skip), null)
        .SetTextString(nameof(Skip))
        .SetTMPPosition(new Vector2(Cam.UIOrthoX, -Cam.UIOrthoY))
        .SetFontScale(.4f, .4f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .ScaleImageSizeToTMP(1.2f)
        .SetImagePosition(new Vector2(Cam.UIOrthoX, -Cam.UIOrthoY))
        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .SetImageToUILayer();

    private Card _desc;
    public Card Desc => _desc ??= new Card(nameof(Desc), null)
        .SetTextString(Puzzle.Desc)
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX, Cam.UIOrthoY))
        .SetFontScale(.5f, .5f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
        .AutoSizeTextContainer(true)
        .SetTextColor(Color.grey)
        .AllowWordWrap(false)
        .SetTMPRectPivot(0, .5f);

    private Card _hint;
    public Card Hint => _hint ??= new Card(nameof(Hint), null)
        .SetTextString(Puzzle.Clue)
        .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1.75f))
        .SetFontScale(.5f, .5f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .SetTextColor(Color.grey)
        .AllowWordWrap(false);

    private Card _question;
    public Card Question => _question ??= new Card(nameof(Question), null)
        .SetTextString(PuzzleType == PuzzleType.Aural ? "Listen to question" : Puzzle.Question)
        .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1.15f))
        .SetFontScale(.65f, .65f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        .ScaleImageSizeToTMP(1.2f)
        .SetImagePosition(new Vector2(0, Cam.UIOrthoY - 1.15f))
        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .SetImageToUILayer();

    private Card _listen;
    public Card Listen => _listen ??= new Card(nameof(Listen), null)
        .SetTextString("Listen to answer")
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.6f, .6f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        .ScaleImageSizeToTMP(1.2f)
        .SetImagePosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .SetImageToUILayer();
}




/*
 *hack this clickable works..
 *   private Card _listen;
    public Card Listen => _listen ??= new Card(nameof(Listen), null)
        .SetTextString("Listen to answer")
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.6f, .6f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        .ScaleImageSizeToTMP(1.2f)
        .SetImagePosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .SetImageToUILayer();
 * 
 * 
 * hack this clickable does not work
    private Card _listen;
    public Card Listen => _listen ??= new Card(nameof(Listen), null)
        .SetTextString("Listen to answer")
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.6f, .6f)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .SetImagePosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .ScaleImageSizeToTMP(1.2f)
        .SetImageToUILayer();
 */