////using System;
//using UnityEngine;
//using MusicTheory.Intervals;
//using MusicTheory.Keys;
//using MusicTheory.Arithmetic;
//using Audio;

//public class IntervalAuralPuzzle_State : State
//{
//    protected override void PrepareState(System.Action callback)
//    {
//        Bottom = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];
//        Top = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];

//        BottomKey = Bottom.GetKeyboardNoteName();
//        TopKey = Top.GetKeyboardNoteName();
//        TopKey += Top.Id <= Bottom.Id ? 12 : 0;

//        Debug.Log(Bottom.Name + " : " + BottomKey.ToString() + ", " + Top.Name + " : " + TopKey.ToString());

//        BottomAC = AudioParser.GetAudioClipFromKey(BottomKey);
//        TopAC = AudioParser.GetAudioClipFromKey(TopKey);

//        Keyboard = new(2, BottomKey);
//        Interval = Bottom.GetInterval(Top);

//        DataManager.Io.TheoryPuzzleData.ResetHints();
//        _ = Question;
//        _ = Hint;
//        _ = Desc;
//        Listen.GO.SetActive(false);
//        Answer.GO.SetActive(false);

//        base.PrepareState(callback);
//    }

//    protected override void EngageState()
//    {
//        AudioManager.Io.KBAudio.PlayInterval(BottomAC, TopAC);
//    }

//    protected override void DisengageState()
//    {
//        AudioManager.Io.KBAudio.Stop();
//        Question.SelfDestruct();
//        Answer.SelfDestruct();
//        Keyboard.SelfDestruct();
//        Listen.SelfDestruct();
//        Hint.SelfDestruct();
//        Desc.SelfDestruct();
//        DataManager.Io.SaveTheoryPuzzleData();
//    }

//    protected override void ClickedOn(GameObject go)
//    {
//        if (go.transform.IsChildOf(Keyboard.Parent.transform))
//        {
//            Keyboard.InteractWithKey(go);
//            Answer.GO.SetActive(Keyboard.SelectedKeys[1] != null);
//        }

//        else if (go.transform.IsChildOf(Answer.GO.transform))
//        {
//            if (Keyboard.SelectedKeys[0].Key.GetInterval(Keyboard.SelectedKeys[1].Key).Id == Interval.Id)
//            {
//                DataManager.Io.TheoryPuzzleData.SolvedPuzzles++;
//                //SetStateDirectly(RandomPuzzleSelector.GetRandomPuzzleState());
//                SetStateDirectly(new IntervalAuralPuzzle_State());
//            }

//            else
//            {
//                if (DataManager.Io.TheoryPuzzleData.PuzzleDifficulty == PuzzleDifficulty.Challenge &&
//                    DataManager.Io.TheoryPuzzleData.HintsRemaining == 0)
//                {
//                    SetStateDirectly(new DialogStart_State(new StartPuzzle_Dialogue()));
//                }

//                DataManager.Io.TheoryPuzzleData.WrongAnswers++;
//                DataManager.Io.TheoryPuzzleData.HintsRemaining--;

//                if (DataManager.Io.TheoryPuzzleData.HintsRemaining < 0)
//                {
//                    DataManager.Io.TheoryPuzzleData.FailedPuzzles++;
//                    SetStateDirectly(PuzzleSelector.WeightedRandomPuzzleState());
//                }
//            }
//        }

//        else if (go.transform.IsChildOf(Question.GO.transform))
//        {
//            if (DataManager.Io.TheoryPuzzleData.HintsRemaining <= 0) return;
//            DataManager.Io.TheoryPuzzleData.HintsRemaining--;
//            AudioManager.Io.KBAudio.PlayInterval(BottomAC, TopAC);
//        }

//        else if (go.transform.IsChildOf(Listen.GO.transform))
//        {
//            if (DataManager.Io.TheoryPuzzleData.HintsRemaining > 0)
//            {
//                DataManager.Io.TheoryPuzzleData.HintsRemaining--;

//                KeyboardNoteName top = Keyboard.SelectedKeys[1].KeyboardNoteName;
//                //Key tempTop = Keyboard.SelectedKeys[1].Key;
//                //if (tempTop.Id <= Bottom.Id) top += 12;

//                AudioManager.Io.KBAudio.PlayInterval(AudioParser.GetAudioClipFromKey(top), BottomAC);
//            }
//        }

//        Hint.SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining);

//        if (DataManager.Io.TheoryPuzzleData.HintsRemaining == 0)
//        {
//            Question.SetTextColor(new Color(.5f, .5f, .5f, .5f));
//            Question.SetImageColor(Color.clear);
//        }

//        Listen.GO.SetActive(Keyboard.SelectedKeys[1] != null && DataManager.Io.TheoryPuzzleData.HintsRemaining > 0);
//    }


//    Interval Interval;
//    Keyboard Keyboard;
//    Key Bottom;
//    Key Top;
//    KeyboardNoteName BottomKey;
//    KeyboardNoteName TopKey;
//    AudioClip BottomAC;
//    AudioClip TopAC;
//    readonly AudioParser AudioParser = new();

//    private Card _answer;
//    public Card Answer => _answer ??= new Card(nameof(Answer), null)
//        .SetTextString(nameof(Answer))
//        .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
//        .SetFontScale(.6f, .6f)
//        .AutoSizeFont(true)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
//        .AutoSizeTextContainer(true)
//        .ScaleImageSizeToTMP(1.2f)
//        .SetImagePosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
//        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
//        .ImageClickable()
//        .AllowWordWrap(false)
//        .SetImageToUILayer();

//    private Card _question;
//    public Card Question => _question ??= new Card(nameof(Question), null)
//        .SetTextString("Sound Interval")
//        .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1))
//        .SetFontScale(.7f, .7f)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
//        .AutoSizeFont(true)
//        .AutoSizeTextContainer(true)
//        .ScaleImageSizeToTMP(1.2f)
//        .SetImagePosition(new Vector2(0, Cam.UIOrthoY - 1))
//        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
//        .ImageClickable()
//        .AllowWordWrap(false)
//        .SetImageToUILayer();

//    private Card _listen;
//    public Card Listen => _listen ??= new Card(nameof(Listen), null)
//        .SetTextString("Listen")
//        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
//        .SetFontScale(.6f, .6f)
//        .AutoSizeFont(true)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
//        .AutoSizeTextContainer(true)
//        .ScaleImageSizeToTMP(1.2f)
//        .SetImagePosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
//        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
//        .ImageClickable()
//        .AllowWordWrap(false)
//        .SetImageToUILayer();

//    private Card _hint;
//    public Card Hint => _hint ??= new Card(nameof(Hint), null)
//        .SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining)
//        .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1.65f))
//        .SetFontScale(.5f, .5f)
//        .AutoSizeFont(true)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
//        .AutoSizeTextContainer(true)
//        .SetTextColor(Color.grey)
//        .AllowWordWrap(false)
//        .AllowWordWrap(false);

//    private Card _desc;
//    public Card Desc => _desc ??= new Card(nameof(Desc), null)
//        .SetTextString("Find the <b><i>interval")
//        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1.25f, Cam.UIOrthoY))
//        .SetFontScale(.5f, .5f)
//        .AutoSizeFont(true)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
//        .AutoSizeTextContainer(true)
//        .SetTextColor(Color.grey)
//        .AllowWordWrap(false);


//}
