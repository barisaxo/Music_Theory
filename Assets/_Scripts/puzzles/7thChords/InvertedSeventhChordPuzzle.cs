
using UnityEngine;
using MusicTheory.Arithmetic;
using MusicTheory.Keys;
using MusicTheory.SeventhChords;

public class InvertedSeventhChordPuzzle : IPuzzle<SeventhChord>
{
    public int NumOfNotes => 4;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool AllowPlayQuestion => true;

    private readonly SeventhChord _gamut;
    public SeventhChord Gamut => _gamut;

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>inverted seventh chord";

    private readonly string _question;
    public string Question => _question;

    enum Inversion { First, Second, Third }

    public InvertedSeventhChordPuzzle()
    {
        Inversion inversion = (Inversion)Random.Range(0, 3);

        _gamut = Enumeration.All<SeventhChordEnum>()[Random.Range(0, Enumeration.Length<SeventhChordEnum>())];

        _notes = new KeyboardNoteName[NumOfNotes];
        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];
        Key[] keys = new Key[4] {
            Root,
            Root.GetKeyAbove(Gamut.ChordTonesAsIntervals()[0]),
            Root.GetKeyAbove(Gamut.ChordTonesAsIntervals()[1]),
            Root.GetKeyAbove(Gamut.ChordTonesAsIntervals()[2])
        };

        for (int i = 0; i < Notes.Length; i++) Notes[i] = keys[(i + (int)inversion + 1) % 4].GetKeyboardNoteName();

        for (int i = 1; i < Notes.Length; i++) Notes[i] += Notes[i] < Notes[0] ? 12 : 0;

        _question = Gamut.Description.SpaceAfterCap() + " " + nameof(MusicTheory.Chords.Chord) + " " + InversionDescription(inversion);
    }

    string InversionDescription(Inversion inversion) => inversion switch
    {
        Inversion.First => "in first inversion",
        Inversion.Second => "in second inversion",
        _ => "in third inversion"
    };
}
