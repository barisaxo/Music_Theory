using UnityEngine;
using MusicTheory;
using MusicTheory.Scales;
using MusicTheory.ScaleDegrees;
using MusicTheory.Keys;
using MusicTheory.Modes;

public class TriadCalculator
{
    public TriadCalculator(Vector2 rootPos, Vector2 thrPos, Vector2 fthPos)
    {
        RootPos = rootPos;
        ThrPos = thrPos;
        FthPos = fthPos;

        _ = RootCard;
        _ = ThirdCard;
        _ = FifthCard;

        ScaleCard = new(new Vector2(-Cam.UIOrthoX + 2.15f, -0.5f));
        ChordCard = new(RootPos + (Vector2.down * 2));

        CircleOfFifths = new ChromaticKeyCircle("Key Circle Of Fifths",
            2,
            new Vector2(-Cam.UIOrthoX + 2.15f, Cam.UIOrthoY - 2.15f),
            new C(),
            ChromaticKeyCircle.CircleType.Fifths);

        RomanCircle = new RomanCircle("Roman Diatonic Circle of Steps",
            1.65f,
            new Vector2(-Cam.UIOrthoX + 2.15f, -Cam.UIOrthoY + 1.35f),
            new Major(),
            ModeDegree.Prime,
            Scale.ScaleDegrees[0],
            RomanCircle.CircleType.Seconds);

        DiatonicKeyCircle = new DiatonicKeyCircle(
            "Nominal Circle of Steps",
             1.25f,
             new Vector2(-Cam.UIOrthoX + 6.45f, -Cam.UIOrthoY + 1.35f),
             CurrentKey,
             Scale,
             CurrentScaleDegree,
             DiatonicKeyCircle.CircleType.Seconds);

        this.SetChordTones();
    }

    public void SelfDestruct()
    {
        ScaleCard.SelfDestruct();
        Parent.SelfDestruct();
        CircleOfFifths.SelfDestruct();
        RomanCircle.SelfDestruct();
        DiatonicKeyCircle.SelfDestruct();
    }

    Vector2 RootPos; Vector2 ThrPos; Vector2 FthPos;

    private Mode _mode;
    public Mode Mode
    {
        get => _mode;
        set
        {
            _mode = value;
            ScaleCard.Mode = value;
            // DiatonicChords.Mode = value;
        }
    }

    private Scale _scale = new Major();
    public Scale Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            ScaleCard.Scale = value;
            RomanCircle.Scale = value;
        }
    }

    private Key _currentKey = new C();
    public Key CurrentKey
    {
        get => _currentKey;
        set
        {
            _currentKey = value;
            CircleOfFifths.CurrentKey = value;
            Root = Scale.Root(CurrentScaleDegree, value);
        }
    }

    private ScaleDegree _curentScaleDegree = new _1();
    public ScaleDegree CurrentScaleDegree
    {
        get => _curentScaleDegree;
        set
        {
            _curentScaleDegree = value;
            Root = Scale.Root(value, CurrentKey);
            RomanCircle.CurrentScaleDegree = value;
            DiatonicKeyCircle.CurrentScaleDegree = value;
        }
    }

    private Key _root = new C();
    public Key Root
    {
        get => _root;
        set
        {
            _root = value;
            ChordCard.CurrentRoot = value;
        }
    }

    public void ScrollRoman(int delta)
    {
        CurrentScaleDegree = RomanCircle.ScrollRoman(delta);
        DiatonicKeyCircle.ScrollKey(new MusicTheory.Intervals.P1());
        Root = Scale.Root(CurrentScaleDegree, CurrentKey);
    }

    public void ScrollKey(MusicTheory.Intervals.Interval delta)
    {
        CurrentKey = CircleOfFifths.ScrollKey(delta);
    }

    public ChromaticKeyCircle CircleOfFifths;
    public RomanCircle RomanCircle;
    public DiatonicKeyCircle DiatonicKeyCircle;

    private Card _parent;
    public Card Parent => _parent ??= new Card(nameof(TriadCalculator), null);

    public ScaleCard ScaleCard;
    public ChordCard ChordCard;

    private Card _rootCard;
    public Card RootCard => _rootCard ??= Parent.CreateChild(nameof(RootCard), Parent.Canvas.transform, Parent.Canvas)
            .SetTextString("Root")
            .SetTMPPosition(RootPos)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .AutoSizeTextContainer(true)
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(false);

    private Card _thirdCard;
    public Card ThirdCard => _thirdCard ??= Parent.CreateChild(nameof(ThirdCard), Parent.Canvas.transform, Parent.Canvas)
            .SetTextString("Third")
            .SetTMPPosition(ThrPos)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .AutoSizeTextContainer(true)
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(false);

    private Card _fifthCard;
    public Card FifthCard => _fifthCard ??= Parent.CreateChild(nameof(FifthCard), Parent.Canvas.transform, Parent.Canvas)
            .SetTextString("Fifth")
            .SetTMPPosition(FthPos)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .AutoSizeTextContainer(true)
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(false);



}
