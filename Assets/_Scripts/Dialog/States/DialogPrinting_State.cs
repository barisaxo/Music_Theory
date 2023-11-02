using System;
using System.Collections;
using Dialog;
using UnityEngine;
using UnityEngine.Video;

public class DialogPrinting_State : State
{
    private readonly Dialog.Dialog Dialog;

    private readonly State SubsequentState;
    private bool waitingForInput;

    public DialogPrinting_State(Dialog.Dialog dialog, State subsequentState)
    {
        Dialog = dialog;
        SubsequentState = subsequentState;
    }

    protected override void PrepareState(Action callback)
    {
        if (Dialog.CurrentLine.VideoClip != null)
        {
            var width = Cam.Io.Camera.orthographicSize * Cam.Io.Camera.aspect * 1.65f;
            Dialog.VideoPlayer.transform.localScale = new Vector3(width,
                width / ((float)Dialog.CurrentLine.VideoClip.width / Dialog.CurrentLine.VideoClip.height), 1);
            Dialog.VideoPlayer.gameObject.SetActive(true);
            Dialog.VideoPlayer.playOnAwake = false;
            Dialog.VideoPlayer.waitForFirstFrame = false;
            Dialog.VideoPlayer.isLooping = false;
            Dialog.VideoPlayer.clip = Dialog.CurrentLine.VideoClip;
            Dialog.VideoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
            Dialog.VideoPlayer.Prepare();
            LoadVideo(callback).StartCoroutine();
            return;
        }

        Dialog.VideoPlayer.gameObject.SetActive(false);
        callback();
    }

    private IEnumerator LoadVideo(Action callback)
    {
        while (!Dialog.VideoPlayer.isPrepared) yield return null;

        callback();
    }

    protected override void EngageState()
    {
        if (Dialog.CurrentLine.VideoClip != null) Dialog.VideoPlayer.Play();
        if (Dialog.Dialogue.PlayTypingSounds) Audio.SFX.PlayClip(Assets.TypingClicks);
        Dialog.NPCIcon(Dialog.CurrentLine);
        Dialog.PrintDialog(FinishedPrinting);
    }

    protected override void Clicked(MouseAction action, Vector3 _)
    {
        if (action != MouseAction.LUp) return;// Click.Down;

        Audio.SFX.StopClip();
        if (Dialog.LetType) Dialog.LetType = false;

        if (!waitingForInput) return;// Click.Down;

        if (Dialog.HasNextLine())
        {
            Dialog.SetNextLine();
            SetStateDirectly(new DialogPrinting_State(Dialog, SubsequentState));
            return;// Click.Down;
        }

        if (Dialog.HasNextState())
        {
            SetStateDirectly(new EndDialog_State(Dialog, Dialog.CurrentLine.NextState, Dialog.CurrentLine.FadeOut));
            return;// Click.Down;
        }

        if (Dialog.HasNextDialogue())
        {
            Dialog.Dialogue = Dialog.CurrentLine.NextDialogue
                .SetSpeakerColor(Dialog.CurrentLine.SpeakerColor)
                .SetSpeakerIcon(Dialog.CurrentLine.SpeakerIcon)
                .SetSpeakerName(Dialog.CurrentLine.SpeakerName)
                .Initiate();

            Dialog.CurrentLine = Dialog.Dialogue.FirstLine;

            SetStateDirectly(new DialogPrinting_State(Dialog, SubsequentState));
        }

        base.Clicked(action, _);
    }

    private void FinishedPrinting()
    {
        Audio.SFX.StopClip();
        if (Dialog.HasResponses())
        {
            SetStateDirectly(new DialogResponse_State(Dialog, SubsequentState));
            return;
        }

        waitingForInput = true;
    }

    protected override void ConfirmPressed()
    {
        Clicked(MouseAction.LUp, Vector3.negativeInfinity);
    }

    protected override void InteractPressed()
    {
        Clicked(MouseAction.LUp, Vector3.negativeInfinity);
    }

    protected override void CancelPressed()
    {
        Clicked(MouseAction.LUp, Vector3.negativeInfinity);
    }

    protected override void WestPressed()
    {
        Clicked(MouseAction.LUp, Vector3.negativeInfinity);
    }
}