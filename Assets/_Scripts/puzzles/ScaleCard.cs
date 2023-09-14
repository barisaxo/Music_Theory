using System.Collections;
using System.Collections.Generic;
using MusicTheory.Scales;
using UnityEngine;

public class ScaleCard
{
    public ScaleCard(Vector2 position)
    {
        Card.SetTMPPosition(position);
    }

    public void SelfDestruct()
    {
        Card.SelfDestruct();
        Object.Destroy(Parent);
    }

    private GameObject _parent;
    public GameObject Parent => _parent ??= new GameObject(nameof(ScaleCard));

    private Card _card;
    public Card Card => _card ??= new Card(nameof(ScaleCard), Parent.transform)
            .SetTextString(nameof(Card) + ": Major")
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .AutoSizeTextContainer(true)
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(false)
            .TMPClickable();


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
            Card.SetTextString(nameof(ScaleCard) + ": " + _scale.Name + " " + Mode.Name);
        }
    }
}
