using Dialog;
using System;

public class DialogStart_State : State
{
    Dialog.Dialog Dialog;
    readonly Dialogue _dialogue;
    readonly State SubsequentState;

    public DialogStart_State(Dialogue dialogue)
    {
        _dialogue = dialogue;
    }

    public DialogStart_State(Dialogue dialogue, State subsequentState)
    {
        _dialogue = dialogue;
        SubsequentState = subsequentState;
    }

    protected override void PrepareState(Action callback)
    {
        Audio.BGMusic.Pause();

        Dialog = new(_dialogue.Initiate());
        callback();
    }

    protected override void EngageState()
    {
        //if (Dialog.Dialogue.StartSound != null) { Audio.SoundFX.PlayOneShot(Dialog.Dialogue.StartSound); }
        SetStateDirectly(new DialogPrinting_State(Dialog, SubsequentState));
    }
}
