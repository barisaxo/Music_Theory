using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Keys;

public class NotePuzzle : IPuzzle<KeyboardNoteName>
{
    public int NumOfNotes => 1;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.Horizontal;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    private readonly KeyboardNoteName _gamut;
    public KeyboardNoteName Gamut => _gamut;

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Find the <b><i>note";

    private readonly string _question;
    public string Question => _question;

    public NotePuzzle()
    {
        _gamut = (KeyboardNoteName)Random.Range(0, 25);
        _notes = new KeyboardNoteName[NumOfNotes];
        Notes[0] = Gamut;

        _question = Gamut.NoteNameToKey().Name;
    }


}
