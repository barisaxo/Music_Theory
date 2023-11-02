//using UnityEngine;
//using MusicTheory.Arithmetic;
//using MusicTheory.Keys;
//using MusicTheory.SeventhChords;
//using MusicTheory.Scales;
//using MusicTheory.Modes;
//using MusicTheory.Steps;
//using Audio;

//public class ModeDescriptionPuzzle_State : State
//{
//    protected override void PrepareState(System.Action callback)
//    {
//        Scale = WeightedRandomScale();
//        Mode = Scale.Modes[Random.Range(0, Scale.Modes.Length)];
//        KeyboardNoteName Root = ((Key)Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())]).GetKeyboardNoteName();
//        Notes = new KeyboardNoteName[Scale.ScaleDegrees.Length + 1];

//        for (int i = 0; i < Notes.Length - 1; i++)
//        {
//            int modalIndex = (Mode.Enum.Id + i) % Scale.ScaleDegrees.Length;
//            Notes[i] = Root.NoteNameToKey().GetKeyAbove(Scale.ScaleDegrees[modalIndex].AsInterval()).GetKeyboardNoteName();
//        }
//        for (int i = 0; i < Notes.Length - 1; i++)
//        {
//            Notes[i] += Notes[i] < Notes[0] ? 12 : 0;
//            Debug.Log(Notes[i].ToString());
//        }
//        Notes[^1] = Notes[0] + 12;

//        Keyboard = new(Scale.ScaleDegrees.Length + 1, Notes[0]);

//        DataManager.Io.TheoryPuzzleData.ResetHints();

//        _ = Question;
//        _ = Desc;
//        _ = Hint;
//        Answer.GO.SetActive(false);

//        base.PrepareState(callback);
//    }

//    private Scale WeightedRandomScale()
//    {
//        return Random.Range(0, 50) switch
//        {
//            < 10 => new Major(),
//            < 19 => new JazzMinor(),
//            < 29 => new HarmonicMinor(),
//            < 37 => new Pentatonic(),
//            < 41 => new MusicTheory.Scales.Diminished(),
//            < 45 => new Diminished6th(),
//            < 48 => new MusicTheory.Scales.WholeTone(),
//            _ => new MusicTheory.Scales.Chromatic(),
//        };
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
//            bool all = true;
//            foreach (var k in Keyboard.SelectedKeys) if (k == null) { all = false; break; }
//            Answer.GO.SetActive(all);
//        }

//        else if (go.transform.IsChildOf(Answer.GO.transform))
//        {
//            bool[] answered = new bool[Notes.Length];
//            bool allAnswered = true;
//            foreach (var key in Keyboard.SelectedKeys)
//            {
//                for (int i = 0; i < Notes.Length; i++)
//                {
//                    if (key.KeyboardNoteName == Notes[i])
//                    {
//                        answered[i] = true;
//                        break;
//                    }
//                }
//            }
//            foreach (bool b in answered) { if (!b) { allAnswered = false; break; } }

//            if (allAnswered)
//            {
//                DataManager.Io.TheoryPuzzleData.SolvedPuzzles++;
//                //SetStateDirectly(RandomPuzzleSelector.GetRandomPuzzleState());
//                //AudioManager.Io.KBAudio()
//                FadeToState(new ModeDescriptionPuzzle_State());
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

//    Scale Scale;
//    Mode Mode;
//    KeyboardNoteName[] Notes;
//    Keyboard Keyboard;

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
//        .SetTextString(GetMajorModeName() + Mode.Enum.Name + " " + nameof(Mode) + " of the " + Scale.Description.SpaceAfterCap() + " " + nameof(Scale) + GetSteps())
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
//        .SetTextString("Build the <b><i>mode")
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

//    private string GetMajorModeName()
//    {
//        return Scale is Major ? Mode.Name + ": the " : "";
//    }

//    private string GetSteps()
//    {
//        string temp = "\n";
//        for (int i = 0; i < Scale.Steps.Length; i++)
//        {
//            int modalIndex = (Mode.Enum.Id + i) % Scale.ScaleDegrees.Length;
//            temp += Scale.Steps[modalIndex].Name + " ";
//        }
//        return temp;
//    }

//}



///*
// * 
// *         Notes = new KeyboardNoteName[Scale.ScaleDegrees.Length];
//        //Notes[0] = Root;
//        Debug.Log("Scale: " + Scale.Name + ", Mode: " + Mode.Enum.Name + ", Parent Root: " + Root.ToString());
//        for (int i = 0; i < Notes.Length; i++)
//        {
//            int modalIndex = (Mode.Enum.Id + i) % Scale.ScaleDegrees.Length;
//            Notes[i] = Root.NoteNameToKey().GetKeyAbove(Scale.ScaleDegrees[modalIndex].AsInterval()).GetKeyboardNoteName();
//        }
//        for (int i = 0; i < Notes.Length; i++)
//        {
//            Notes[i] += Notes[i] < Notes[0] ? 12 : 0;
//            Debug.Log(Notes[i].ToString());
//        }
//        Keyboard = new(Scale.ScaleDegrees.Length, Notes[0]);
// * 
// */