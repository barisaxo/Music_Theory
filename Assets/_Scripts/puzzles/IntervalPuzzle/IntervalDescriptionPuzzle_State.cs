//using System;
using UnityEngine;
using MusicTheory.Intervals;
using MusicTheory.Keys;
using MusicTheory.Arithmetic;
using Audio;

public class IntervalDescriptionPuzzle_State : State
{
    protected override void PrepareState(System.Action callback)
    {
        Bottom = Enumeration.ListAll<KeyEnum>()[Random.Range(0, Enumeration.ListAll<KeyEnum>().Count)];
        Top = Enumeration.ListAll<KeyEnum>()[Random.Range(0, Enumeration.ListAll<KeyEnum>().Count)];

        Keyboard = new(2, Bottom.GetKeyboardNote());

        Interval = Bottom.GetInterval(Top);

        DataManager.Io.TheoryPuzzleData.ResetHints();
        _ = Question;
        _ = Desc;
        _ = Hint;
        Answer.GO.SetActive(false);

        base.PrepareState(callback);
    }

    protected override void DisengageState()
    {
        DataManager.Io.SaveTheoryPuzzleData();
        AudioManager.Io.KBAudio.Stop();
        Question.SelfDestruct();
        Answer.SelfDestruct();
        Keyboard.SelfDestruct();
        Desc.SelfDestruct();
        Hint.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(Keyboard.Parent.transform))
        {
            Keyboard.InteractWithKey(go);
            Answer.GO.SetActive(Keyboard.SelectedKeys[1] != null);
        }

        else if (go.transform.IsChildOf(Answer.GO.transform))
        {
            if (Keyboard.SelectedKeys[0].KeyboardNoteName.NoteNameToKey()
                .GetInterval(Keyboard.SelectedKeys[1].KeyboardNoteName.NoteNameToKey()).Id == Interval.Id)
            {
                DataManager.Io.TheoryPuzzleData.SolvedPuzzles++;
                SetStateDirectly(RandomPuzzleSelector.GetRandomPuzzleState());
            }
            else
            {
                if (DataManager.Io.TheoryPuzzleData.PuzzleDifficulty == PuzzleDifficulty.Challenge &&
                    DataManager.Io.TheoryPuzzleData.HintsRemaining == 0)
                {
                    SetStateDirectly(new DialogStart_State(new StartPuzzle_Dialogue()));
                }

                DataManager.Io.TheoryPuzzleData.WrongAnswers++;
                DataManager.Io.TheoryPuzzleData.HintsRemaining--;

                if (DataManager.Io.TheoryPuzzleData.HintsRemaining < 0)
                {
                    DataManager.Io.TheoryPuzzleData.FailedPuzzles++;
                    SetStateDirectly(RandomPuzzleSelector.GetRandomPuzzleState());
                }
            }
        }

        Hint.SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining);
    }

    Interval Interval;
    Keyboard Keyboard;
    Key Bottom;
    Key Top;

    private Card _answer;
    public Card Answer => _answer ??= new Card(nameof(Answer), null)
        .SetTextString(nameof(Answer))
        .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.6f, .6f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        .ScaleImageSizeToTMP(1.2f)
        .SetImagePosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .SetImageToUILayer();

    private Card _question;
    public Card Question => _question ??= new Card(nameof(Question), null)
        .SetTextString(Interval.Description)
        .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .SetFontScale(.7f, .7f)
        .AutoSizeFont(true)
        .AllowWordWrap(false)
        .AutoSizeTextContainer(true);

    private Card _desc;
    public Card Desc => _desc ??= new Card(nameof(Desc), null)
        .SetTextString("Build the <b><i>interval")
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1.25f, Cam.UIOrthoY))
        .SetFontScale(.5f, .5f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
        .AutoSizeTextContainer(true)
        .SetTextColor(Color.grey)
        .AllowWordWrap(false);

    private Card _hint;
    public Card Hint => _hint ??= new Card(nameof(Hint), null)
        .SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining)
        .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1.65f))
        .SetFontScale(.5f, .5f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .SetTextColor(Color.grey)
        .AllowWordWrap(false);


}