//using UnityEngine;
//using MusicTheory.Arithmetic;
//using MusicTheory.Keys;
//using MusicTheory.Triads;
//using Audio;

//public class TriadAuralPuzzle_State : State
//{

//    protected override void PrepareState(System.Action callback)
//    {
//        Triad = Random.Range(0, 4) switch { 0 => new Minor(), 1 => new Major(), 2 => new Diminished(), _ => new Augmented() };
//        Debug.Log(Triad.Description);

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

//        KeyboardNoteName middle = Third.GetKeyboardNoteName();
//        KeyboardNoteName top = Fifth.GetKeyboardNoteName();

//        middle += Third.Id < Root.Id ? 12 : 0;
//        top += Fifth.Id < Root.Id ? 12 : 0;

//        BottomAC = AudioParser.GetAudioClipFromKey(Root.GetKeyboardNoteName());
//        MiddleAC = AudioParser.GetAudioClipFromKey(middle);
//        TopAC = AudioParser.GetAudioClipFromKey(top);

//        DataManager.Io.TheoryPuzzleData.ResetHints();
//        _ = Question;
//        _ = Desc;
//        _ = Hint;
//        Listen.GO.SetActive(false);
//        Answer.GO.SetActive(false);

//        base.PrepareState(callback);
//    }

//    protected override void EngageState()
//    {
//        AudioManager.Io.KBAudio.PlayVertical(new AudioClip[] { BottomAC, MiddleAC, TopAC }, null);
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
//        Debug.Log(go.name);
//        if (go.transform.IsChildOf(Keyboard.Parent.transform))
//        {
//            Keyboard.InteractWithKey(go);
//            Answer.GO.SetActive(Keyboard.SelectedKeys[1] != null && Keyboard.SelectedKeys[2] != null);
//        }
//        else if (go.transform.IsChildOf(Answer.GO.transform))
//        {
//            if ((Keyboard.SelectedKeys[1].KeyboardNoteName.NoteNameToKey().Id == Third.Id ||
//                Keyboard.SelectedKeys[1].KeyboardNoteName.NoteNameToKey().Id == Fifth.Id) &&
//                (Keyboard.SelectedKeys[2].KeyboardNoteName.NoteNameToKey().Id == Fifth.Id ||
//                Keyboard.SelectedKeys[2].KeyboardNoteName.NoteNameToKey().Id == Third.Id))
//            {
//                DisableInput();
//                DataManager.Io.TheoryPuzzleData.SolvedPuzzles++;
//                Question.SetTextString(Root.Name + " " + Triad.Description);
//                AudioManager.Io.KBAudio.PlayHorAndVert(new AudioClip[] { BottomAC, MiddleAC, TopAC }, EndState);
//            }
//            else
//            {
//                if (DataManager.Io.TheoryPuzzleData.PuzzleDifficulty == PuzzleDifficulty.Challenge &&
//                    DataManager.Io.TheoryPuzzleData.HintsRemaining == 0)
//                {
//                    SetStateDirectly(new DialogStart_State(new StartPuzzle_Dialogue()));
//                    return;
//                }

//                DataManager.Io.TheoryPuzzleData.WrongAnswers++;
//                DataManager.Io.TheoryPuzzleData.HintsRemaining--;

//                AudioManager.Io.KBAudio.PlayVertical(new AudioClip[] {
//                    BottomAC,
//                    AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[1].KeyboardNoteName),
//                    AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[2].KeyboardNoteName) },
//                    null);

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

//            AudioManager.Io.KBAudio.PlayVertical(new AudioClip[] { BottomAC, MiddleAC, TopAC }, null);
//        }

//        else if (go.transform.IsChildOf(Listen.GO.transform))
//        {
//            if (DataManager.Io.TheoryPuzzleData.HintsRemaining > 0)
//            {
//                DataManager.Io.TheoryPuzzleData.HintsRemaining--;
//                AudioManager.Io.KBAudio.PlayVertical(new AudioClip[] {
//                    BottomAC,
//                    AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[1].KeyboardNoteName),
//                    AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[2].KeyboardNoteName) },
//                    null);
//            }
//        }

//        if (DataManager.Io.TheoryPuzzleData.HintsRemaining == 0)
//        {
//            Question.SetTextColor(new Color(.5f, .5f, .5f, .5f));
//            Question.SetImageColor(Color.clear);
//        }

//        Hint.SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining);
//        Listen.GO.SetActive(Keyboard.SelectedKeys[1] != null && Keyboard.SelectedKeys[2] != null &&
//            DataManager.Io.TheoryPuzzleData.HintsRemaining > 0);
//    }

//    void EndState()
//    {
//        SetStateDirectly(new TriadAuralPuzzle_State());
//        //SetStateDirectly(RandomPuzzleSelector.GetRandomPuzzleState());
//    }

//    Keyboard Keyboard;
//    Triad Triad;
//    Key Root;
//    Key Third;
//    Key Fifth;
//    AudioClip BottomAC;
//    AudioClip MiddleAC;
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
//        .SetTextString("Sound Triad")
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
//        .AllowWordWrap(false);

//    private Card _desc;
//    public Card Desc => _desc ??= new Card(nameof(Desc), null)
//        .SetTextString("Find the <b><i>triad")
//        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1.25f, Cam.UIOrthoY))
//        .SetFontScale(.5f, .5f)
//        .AutoSizeFont(true)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
//        .AutoSizeTextContainer(true)
//        .SetTextColor(Color.grey)
//        .AllowWordWrap(false);


//}
