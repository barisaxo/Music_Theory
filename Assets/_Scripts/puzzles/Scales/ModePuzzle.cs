using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Arithmetic;
using MusicTheory.Scales;
using MusicTheory.Keys;
using MusicTheory.Steps;
using MusicTheory.Modes;

public class ModePuzzle : IPuzzle<Scale>
{
    readonly int _numOfNotes;
    public int NumOfNotes => _numOfNotes;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.Horizontal;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    private readonly Scale _gamut;
    public Scale Gamut => _gamut;

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>mode";

    private readonly string _question;
    public string Question => _question;

    public ModePuzzle()
    {
        _gamut = WeightedRandomScale();
        Mode Mode = Gamut.Modes[Random.Range(0, Gamut.Modes.Length)];

        _numOfNotes = Gamut.ScaleDegrees.Length + 1;
        _notes = new KeyboardNoteName[NumOfNotes];

        KeyboardNoteName Root = ((Key)Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())]).GetKeyboardNoteName();

        Notes[0] = Root;
        Notes[^1] = Root + 12;

        for (int i = 1; i < Notes.Length - 1; i++)
        {
            Notes[i] = Root.NoteNameToKey().GetKeyAbove(Gamut.ScaleDegrees[i].AsInterval()).GetKeyboardNoteName();
            Notes[i] += Notes[i] < Root ? 12 : 0;
            Debug.Log(Notes[i].ToString());
        }

        for (int i = 0; i < Notes.Length - 1; i++)
        {
            int modalIndex = (Mode.Enum.Id + i) % Gamut.ScaleDegrees.Length;
            Notes[i] = Root.NoteNameToKey().GetKeyAbove(Gamut.ScaleDegrees[modalIndex].AsInterval()).GetKeyboardNoteName();
        }

        for (int i = 0; i < Notes.Length - 1; i++)
        {
            Notes[i] += Notes[i] < Notes[0] ? 12 : 0;
            Debug.Log(Notes[i].ToString());
        }

        Notes[^1] = Notes[0] + 12;

        _question = GetMajorModeName(Mode) + Mode.Enum.Name + " " + nameof(Mode) + " of the " +
            Gamut.Description.SpaceAfterCap() + " " + nameof(Scale) + GetSteps(Mode);
    }

    private string GetMajorModeName(Mode mode)
    {
        return Gamut is Major ? mode.Name + ": " : "";
    }

    private string GetSteps(Mode mode)
    {
        string temp = "\n";
        for (int i = 0; i < Gamut.Steps.Length; i++)
        {
            int modalIndex = (mode.Enum.Id + i) % Gamut.ScaleDegrees.Length;
            temp += Gamut.Steps[modalIndex].Name + " ";
        }
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
