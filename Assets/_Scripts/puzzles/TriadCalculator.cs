using UnityEngine;
using MusicTheory;
using MusicTheory.Scales;
using MusicTheory.Keys;

public class TriadCalculator
{
    public TriadCalculator(Vector2 rootPos, Vector2 thrPos, Vector2 fthPos)
    {
        RootPos = rootPos;
        ThrPos = thrPos;
        FthPos = fthPos;

        _ = Root;
        _ = Third;
        _ = Fifth;

        ScaleCard = new(new Vector2(-Cam.UIOrthoX + 2.15f, -0.5f));

        CircleOfFifths = new ChromaticKeyCircle("Key Circle Of Fifths",
            2,
            new Vector2(-Cam.UIOrthoX + 2.15f, Cam.UIOrthoY - 2.15f),
            new C(),
            ChromaticKeyCircle.CircleType.Fifths);

        RomanCircle = new RomanCircle("Roman Diatonic Circle of Steps",
            1.25f,
            new Vector2(-Cam.UIOrthoX + 2.15f, -Cam.UIOrthoY + 1.35f),
            new Major(),
            ModeDegree.Prime,
            0,
            RomanCircle.CircleType.Seconds);

        this.SetChordTones();
    }

    public void SelfDestruct()
    {
        ScaleCard.SelfDestruct();
        Parent.SelfDestruct();
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

    public ChromaticKeyCircle CircleOfFifths;
    public RomanCircle RomanCircle;

    private Card _parent;
    public Card Parent => _parent ??= new Card(nameof(TriadCalculator), null);

    public ScaleCard ScaleCard;

    private Card _root;
    public Card Root => _root ??= Parent.CreateChild(nameof(Root), Parent.Canvas.transform, Parent.Canvas)
            .SetTextString(nameof(Root))
            .SetTMPPosition(RootPos)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .AutoSizeTextContainer(true)
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(false);

    private Card _third;
    public Card Third => _third ??= Parent.CreateChild(nameof(Third), Parent.Canvas.transform, Parent.Canvas)
            .SetTextString(nameof(Third))
            .SetTMPPosition(ThrPos)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .AutoSizeTextContainer(true)
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(false);

    private Card _fifth;
    public Card Fifth => _fifth ??= Parent.CreateChild(nameof(Fifth), Parent.Canvas.transform, Parent.Canvas)
            .SetTextString(nameof(Fifth))
            .SetTMPPosition(FthPos)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .AutoSizeTextContainer(true)
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(false);



}
