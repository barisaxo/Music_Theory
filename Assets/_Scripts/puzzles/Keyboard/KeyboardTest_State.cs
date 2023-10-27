//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Intervals;
using MusicTheory.Keys;
using MusicTheory.Arithmetic;

public class KeyboardTest_State : State
{
    protected override void PrepareState(System.Action callback)
    {
        Bottom = Enumeration.ListAll<KeyEnum>()[Random.Range(0, Enumeration.ListAll<KeyEnum>().Count)];
        Top = Enumeration.ListAll<KeyEnum>()[Random.Range(0, Enumeration.ListAll<KeyEnum>().Count)];

        Keyboard = new(2, Bottom.GetKeyboardNote());

        Interval = Bottom.GetInterval(Top);
        _ = Question;

        Answer.GO.SetActive(Keyboard.SelectedKeys[1] != null);
        base.PrepareState(callback);
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
        .SetImageToUILayer();

    private Card _question;
    public Card Question => _question ??= new Card(nameof(Question), null)
        .SetTextString(Interval.Description)
        .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .SetFontScale(.7f, .7f)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true);

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
                SetStateDirectly(RandomPuzzleSelector.GetRandomPuzzleState());
            }
            else
            {
                Debug.Log("narp?");
            }
        }
    }

    protected override void DisengageState()
    {
        Question.SelfDestruct();
        Answer.SelfDestruct();
        Keyboard.SelfDestruct();
    }


}
