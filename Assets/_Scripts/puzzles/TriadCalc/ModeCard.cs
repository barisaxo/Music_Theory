using System.Collections;
using System.Collections.Generic;
using MusicTheory.Scales;
using UnityEngine;
using MusicTheory.Modes;

public class ModeCard
{
    public ModeCard(Vector2 position)
    {
        Card.SetTMPPosition(position);
    }

    public void SelfDestruct()
    {
        Card.SelfDestruct();
        Object.Destroy(Parent);
    }

    private GameObject _parent;
    public GameObject Parent => _parent != null ? _parent : _parent = new GameObject(nameof(ModeCard));

    private Card _card;
    public Card Card => _card ??= new Card(nameof(ModeCard), Parent.transform)
        .SetTextString("<size=60%><font-weight=\"100\">" + nameof(Mode) + ": " + "</font-weight><size=100%>" + "Ionian")
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .SetFontScale(.65f, .65f)
        .AutoSizeFont(true)
        .AllowWordWrap(false)
        .TMPClickable();

    private Mode _mode = new Ionian();
    public Mode Mode
    {
        get => _mode;
        set
        {
            _mode = value;
            Card.SetTextString("<size=60%><font-weight=\"100\">" + nameof(Mode) + ": " + "</font-weight><size=100%>" + Mode.Name);
        }
    }

}
