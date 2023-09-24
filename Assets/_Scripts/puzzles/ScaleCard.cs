using System.Collections;
using System.Collections.Generic;
using MusicTheory.Scales;
using UnityEngine;
using MusicTheory.Modes;

public class ScaleCard
{
    public ScaleCard(Vector2 position)
    {
        Card.SetTMPPosition(position);
        //_ = ClickArea;
    }

    public void SelfDestruct()
    {
        Card.SelfDestruct();
        Object.Destroy(Parent);
    }

    private GameObject _parent;
    public GameObject Parent => _parent != null ? _parent : _parent = new GameObject(nameof(ScaleCard));

    private Card _card;
    public Card Card => _card ??= new Card(nameof(ScaleCard), Parent.transform)
        .SetTextString("<size=60%><font-weight=\"100\">" + nameof(Scale) + ": " + "</font-weight><size=100%>" + "Major")
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .SetFontScale(.65f, .65f)
        .AutoSizeFont(true)
        .AllowWordWrap(false)
        .TMPClickable();

    //private Card _clickArea;
    //public Card ClickArea => _clickArea != null ? _clickArea : _clickArea = Card.CreateChild(nameof(ClickArea), Parent.transform, Card.Canvas)
    //    .SetImageColor(new Color(.62f, .62f, .62f, .2f))
    //    .ImageClickable();


    private Mode _mode = new Mode(new Major(), ModeDegreeEnum.Prime, nameof(Mode));
    public Mode Mode
    {
        get => _mode;
        set
        {
            _mode = value;
            Card.SetTextString(nameof(ScaleCard) + ": " + Scale.Name + _mode.Name);
        }
    }

    private Scale _scale = new Major();
    public Scale Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            Card.SetTextString("<size=60%><font-weight=\"100\">" + nameof(Scale) + ": " + "</font-weight><size=100%>" + Scale.Description);
        }
    }
}
