using System.Collections;
using System.Collections.Generic;
using MusicTheory.Keys;
using UnityEngine;
using MusicTheory.Triads;

public class ChordCard
{
    public ChordCard(Vector2 position)
    {
        Card.SetTMPPosition(position);
    }

    public void SelfDestruct()
    {
        Card.SelfDestruct();
        Object.Destroy(Parent);
    }

    private GameObject _parent;
    public GameObject Parent => _parent != null ? _parent : _parent = new GameObject(nameof(ChordCard));

    private Card _card;
    public Card Card => _card ??= new Card(nameof(ChordCard), Parent.transform)
            .SetTextString("<size=60%><font-weight=\"100\">" + nameof(Triad) + ": " + "</font-weight><size=100%>" + "Major")
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .AutoSizeTextContainer(true)
            .SetFontScale(.65f, .65f)
            .AutoSizeFont(true)
            .AllowWordWrap(false);


    //private Mode _mode = new Mode(new Major(), ModeDegreeEnum.Prime, nameof(Mode));
    //public Mode Mode
    //{
    //    get => _mode;
    //    set
    //    {
    //        _mode = value;
    //        Card.SetTextString(nameof(ScaleCard) + ": " + Scale.Name + _mode.Name);
    //    }
    //}

    private Key _currentRoot = new C();
    public Key CurrentRoot
    {
        get => _currentRoot;
        set
        {
            _currentRoot = value;
            Card.SetTextString(value.Name + Triad.Name);
        }
    }

    private Triad _triad = new Major();
    public Triad Triad
    {
        get => _triad;
        set
        {
            _triad = value;
            Card.SetTextString(CurrentRoot.Name + value.Name);
        }
    }
}
