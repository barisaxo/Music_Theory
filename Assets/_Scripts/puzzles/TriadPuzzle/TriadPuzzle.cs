using UnityEngine;
using MusicTheory.Arithmetic;
using MusicTheory.Keys;
using MusicTheory.Triads;

public class TriadPuzzle : IPuzzle
{
    public int NumOfNotes => 3;

    public PlaybackMode ListenPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;
    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Vertical;

    public bool AllowPlayQuestion => true;

    public System.Type GamutType => typeof(Triad);
    public IMusicalElement Gamut { get; private set; }
    public Triad Triad => Gamut is Triad triad ? triad : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>triad";

    private readonly string _question;
    public string Question => _question;
    public string Clue => GetChordTones(Triad);

    public TriadPuzzle()
    {
        Gamut = (Triad)Enumeration.All<TriadEnum>()[Random.Range(0, Enumeration.Length<TriadEnum>())];

        _notes = new KeyboardNoteName[NumOfNotes];

        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];

        Notes[0] = Root.GetKeyboardNoteName();
        Notes[1] = Root.GetKeyAbove(Triad.ChordTonesAsIntervals()[0]).GetKeyboardNoteName();
        Notes[2] = Root.GetKeyAbove(Triad.ChordTonesAsIntervals()[1]).GetKeyboardNoteName();

        for (int i = 1; i < Notes.Length; i++) Notes[i] += Notes[i] < Notes[0] ? 12 : 0;

        _question = Triad.Description + " " + nameof(MusicTheory.Triads.Triad);
    }

    private string GetChordTones(Triad chord)
    {
        string temp = "Root ";
        foreach (MusicTheory.Intervals.Interval i in chord.ChordTonesAsIntervals())
            temp += i.Name + " ";
        return temp;
    }
}