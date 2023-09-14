using System;
using Dialog;
using UnityEngine;

public class DialogResponse_State : State
{
    private readonly Dialog.Dialog Dialog;
    private readonly State SubsequentState;
    private Reply Reply;

    public DialogResponse_State(Dialog.Dialog dialog, State subsequentState)
    {
        Dialog = dialog;
        SubsequentState = subsequentState;
    }

    protected override void PrepareState(Action callback)
    {
        Reply = new Reply(Dialog.CurrentLine.Responses);
        callback();
    }

    protected override void ClickedOn(GameObject go)
    {
        for (var i = 0; i < Reply.ResponseCards.Length; i++)
        {
            if (!go.transform.IsChildOf(Reply.ResponseCards[i].GO.transform)) continue;
            CheckResponse(Dialog.CurrentLine.Responses[i]);
            return;
        }
    }

    protected override void ConfirmPressed()
    {
        if (Reply.ResponseCards.Length >= 2)
            CheckResponse(Dialog.CurrentLine.Responses[^2]);
    }

    protected override void CancelPressed()
    {
        CheckResponse(Dialog.CurrentLine.Responses[^1]);
    }

    protected override void InteractPressed()
    {
        if (Reply.ResponseCards.Length >= 3)
            CheckResponse(Dialog.CurrentLine.Responses[^3]);
    }

    protected override void WestPressed()
    {
        if (Reply.ResponseCards.Length >= 4)
            CheckResponse(Dialog.CurrentLine.Responses[^4]);
    }

    private void CheckResponse(Response r)
    {
        if (r.HasGoToLine())
        {
            Dialog.SetCurrentLine(r.GoToLine);
        }

        else if (r.HasNextState())
        {
            Reply.SelfDestruct();

            DisengageState();
            SetStateDirectly(new EndDialog_State(
                Dialog,
                r.NextState,
                r.FadeOut));

            return;
        }
        else if (r.HasNextDialogue())
        {
            Dialog.Dialogue =
                r.GoToDialogue
                    .SetSpeakerColor(Dialog.CurrentLine.SpeakerColor)
                    .SetSpeakerIcon(Dialog.CurrentLine.SpeakerIcon)
                    .SetSpeakerName(Dialog.CurrentLine.SpeakerName)
                    .Initiate();

            Dialog.SetCurrentLine(Dialog.Dialogue.FirstLine);
        }

        Reply.SelfDestruct();

        SetStateDirectly(new DialogPrinting_State(Dialog, SubsequentState));

        DisengageState();
    }
}