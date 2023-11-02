
//using UnityEngine;
//using Audio;

//public class NoteIdentificationAuralPuzzle_State : State
//{
//    protected override void PrepareState(System.Action callback)
//    {
//        Keyboard = new(1);
//        PuzzleKey = Keyboard.Keys[Random.Range(0, Keyboard.Keys.Count)];
//        Hint.GO.SetActive(true);
//        Answer.GO.SetActive(Keyboard.SelectedKeys[0] != null);

//        DataManager.Io.TheoryPuzzleData.ResetHints();

//        _ = Question;
//        _ = Hint;
//        _ = Answer;
//        _ = Desc;

//        base.PrepareState(callback);
//    }

//    protected override void EngageState()
//    {
//        AudioManager.Io.KBAudio.PlayNote(AudioParser.GetAudioClipFromKey(PuzzleKey.KeyboardNoteName));
//    }

//    protected override void DisengageState()
//    {
//        AudioManager.Io.KBAudio.Stop();
//        Question.SelfDestruct();
//        Answer.SelfDestruct();
//        Keyboard.SelfDestruct();
//        Hint.SelfDestruct();
//        Desc.SelfDestruct();
//        DataManager.Io.SaveTheoryPuzzleData();
//    }

//    protected override void ClickedOn(GameObject go)
//    {
//        if (go.transform.IsChildOf(Keyboard.Parent.transform))
//        {
//            Keyboard.InteractWithKey(go);
//            Answer.GO.SetActive(Keyboard.SelectedKeys[0] != null);
//        }

//        else if (go.transform.IsChildOf(Answer.GO.transform))
//        {
//            if (Keyboard.SelectedKeys[0] == PuzzleKey)
//            {
//                DataManager.Io.TheoryPuzzleData.SolvedPuzzles++;
//                SetStateDirectly(PuzzleSelector.WeightedRandomPuzzleState());
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
//            if (DataManager.Io.TheoryPuzzleData.HintsRemaining > 0)
//            {
//                DataManager.Io.TheoryPuzzleData.HintsRemaining--;
//                AudioManager.Io.KBAudio.PlayNote(AudioParser.GetAudioClipFromKey(PuzzleKey.KeyboardNoteName));
//            }
//        }

//        Hint.SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining);
//        if (DataManager.Io.TheoryPuzzleData.HintsRemaining == 0)
//        {
//            Question.SetTextColor(new Color(.5f, .5f, .5f, .5f));
//            Question.SetImageColor(Color.clear);
//        }
//    }


//    Keyboard Keyboard;
//    KeyboardKey PuzzleKey;
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
//        .SetTextString("Sound Note")
//        .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1))
//        .SetFontScale(.7f, .7f)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
//        .AutoSizeFont(true)
//        .AutoSizeTextContainer(true)
//        .ScaleImageSizeToTMP(1.1f)
//        .SetImagePosition(new Vector2(0, Cam.UIOrthoY - 1))
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
//        .AllowWordWrap(false);

//    private Card _desc;
//    public Card Desc => _desc ??= new Card(nameof(Desc), null)
//        .SetTextString("Find the <b><i>note")
//        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1.25f, Cam.UIOrthoY))
//        .SetFontScale(.5f, .5f)
//        .AutoSizeFont(true)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
//        .AutoSizeTextContainer(true)
//        .SetTextColor(Color.grey)
//        .AllowWordWrap(false);


//}
