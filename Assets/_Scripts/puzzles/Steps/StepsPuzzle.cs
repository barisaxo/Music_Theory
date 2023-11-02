
using MusicTheory.Arithmetic;
using MusicTheory.Keys;
using MusicTheory.Steps;
using UnityEngine;

public class StepsPuzzle : IPuzzle<Step>
{
    public int NumOfNotes => 2;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    private readonly Step _gamut;
    public Step Gamut => _gamut;

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>step";

    private readonly string _question;
    public string Question => _question;

    public StepsPuzzle()
    {
        _gamut = Random.Range(0, 3) switch
        {
            1 => new Whole(),
            0 => new Half(),
            2 => new Augmented(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        _notes = new KeyboardNoteName[NumOfNotes];

        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];

        Notes[0] = Root.GetKeyboardNoteName();

        Notes[1] = Root.GetKeyAbove(Gamut.AsInterval()).GetKeyboardNoteName();

        Notes[1] += Notes[1] < Notes[0] ? 12 : 0;

        _question = Gamut.Name + " " + nameof(Step);
    }

}