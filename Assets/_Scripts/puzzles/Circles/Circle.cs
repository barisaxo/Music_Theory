using UnityEngine;
using MusicTheory.Keys;
using MusicTheory.Scales;
using MusicTheory.Intervals;
using MusicTheory.RomanNumerals;
using MusicTheory.Modes;

public class Circle
{
    public Circle(string name, float radius, Vector2 pos)
    {
        Name = name;
        Radius = radius;
        Position = pos;
    }

    public void SelfDestruct()
    {
        Parent.SelfDestruct();
    }

    public readonly string Name;
    public readonly float StartingAngle = -Mathf.PI / 2;

    internal float Radius;
    internal Vector2 Position;

    public float RotationalOffset;
    public string[] PointNames;

    private Card _parent;
    public Card Parent => _parent ??= new Card(Name, null);

    public Card[] PointCards;

    private Card _centerCard;
    public Card CenterCard => _centerCard ??= Parent.CreateChild(nameof(CenterCard), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString(PointNames[0])
        .SetTMPPosition(Position)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .SetFontScale(.65f, .65f)
        .AutoSizeFont(true)
        .AllowWordWrap(false)
        .TMPClickable()
        ;

}

public interface IRoman<T> where T : Circle
{
    public MusicTheory.ScaleDegrees.ScaleDegree CurrentScaleDegree { get; set; }
    public Scale Scale { get; set; }
    public ModeDegree Mode { get; set; }
    public RomanNumeral RomanNumeral { get; }
    public MusicTheory.ScaleDegrees.ScaleDegree ScrollRoman(int delta);
}

public interface IKey<T> where T : Circle
{
    public Key CurrentKey { get; set; }
    public Key ScrollKey(Interval delta);
}





