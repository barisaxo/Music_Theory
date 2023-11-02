using UnityEngine;
using MusicTheory.Arithmetic;
using MusicTheory.Keys;
using MusicTheory.Triads;

public class TriadPuzzle : IPuzzle<Triad>
{
    public int NumOfNotes => 3;

    public PlaybackMode ListenPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;
    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Vertical;

    public bool AllowPlayQuestion => true;

    private readonly Triad _gamut;
    public Triad Gamut => _gamut;

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>triad";

    private readonly string _question;
    public string Question => _question;

    public TriadPuzzle()
    {
        _gamut = Enumeration.All<TriadEnum>()[Random.Range(0, Enumeration.Length<TriadEnum>())];

        _notes = new KeyboardNoteName[NumOfNotes];

        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];

        Notes[0] = Root.GetKeyboardNoteName();
        Notes[1] = Root.GetKeyAbove(Gamut.ChordTonesAsIntervals()[0]).GetKeyboardNoteName();
        Notes[2] = Root.GetKeyAbove(Gamut.ChordTonesAsIntervals()[1]).GetKeyboardNoteName();

        for (int i = 1; i < Notes.Length; i++) Notes[i] += Notes[i] < Notes[0] ? 12 : 0;

        _question = Gamut.Description + " " + nameof(Triad);
    }
}