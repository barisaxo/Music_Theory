using System;
using System.Collections;
using System.Collections.Generic;
using MusicTheory;
using MusicTheory.Intervals;
using MusicTheory.Scales;
using UnityEngine;

public class TheoryPuzzleState : State
{
    public TheoryPuzzleState()
    {
        TriadCalculator = new(new Vector2(2, -1.5f), new Vector2(2, 0), new Vector2(2, 1.5f));
        //TriadCalculator.SetChordTones();
    }

    TriadCalculator TriadCalculator;

    protected override void PrepareState(Action callback)
    {

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {

    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(TriadCalculator.CircleOfFifths.Parent.GO.transform))
        {
            TriadCalculator.ScrollKey(TriadCalculator.CircleOfFifths.Type switch
            {
                ChromaticKeyCircle.CircleType.Fifths => new P5(),
                ChromaticKeyCircle.CircleType.Chromatic => new mi2(),
                _ => throw new ArgumentOutOfRangeException()
            });

            TriadCalculator.DiatonicKeyCircle = TriadCalculator.NewDiatonicKeyCircle();
            TriadCalculator.SetChordTones();
        }

        if (go.transform.IsChildOf(TriadCalculator.RomanCircle.Parent.GO.transform) ||
            go.transform.IsChildOf(TriadCalculator.DiatonicKeyCircle.Parent.GO.transform))
        {
            TriadCalculator.ScrollRoman(TriadCalculator.RomanCircle.Type switch
            {
                RomanCircle.CircleType.Fifths => 4,
                RomanCircle.CircleType.Thirds => 2,
                RomanCircle.CircleType.Seconds => 1,
                _ => throw new ArgumentOutOfRangeException()
            });

            TriadCalculator.SetChordTones();
        }

        if (go.transform.IsChildOf(TriadCalculator.ScaleCard.Parent.transform))
        {
            TriadCalculator.Scale++;
            TriadCalculator.RomanCircle = TriadCalculator.NewDiatonicRomanCircle();
            TriadCalculator.DiatonicKeyCircle = TriadCalculator.NewDiatonicKeyCircle();
            TriadCalculator.SetChordTones();
            // TriadCalculator.SetScale((Scale)(TriadCalculator.Scale + 1));
        }

        //if (go.transform.IsChildOf(TriadCalculator.CircleOfFifths.Parent.GO.transform))
        //{
        //    //TriadCalculator.CircleOfFifths.RotateCounterClockwise();
        //}

        //if (go.transform.IsChildOf(TriadCalculator.DiatonicChords.Parent.GO.transform))
        //{
        //    //TriadCalculator.DiatonicChords.RotateCounterClockwise();
        //}

        //TriadCalculator.SetChordTones();
    }
}