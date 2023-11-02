using System;
using System.Collections.Generic;
using UnityEngine;

public class EndPuzzle_State : State
{
    public EndPuzzle_State(AudioClip[] acs, string message, Action callback)
    {
        ACs = acs;
        Message = message;
        Callback = callback;
    }

    readonly AudioClip[] ACs;
    readonly string Message;
    readonly Action Callback;

    Card _results;
    Card Results => _results ??= new Card(nameof(Results), null)
        .SetTextString(Message);


}