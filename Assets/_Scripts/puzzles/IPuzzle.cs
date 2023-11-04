using System;

public interface IPuzzle
{//Gamut: a complete scale of musical notes; the compass or range of a voice or instrument.
    public IMusicalElement Gamut { get; }
    public Type GamutType { get; }
    public int NumOfNotes { get; }
    public string Desc { get; }
    public string Question { get; }
    public string Clue { get; }
    public KeyboardNoteName[] Notes { get; }
    public PlaybackMode QuestionPlaybackMode { get; }
    public PlaybackMode ListenPlaybackMode { get; }
    public PlaybackMode AnswerPlaybackMode { get; }
    public bool AllowPlayQuestion { get; }
}