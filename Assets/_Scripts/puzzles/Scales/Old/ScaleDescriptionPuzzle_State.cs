//using UnityEngine;
//using MusicTheory.Arithmetic;
//using MusicTheory.Keys;
//using MusicTheory.SeventhChords;
//using MusicTheory.Steps;
//using MusicTheory.Scales;
//using MusicTheory.ScaleDegrees;
//using MusicTheory.Modes;
//using Audio;

//public class ScaleDescriptionPuzzle_State : State
//{
//    protected override void PrepareState(System.Action callback)
//    {
//        Scale = Enumeration.All<ScaleEnum>()[Random.Range(0, Enumeration.Length<ScaleEnum>())];

//        KeyboardNoteName Root = ((Key)Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())]).GetKeyboardNoteName();

//        Notes = new KeyboardNoteName[Scale.ScaleDegrees.Length + 1];
//        Notes[0] = Root;
//        Notes[^1] = Root + 12;
//        Debug.Log("Scale: " + Scale.Name + ", Root: " + Root.ToString());
//        for (int i = 1; i < Notes.Length - 1; i++)
//        {
//            Notes[i] = Root.NoteNameToKey().GetKeyAbove(Scale.ScaleDegrees[i].AsInterval()).GetKeyboardNoteName();
//            Notes[i] += Notes[i] < Root ? 12 : 0;
//            Debug.Log(Notes[i].ToString());
//        }
//        Keyboard = new(Scale.ScaleDegrees.Length + 1, Notes[0]);

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

//                SetStateDirectly(new ScaleDescriptionPuzzle_State());
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

//    Scale Scale;
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
//        .SetTextString(Scale.Description.SpaceAfterCap() + " " + nameof(Scale) + GetSteps(Scale))
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
//        .SetTextString("Build the <b><i>scale")
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

//    private string GetSteps(Scale scale)
//    {
//        string temp = "\n";
//        //foreach (ScaleDegree sd in Scale.ScaleDegrees)
//        //{
//        //    temp += sd.Name + " ";
//        //}
//        //temp += "\n";
//        foreach (Step s in Scale.Steps)
//        {
//            temp += s.Name + " ";
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