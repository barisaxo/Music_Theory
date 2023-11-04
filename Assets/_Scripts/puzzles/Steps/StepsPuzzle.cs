
using MusicTheory.Arithmetic;
using MusicTheory.Keys;
using MusicTheory.Steps;
using UnityEngine;

public class StepsPuzzle : IPuzzle
{
    public int NumOfNotes => 2;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    public System.Type GamutType => typeof(Step);
    public IMusicalElement Gamut { get; private set; }
    public Step Step => Gamut is Step step ? step : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>step";

    private readonly string _question;
    public string Question => _question;
    public string Clue => "Half = +1, Whole = +2, Augmented = +3";
    public StepsPuzzle()
    {
        Gamut = Random.Range(0, 3) switch
        {
            1 => new Whole(),
            0 => new Half(),
            2 => new Augmented(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        _notes = new KeyboardNoteName[NumOfNotes];

        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];

        Notes[0] = Root.GetKeyboardNoteName();

        Notes[1] = Root.GetKeyAbove(Step.AsInterval()).GetKeyboardNoteName();

        Notes[1] += Notes[1] < Notes[0] ? 12 : 0;

        _question = Gamut.Name + " " + nameof(MusicTheory.Steps.Step);
    }

}