using UnityEngine;
using MusicTheory;
using MusicTheory.Scales;
using MusicTheory.ScaleDegrees;
using MusicTheory.Keys;
using MusicTheory.Modes;
using MusicTheory.Arithmetic;
using System;

public class TriadCalculator
{
    public TriadCalculator()
    {
        _ = RootCard;
        _ = ThirdCard;
        _ = FifthCard;
        _ = HowTo;

        ScaleCard = new(new Vector2(Cam.UIOrthoX - 2.15f, -4f));
        ChordCard = new(RootPos + (Vector2.down * 2));
        ModeCard = new(new Vector2(Cam.UIOrthoX - 2.15f, -3f));

        Keys = Key.GetKeys(Scale, Mode);

        CircleOfFifths = new ChromaticKeyCircle("Key Circle Of Fifths",
            2.25f,
            new Vector2(-Cam.UIOrthoX + 2.15f, 0),
            Key,
            ChromaticKeyCircle.CircleType.Fifths);

        //RomanCircle = new RomanCircle("Roman Diatonic Circle of Steps",
        //    1.65f,
        //    new Vector2(-Cam.UIOrthoX + 2.15f, -Cam.UIOrthoY + 1.35f),
        //    new Major(),
        //    Mode,
        //    Scale.ScaleDegrees[0],
        //    RomanCircle.CircleType.Seconds);

        DiatonicKeyCircle = new DiatonicKeyCircle(
            "Nominal Circle of Steps",
             2.25f,
             new Vector2(Cam.UIOrthoX - 2.15f, 1f),
             Keys,
             Scale,
             Mode,
             ScaleDegree,
             DiatonicKeyCircle.CircleType.Seconds);
        //DiatonicKeyCircle = this.NewDiatonicKeyCircle();
        //CircleOfFifths = this.NewChromaticKeyCircleOfFifths();
        //RomanCircle = this.NewDiatonicRomanCircle();

        this.SetChordTones();

        TriadAudio = new(new VolumeData());
    }

    public void SelfDestruct()
    {
        ScaleCard.SelfDestruct();
        ChordCard.SelfDestruct();
        ModeCard.SelfDestruct();
        CircleOfFifths.SelfDestruct();
        //RomanCircle.SelfDestruct();
        DiatonicKeyCircle.SelfDestruct();
        TriadAudio.SelfDestruct();
        Parent.SelfDestruct();
    }

    Vector2 RootPos = new Vector2(0, -1.5f);
    Vector2 ThrPos = new Vector2(0, 0);
    Vector2 FthPos = new Vector2(0, 1.5f);

    private Mode _mode = new Ionian();
    public Mode Mode
    {
        get => _mode;
        set
        {
            _mode = value;
            ModeCard.Mode = Mode;
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
            Mode = Scale.Modes[0];
        }
    }

    private Key _currentKey = new C();
    public Key Key
    {
        get => _currentKey;
        set
        {
            _currentKey = value;
            Root = Scale.Root(ScaleDegree, value);
        }
    }

    private ScaleDegree _scaleDegree = new _1();
    public ScaleDegree ScaleDegree
    {
        get => _scaleDegree;
        set
        {
            _scaleDegree = value;
            Root = Scale.Root(value, Key);
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

    public Key[] Keys;

    public void ScrollRoman(GameObject go)
    {
        //ScaleDegree = Scale.ScaleDegrees[(Scale.GetIndex(ScaleDegree) + 1) % Scale.ScaleDegrees.Length];
        ScaleDegree = DiatonicKeyCircle.GetClickedScaleDegree(go);
        Root = Scale.Root(ScaleDegree, Key);
        UpdateCalculator();
    }

    public void ScrollKey(GameObject go)
    {
        ScaleDegree = new _1();
        Key = CircleOfFifths.GetClickedKey(go);

        UpdateCalculator();
    }

    internal void ScrollScales()
    {
        Scale++;
        ScaleDegree = new _1();

        UpdateCalculator();
    }

    internal void ScrollModes()
    {
        Mode = Scale.Modes[(Scale.GetIndex(Mode) + 1) % Scale.Modes.Length];

        this.SetChordTones();
        UpdateCalculator();
    }

    public ChromaticKeyCircle CircleOfFifths = null;
    //public RomanCircle RomanCircle = null;
    public DiatonicKeyCircle DiatonicKeyCircle = null;

    private Card _parent;
    public Card Parent => _parent ??= new Card(nameof(TriadCalculator), null);

    public ScaleCard ScaleCard;
    public ModeCard ModeCard;
    public ChordCard ChordCard;

    public readonly Triad_AudioSystem TriadAudio;

    private Card _rootCard;
    public Card RootCard => _rootCard ??= Parent.CreateChild(nameof(RootCard), Parent.Canvas.transform, Parent.Canvas)
            .SetTextString("Root")
            .SetTextColor(new Color(1f, 1f, .6f))
            .SetTMPPosition(RootPos)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .AutoSizeTextContainer(true)
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(false);

    private Card _thirdCard;
    public Card ThirdCard => _thirdCard ??= Parent.CreateChild(nameof(ThirdCard), Parent.Canvas.transform, Parent.Canvas)
            .SetTextString("Third")
            .SetTextColor(new Color(1f, 1f, .6f))
            .SetTMPPosition(ThrPos)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .AutoSizeTextContainer(true)
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(false);

    private Card _fifthCard;
    public Card FifthCard => _fifthCard ??= Parent.CreateChild(nameof(FifthCard), Parent.Canvas.transform, Parent.Canvas)
            .SetTextString("Fifth")
            .SetTextColor(new Color(1f, 1f, .6f))
            .SetTMPPosition(FthPos)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .AutoSizeTextContainer(true)
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(false);

    public void UpdateCalculator()
    {
        Keys = Key.GetKeys(Scale, Mode);
        CircleOfFifths = this.NewChromaticKeyCircleOfFifths();
        //RomanCircle = this.NewDiatonicRomanCircle();
        DiatonicKeyCircle = this.NewDiatonicKeyCircle();

        this.SetChordTones();
        this.PlayTriad();
    }

    Card _howTo;
    Card HowTo => _howTo ??= Parent.CreateChild(nameof(HowTo), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("Click/tap on left circle to select key, right circle to select scale degree.\nClick/tap on scale, or mode to scroll.")
        .AutoSizeTextContainer(true)
        .SetTextColor(new Color(1f, 1f, .6f))
        .AutoSizeFont(true)
        .SetFontScale(.5f, .5f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .SetTMPPosition(0, Cam.UIOrthoY - .5f);
}
