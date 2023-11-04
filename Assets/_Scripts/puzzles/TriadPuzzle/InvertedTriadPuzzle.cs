using UnityEngine;
using MusicTheory.Arithmetic;
using MusicTheory.Keys;
using MusicTheory.Triads;

public class InvertedTriadPuzzle : IPuzzle
{
    public int NumOfNotes => 3;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool AllowPlayQuestion => true;

    public System.Type GamutType => typeof(Triad);
    public IMusicalElement Gamut { get; private set; }
    public Triad Triad => Gamut is Triad triad ? triad : throw new System.ArgumentOutOfRangeException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;
    readonly Inversion inversion;
    public string Desc => "Build the <b><i>inverted triad";

    private readonly string _question;
    public string Question => _question;
    enum Inversion { first, second }
    public string Clue => GetChordTones(Triad, inversion);

    public InvertedTriadPuzzle()
    {
        Gamut = Random.Range(0, 4) switch
        {
            2 => new Minor(),
            3 => new Major(),
            1 => new Diminished(),
            0 => new Augmented(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        inversion = Random.value > .5f ? Inversion.first : Inversion.second;

        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];
        Key Third = Gamut switch
        {
            Major or Augmented => Root.GetKeyAbove(new MusicTheory.Intervals.M3()),
            _ => Root.GetKeyAbove(new MusicTheory.Intervals.mi3()),
        };
        Key Fifth = Gamut switch
        {
            Augmented => Root.GetKeyAbove(new MusicTheory.Intervals.A5()),
            Diminished => Root.GetKeyAbove(new MusicTheory.Intervals.d5()),
            _ => Root.GetKeyAbove(new MusicTheory.Intervals.P5()),
        };

        _notes = new KeyboardNoteName[NumOfNotes];

        Notes[0] = (inversion switch
        {
            Inversion.first => Third,
            _ => Fifth
        }).GetKeyboardNoteName();

        Notes[1] = (inversion switch
        {
            Inversion.first => Fifth,
            _ => Root
        }).GetKeyboardNoteName();

        Notes[2] = (inversion switch
        {
            Inversion.first => Root,
            _ => Third
        }).GetKeyboardNoteName();

        for (int i = 1; i < Notes.Length; i++) Notes[i] += Notes[i] < Notes[0] ? 12 : 0;

        _question = Triad.Description + " " + nameof(MusicTheory.Triads.Triad) + InversionDescription(inversion);
    }

    string InversionDescription(Inversion inversion) => inversion switch
    {
        Inversion.first => " in first inversion",
        _ => " in second inversion"
    };

    string GetChordTones(Triad chord, Inversion inversion)
    {
        string temp = "";
        for (int i = 0; i < 3; i++)
        {
            int invertedIndex = (i + (int)inversion + 1) % 3;
            if (invertedIndex == 0) temp += "1  ";
            else temp += chord.ChordTonesAsIntervals()[invertedIndex - 1].AsScaleDegree().Name + "  ";
        }
        return temp;
    }
}