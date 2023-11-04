using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Arithmetic;
using MusicTheory.Scales;
using MusicTheory.Keys;
using MusicTheory.Modes;

public class ModePuzzle : IPuzzle
{
    readonly int _numOfNotes;
    public int NumOfNotes => _numOfNotes;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.Horizontal;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    public System.Type GamutType => typeof(Scale);
    public IMusicalElement Gamut { get; private set; }
    public Scale Scale => Gamut is Scale scale ? scale : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>mode";

    private readonly string _question;
    public string Question => _question;

    public string Clue => GetSteps(Mode);
    readonly Mode Mode;

    public ModePuzzle()
    {
        Gamut = WeightedRandomScale();
        Mode = Scale.Modes[Random.Range(0, Scale.Modes.Length)];

        _numOfNotes = Scale.ScaleDegrees.Length + 1;
        _notes = new KeyboardNoteName[NumOfNotes];

        KeyboardNoteName Root = ((Key)Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())]).GetKeyboardNoteName();

        Notes[0] = Root;
        Notes[^1] = Root + 12;

        for (int i = 1; i < Notes.Length - 1; i++)
        {
            Notes[i] = Root.NoteNameToKey().GetKeyAbove(Scale.ScaleDegrees[i].AsInterval()).GetKeyboardNoteName();
            Notes[i] += Notes[i] < Root ? 12 : 0;
        }

        for (int i = 0; i < Notes.Length - 1; i++)
        {
            int modalIndex = (Mode.Enum.Id + i) % Scale.ScaleDegrees.Length;
            Notes[i] = Root.NoteNameToKey().GetKeyAbove(Scale.ScaleDegrees[modalIndex].AsInterval()).GetKeyboardNoteName();
        }

        for (int i = 0; i < Notes.Length - 1; i++)
        {
            Notes[i] += Notes[i] < Notes[0] ? 12 : 0;
        }

        Notes[^1] = Notes[0] + 12;

        _question = GetMajorModeName(Mode) + Mode.Enum.Name + " " + nameof(Mode) + " of the " +
            Scale.Description.SpaceAfterCap() + " " + nameof(MusicTheory.Scales.Scale);
    }

    private string GetMajorModeName(Mode mode)
    {
        return Scale is Major ? mode.Name + ": " : "";
    }

    private string GetSteps(Mode mode)
    {
        string temp = "\n";
        for (int i = 0; i < Scale.Steps.Length; i++) temp += Scale.Steps[(mode.Enum.Id + i) % Scale.ScaleDegrees.Length].Name + " ";
        return temp;
    }

    private Scale WeightedRandomScale()
    {
        return Random.Range(0, 42) switch
        {
            < 10 => new Major(),
            < 19 => new JazzMinor(),
            < 29 => new HarmonicMinor(),
            < 37 => new Pentatonic(),
            _ => new MusicTheory.Scales.Diminished(),
        };
    }

}
