
//using UnityEngine;
//using MusicTheory.Arithmetic;
//using MusicTheory.Keys;
//using MusicTheory.SeventhChords;
//using Audio;


//public class SeventhChordAuralPuzzle_State : State
//{
//    protected override void PrepareState(System.Action callback)
//    {
//        Chord = Enumeration.All<SeventhChordEnum>()[Random.Range(0, Enumeration.Length<SeventhChordEnum>())];

//        Root = ((Key)Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())]).GetKeyboardNoteName();

//        Third = Chord switch
//        {
//            MajorSeventh or DominantSeventh => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.M3()).GetKeyboardNoteName(),
//            _ => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.mi3()).GetKeyboardNoteName(),
//        };

//        Fifth = Chord switch
//        {
//            DiminishedSeventh or HalfDiminishedSeventh => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.d5()).GetKeyboardNoteName(),
//            _ => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.P5()).GetKeyboardNoteName(),
//        };

//        Seventh = Chord switch
//        {
//            MajorSeventh or MinorMajorSeventh => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.M7()).GetKeyboardNoteName(),
//            DiminishedSeventh => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.d7()).GetKeyboardNoteName(),
//            _ => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.mi7()).GetKeyboardNoteName(),
//        };

//        Third += Third < Root ? 12 : 0;
//        Fifth += Fifth < Root ? 12 : 0;
//        Seventh += Seventh < Root ? 12 : 0;

//        Keyboard = new(4, Root);
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
//        AudioManager.Io.KBAudio.PlaySeventhChord(
//           AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[0].KeyboardNoteName),
//           AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[1].KeyboardNoteName),
//           AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[2].KeyboardNoteName),
//           AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[3].KeyboardNoteName));
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
//            Answer.GO.SetActive(
//                Keyboard.SelectedKeys[1] != null &&
//                Keyboard.SelectedKeys[2] != null &&
//                Keyboard.SelectedKeys[3] != null);
//        }

//        else if (go.transform.IsChildOf(Answer.GO.transform))
//        {
//            bool hasB = false, hasT = false, hasA = false, hasS = false;

//            foreach (var key in Keyboard.SelectedKeys)
//            {
//                if (key.KeyboardNoteName == Root) { hasB = true; }
//                else if (key.KeyboardNoteName == Third) { hasT = true; }
//                else if (key.KeyboardNoteName == Fifth) { hasA = true; }
//                else if (key.KeyboardNoteName == Seventh) { hasS = true; }
//            }

//            if (hasB && hasT && hasA && hasS)
//            {
//                DataManager.Io.TheoryPuzzleData.SolvedPuzzles++;
//                //SetStateDirectly(RandomPuzzleSelector.GetRandomPuzzleState());

//                SetStateDirectly(new SeventhChordAuralPuzzle_State());

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

//            AudioManager.Io.KBAudio.PlaySeventhChord(
//                AudioParser.GetAudioClipFromKey(Root),
//                AudioParser.GetAudioClipFromKey(Third),
//                AudioParser.GetAudioClipFromKey(Fifth),
//                AudioParser.GetAudioClipFromKey(Seventh));
//        }

//        else if (go.transform.IsChildOf(Listen.GO.transform))
//        {
//            if (DataManager.Io.TheoryPuzzleData.HintsRemaining > 0)
//            {
//                DataManager.Io.TheoryPuzzleData.HintsRemaining--;

//                AudioManager.Io.KBAudio.PlaySeventhChord(
//                    AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[0].KeyboardNoteName),
//                    AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[1].KeyboardNoteName),
//                    AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[2].KeyboardNoteName),
//                    AudioParser.GetAudioClipFromKey(Keyboard.SelectedKeys[3].KeyboardNoteName));
//            }
//        }

//        if (DataManager.Io.TheoryPuzzleData.HintsRemaining == 0)
//        {
//            Question.SetTextColor(new Color(.5f, .5f, .5f, .5f));
//            Question.SetImageColor(Color.clear);
//        }

//        Hint.SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining);

//        Listen.GO.SetActive(Keyboard.SelectedKeys[1] != null &&
//                            Keyboard.SelectedKeys[2] != null &&
//                            Keyboard.SelectedKeys[3] != null &&
//                            DataManager.Io.TheoryPuzzleData.HintsRemaining > 0);
//    }


//    Keyboard Keyboard;
//    SeventhChord Chord;
//    KeyboardNoteName Root;
//    KeyboardNoteName Third;
//    KeyboardNoteName Fifth;
//    KeyboardNoteName Seventh;
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
//        .SetTextString("Sound seventh chord")
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
//        .SetTextString("Find the <b><i>seventh chord")
//        .SetTMPPosition(new Vector2(-Cam.UIOrthoX, Cam.UIOrthoY))
//        .SetFontScale(.5f, .5f)
//        .AutoSizeFont(true)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
//        .AutoSizeTextContainer(true)
//        .SetTextColor(Color.grey)
//        .AllowWordWrap(false)
//        .SetTMPRectPivot(0, .5f);


//}
