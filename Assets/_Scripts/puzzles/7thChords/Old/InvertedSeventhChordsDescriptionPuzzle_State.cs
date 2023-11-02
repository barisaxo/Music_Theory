//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using MusicTheory.Arithmetic;
//using MusicTheory.Keys;
//using MusicTheory.SeventhChords;
//using Audio;

//public class InvertedSeventhChordsDescriptionPuzzle_State : State
//{
//    protected override void PrepareState(System.Action callback)
//    {
//        inversion = (Inversion)Random.Range(0, 3);
//        Chord = Enumeration.All<SeventhChordEnum>()[Random.Range(0, Enumeration.Length<SeventhChordEnum>())];

//        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];

//        Key Third = Chord switch
//        {
//            MajorSeventh or DominantSeventh => Root.GetKeyAbove(new MusicTheory.Intervals.M3()),
//            _ => Root.GetKeyAbove(new MusicTheory.Intervals.mi3()),
//        };

//        Key Fifth = Chord switch
//        {
//            DiminishedSeventh or HalfDiminishedSeventh => Root.GetKeyAbove(new MusicTheory.Intervals.d5()),
//            _ => Root.GetKeyAbove(new MusicTheory.Intervals.P5()),
//        };

//        Key Seventh = Chord switch
//        {
//            MajorSeventh or MinorMajorSeventh => Root.GetKeyAbove(new MusicTheory.Intervals.M7()),
//            DiminishedSeventh => Root.GetKeyAbove(new MusicTheory.Intervals.d7()),
//            _ => Root.GetKeyAbove(new MusicTheory.Intervals.mi7()),
//        };

//        Bass = (inversion switch
//        {
//            Inversion.First => Third,
//            Inversion.Second => Fifth,
//            _ => Seventh
//        }).GetKeyboardNoteName();

//        Tenor = (inversion switch
//        {
//            Inversion.First => Fifth,
//            Inversion.Second => Seventh,
//            _ => Root
//        }).GetKeyboardNoteName();

//        Alto = (inversion switch
//        {
//            Inversion.First => Seventh,
//            Inversion.Second => Root,
//            _ => Third
//        }).GetKeyboardNoteName();

//        Soprano = (inversion switch
//        {
//            Inversion.First => Root,
//            Inversion.Second => Third,
//            _ => Fifth
//        }).GetKeyboardNoteName();

//        Tenor = Tenor < Bass ? Tenor + 12 : Tenor;
//        Alto = Alto < Bass ? Alto + 12 : Alto;
//        Soprano = Soprano < Bass ? Soprano + 12 : Soprano;

//        Keyboard = new(4, (inversion switch
//        {
//            Inversion.First => Third,
//            Inversion.Second => Fifth,
//            _ => Seventh
//        }).GetKeyboardNoteName());

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
//                if (key.KeyboardNoteName == Bass) { hasB = true; }
//                else if (key.KeyboardNoteName == Tenor) { hasT = true; }
//                else if (key.KeyboardNoteName == Alto) { hasA = true; }
//                else if (key.KeyboardNoteName == Soprano) { hasS = true; }
//            }

//            if (hasB && hasT && hasA && hasS)
//            {
//                DataManager.Io.TheoryPuzzleData.SolvedPuzzles++;
//                //SetStateDirectly(RandomPuzzleSelector.GetRandomPuzzleState());

//                SetStateDirectly(new InvertedSeventhChordsDescriptionPuzzle_State());
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

//    enum Inversion { First, Second, Third }
//    Inversion inversion;
//    Keyboard Keyboard;
//    SeventhChord Chord;
//    KeyboardNoteName Bass;
//    KeyboardNoteName Tenor;
//    KeyboardNoteName Alto;
//    KeyboardNoteName Soprano;

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
//        .SetTextString(Chord.Description.SpaceAfterCap() + " " + nameof(Chord) + " " + InversionDescription())
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
//        .SetTextString("Build the <b><i>inverted seventh chord")
//        .SetTMPPosition(new Vector2(-Cam.UIOrthoX, Cam.UIOrthoY))
//        .SetFontScale(.5f, .5f)
//        .AutoSizeFont(true)
//        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
//        .AutoSizeTextContainer(true)
//        .SetTextColor(Color.grey)
//        .AllowWordWrap(false)
//        .SetTMPRectPivot(0, .5f);

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
//        Inversion.First => "in first inversion",
//        Inversion.Second => "in second inversion",
//        _ => "in third inversion"
//    };
//}
