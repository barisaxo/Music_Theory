using System;
using System.Collections.Generic;
using UnityEngine;

public class EndPuzzleSplash
{
    public EndPuzzleSplash(string message, Action callback)
    {
        Message = message;
        Callback = callback;
    }

    public void SelfDestruct()
    {
        Results.SelfDestruct();
    }

    readonly string Message;
    readonly Action Callback;



    Card _results;
    Card Results => _results ??= new Card(nameof(Results), null)
        .SetTextString(Message);




}