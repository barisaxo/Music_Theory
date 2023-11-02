//using UnityEngine;
//using MusicTheory.Arithmetic;
//using MusicTheory.Keys;
//using MusicTheory.Triads;
//using Audio;

//public class TriadInversionDescriptionPuzzle_State : State
//{
//    protected override void PrepareState(System.Action callback)
//    {
//        inversion = Random.Range(1, 3) switch { 0 => Inversion.root, 1 => Inversion.first, _ => Inversion.second };
//        Triad = Random.Range(0, 4) switch { 0 => new Minor(), 1 => new Major(), 2 => new Diminished(), _ => new Augmented() };

//        Root = ((Key)Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())]).GetKeyboardNoteName();

//        Third = Triad switch
//        {
//            Major or Augmented => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.M3()).GetKeyboardNoteName(),
//            _ => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.mi3()).GetKeyboardNoteName(),
//        };

//        Fifth = Triad switch
//        {
//            Augmented => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.A5()).GetKeyboardNoteName(),
//            Diminished => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.d5()).GetKeyboardNoteName(),
//            _ => Root.NoteNameToKey().GetKeyAbove(new MusicTheory.Intervals.P5()).GetKeyboardNoteName(),
//        };

//        Third += Third < Root ? 12 : 0;
//        Fifth += Fifth < Root ? 12 : 0;

//        Keyboard = new(3, (inversion switch
//        {
//            Inversion.root => Root,
//            Inversion.first => Third,
//            _ => Fifth
//        }));

//        DataManager.Io.TheoryPuzzleData.ResetHints();
//        _ = Question;
//        Answer.GO.SetActive(false);
//        _ = Desc;
//        _ = Hint;

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
//            bool has1 = false, has3 = false, has5 = false;
//            foreach (KeyboardKey key in Keyboard.SelectedKeys)
//            {
//                if (key.KeyboardNoteName == Root) has1 = true;
//                else if (key.KeyboardNoteName == Third) has3 = true;
//                else if (key.KeyboardNoteName == Fifth) has5 = true;
//            }

//            if (has1 && has3 && has5)
//            {
//                DataManager.Io.TheoryPuzzleData.SolvedPuzzles++;
//                //SetStateDirectly(PuzzleSelector.WeightedRandomPuzzleState());
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
//                    //SetStateDirectly(PuzzleSelector.WeightedRandomPuzzleState());
//                }
//            }
//        }

//        Hint.SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining);
//    }

//    enum Inversion { root, first, second }
//    Inversion inversion;
//    Keyboard Keyboard;
//    Triad Triad;
//    KeyboardNoteName Root;
//    KeyboardNoteName Third;
//    KeyboardNoteName Fifth;

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
//        .SetTextString(Triad.Description + " " + nameof(Triad) + " " + InversionDescription())
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
//        .SetTextString("Build the <b><i>inverted triad")
//        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1.25f, Cam.UIOrthoY))
//        .SetFontScale(.5f, .5f)
//        .AutoSizeFont(true)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
//        .AutoSizeTextContainer(true)
//        .SetTextColor(Color.grey)
//        .AllowWordWrap(false)
//        .SetTMPRectPivot(new Vector2(.5f, 0));

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

//    string InversionDescription() => inversion switch
//    {
//        Inversion.root => "in root position",
//        Inversion.first => "in first inversion",
//        _ => "in second inversion"
//    };

//}
