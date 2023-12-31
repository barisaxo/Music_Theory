//using UnityEngine;
//using MusicTheory.Arithmetic;
//using MusicTheory.Keys;
//using MusicTheory.Triads;
//using Audio;

//public class TriadDescriptionPuzzle_State : State
//{
//    protected override void PrepareState(System.Action callback)
//    {
//        Triad = Random.Range(0, 4) switch
//        {
//            2 => new Minor(),
//            3 => new Major(),
//            1 => new Diminished(),
//            0 => new Augmented(),
//            _ => throw new System.ArgumentOutOfRangeException()
//        };

//        Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];
//        Keyboard = new(3, Root.GetKeyboardNoteName());

//        Third = Triad switch
//        {
//            Major or Augmented => Root.GetKeyAbove(new MusicTheory.Intervals.M3()),
//            _ => Root.GetKeyAbove(new MusicTheory.Intervals.mi3()),
//        };

//        Fifth = Triad switch
//        {
//            Augmented => Root.GetKeyAbove(new MusicTheory.Intervals.A5()),
//            Diminished => Root.GetKeyAbove(new MusicTheory.Intervals.d5()),
//            _ => Root.GetKeyAbove(new MusicTheory.Intervals.P5()),
//        };

//        DataManager.Io.TheoryPuzzleData.ResetHints();
//        _ = Question;
//        _ = Desc;
//        _ = Hint;
//        Answer.GO.SetActive(false);

//        base.PrepareState(callback);
//    }

//    protected override void EngageState()
//    {
//    }

//    protected override void DisengageState()
//    {
//        AudioManager.Io.KBAudio.Stop();
//        Question.SelfDestruct();
//        Answer.SelfDestruct();
//        Keyboard.SelfDestruct();
//        Desc.SelfDestruct();
//        Hint.SelfDestruct();
//        DataManager.Io.SaveTheoryPuzzleData();
//    }

//    protected override void ClickedOn(GameObject go)
//    {
//        if (go.transform.IsChildOf(Keyboard.Parent.transform))
//        {
//            Keyboard.InteractWithKey(go);
//            Answer.GO.SetActive(Keyboard.SelectedKeys[1] != null && Keyboard.SelectedKeys[2] != null);
//        }

//        else if (go.transform.IsChildOf(Answer.GO.transform))
//        {
//            if ((Keyboard.SelectedKeys[1].KeyboardNoteName.NoteNameToKey().Id == Third.Id &&
//                Keyboard.SelectedKeys[2].KeyboardNoteName.NoteNameToKey().Id == Fifth.Id) ||
//                (Keyboard.SelectedKeys[1].KeyboardNoteName.NoteNameToKey().Id == Fifth.Id &&
//                Keyboard.SelectedKeys[2].KeyboardNoteName.NoteNameToKey().Id == Third.Id))
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

//        Hint.SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining);
//    }

//    Keyboard Keyboard;
//    Triad Triad;
//    Key Root;
//    Key Third;
//    Key Fifth;

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
//        .SetTextString(Triad.Description + " " + nameof(Triad))
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

//    private Card _desc;
//    public Card Desc => _desc ??= new Card(nameof(Desc), null)
//        .SetTextString("Build the <b><i>triad")
//        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1.25f, Cam.UIOrthoY))
//        .SetFontScale(.5f, .5f)
//        .AutoSizeFont(true)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
//        .AutoSizeTextContainer(true)
//        .SetTextColor(Color.grey)
//        .AllowWordWrap(false);

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

//}
