using UnityEngine;
using MusicTheory.Arithmetic;
using MusicTheory.Keys;
using MusicTheory.Triads;
using Audio;

public class InvertedTriadPuzzle : IPuzzle<Triad>
{
    public int NumOfNotes => 3;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool AllowPlayQuestion => true;

    private readonly Triad _gamut;
    public Triad Gamut => _gamut;

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>inverted triad";

    private readonly string _question;
    public string Question => _question;
    enum Inversion { first, second }

    public InvertedTriadPuzzle()
    {
        _gamut = Random.Range(0, 4) switch
        {
            2 => new Minor(),
            3 => new Major(),
            1 => new Diminished(),
            0 => new Augmented(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        Inversion inversion = Random.value > .5f ? Inversion.first : Inversion.second;

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

        _question = Gamut.Description + " " + nameof(Triad) + InversionDescription(inversion);
    }

    string InversionDescription(Inversion inversion) => inversion switch
    {
        Inversion.first => " in first inversion",
        _ => " in second inversion"
    };
}