using System;
using UnityEngine;

public class TheoryPuzzleState : State
{
    public TheoryPuzzleState()
    {
        TriadCalculator = new();
    }

    readonly TriadCalculator TriadCalculator;

    protected override void PrepareState(Action callback)
    {
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {

    }

    protected override void DisengageState()
    {
        TriadCalculator.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(TriadCalculator.CircleOfFifths.Parent.GO.transform))
        {
            TriadCalculator.ScrollKey(go);
        }

        else if (go.transform.IsChildOf(TriadCalculator.DiatonicKeyCircle.Parent.GO.transform))
        {
            TriadCalculator.ScrollRoman(go);
        }

        else if (go.transform.IsChildOf(TriadCalculator.ScaleCard.Parent.transform))
        {
            TriadCalculator.ScrollScales();
        }

        else if (go.transform.IsChildOf(TriadCalculator.ModeCard.Parent.transform))
        {
            TriadCalculator.ScrollModes();
        }
    }

}