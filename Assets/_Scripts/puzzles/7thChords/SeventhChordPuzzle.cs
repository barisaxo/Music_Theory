
using UnityEngine;
using MusicTheory.Arithmetic;
using MusicTheory.Keys;
using MusicTheory.SeventhChords;

public class SeventhChordPuzzle : IPuzzle
{
    public int NumOfNotes => 4;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool AllowPlayQuestion => true;

    public System.Type GamutType => typeof(SeventhChord);
    public IMusicalElement Gamut { get; private set; }
    public SeventhChord SeventhChord => Gamut is SeventhChord chord ? chord : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>seventh chord";

    private readonly string _question;
    public string Question => _question;
    public string Clue => GetChordTones(SeventhChord);

    public SeventhChordPuzzle()
    {
        Gamut = (SeventhChord)Enumeration.All<SeventhChordEnum>()[Random.Range(0, Enumeration.Length<SeventhChordEnum>())];

        _notes = new KeyboardNoteName[NumOfNotes];

        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];

        Notes[0] = Root.GetKeyboardNoteName();
        Notes[1] = Root.GetKeyAbove(SeventhChord.ChordTonesAsIntervals()[0]).GetKeyboardNoteName();
        Notes[2] = Root.GetKeyAbove(SeventhChord.ChordTonesAsIntervals()[1]).GetKeyboardNoteName();
        Notes[3] = Root.GetKeyAbove(SeventhChord.ChordTonesAsIntervals()[2]).GetKeyboardNoteName();

        for (int i = 1; i < Notes.Length; i++) Notes[i] += Notes[i] < Notes[0] ? 12 : 0;

        _question = SeventhChord.Description.SpaceAfterCap() + " " + Gamut.Name;
    }

    private string GetChordTones(SeventhChord seventhChord)
    {
        string temp = "Root ";
        foreach (MusicTheory.Intervals.Interval i in seventhChord.ChordTonesAsIntervals())
            temp += i.Name + " ";
        return temp;
    }


}
