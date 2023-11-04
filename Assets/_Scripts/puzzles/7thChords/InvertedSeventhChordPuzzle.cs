
using UnityEngine;
using MusicTheory.Arithmetic;
using MusicTheory.Keys;
using MusicTheory.SeventhChords;

public class InvertedSeventhChordPuzzle : IPuzzle
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

    public string Desc => "Build the <b><i>inverted seventh chord";
    Inversion inversion;
    private readonly string _question;
    public string Question => _question;
    public string Clue => GetChordTones(SeventhChord, inversion);

    enum Inversion { First, Second, Third }

    public InvertedSeventhChordPuzzle()
    {
        inversion = (Inversion)Random.Range(0, 3);

        Gamut = (SeventhChord)Enumeration.All<SeventhChordEnum>()[Random.Range(0, Enumeration.Length<SeventhChordEnum>())];

        _notes = new KeyboardNoteName[NumOfNotes];
        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];
        Key[] keys = new Key[4] {
            Root,
            Root.GetKeyAbove(SeventhChord.ChordTonesAsIntervals()[0]),
            Root.GetKeyAbove(SeventhChord.ChordTonesAsIntervals()[1]),
            Root.GetKeyAbove(SeventhChord.ChordTonesAsIntervals()[2])
        };

        for (int i = 0; i < Notes.Length; i++) Notes[i] = keys[(i + (int)inversion + 1) % 4].GetKeyboardNoteName();

        for (int i = 1; i < Notes.Length; i++) Notes[i] += Notes[i] < Notes[0] ? 12 : 0;

        _question = SeventhChord.Description.SpaceAfterCap() + " " + nameof(MusicTheory.Chords.Chord) + " " + InversionDescription(inversion);
    }

    string InversionDescription(Inversion inversion) => inversion switch
    {
        Inversion.First => "in first inversion",
        Inversion.Second => "in second inversion",
        _ => "in third inversion"
    };

    string GetChordTones(SeventhChord chord, Inversion inversion)
    {
        string temp = "";
        for (int i = 0; i < 4; i++)
        {
            int invertedIndex = (i + (int)inversion + 1) % 4;
            if (invertedIndex == 0) temp += "1  ";
            else temp += chord.ChordTonesAsIntervals()[invertedIndex - 1].AsScaleDegree() + "  ";
        }
        return temp;
    }
}
