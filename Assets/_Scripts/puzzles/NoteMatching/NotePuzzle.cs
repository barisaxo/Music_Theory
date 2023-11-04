
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Keys;

public class NotePuzzle : IPuzzle
{
    public int NumOfNotes => 1;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.Horizontal;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    public IMusicalElement Gamut { get; private set; }
    public System.Type GamutType => typeof(Key);
    public Key Key => Gamut is Key note ? note : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Find the <b><i>note";

    private readonly string _question;
    public string Question => _question;

    public string Clue => "D is always between the group with two black keys." +
        "\nThe notes of the keyboard: C_D_EF_G_A_BC_D_EF_G_A_BC" +
        "\nSharp (#) = +1. Flat (b) = -1.";

    public NotePuzzle()
    {
        Gamut = (Key)Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];
        _notes = new KeyboardNoteName[NumOfNotes];
        Notes[0] = Key.GetKeyboardNoteName();

        _question = Key.Name;
    }


}
