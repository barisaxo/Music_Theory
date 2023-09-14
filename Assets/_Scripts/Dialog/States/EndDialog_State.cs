
public class EndDialog_State : State
{
    readonly Dialog.Dialog Dialog;
    readonly State SubsequentState;
    readonly bool Fade;

    public EndDialog_State(Dialog.Dialog dialog, State subsequentState, bool fade)
    {
        Dialog = dialog;
        SubsequentState = subsequentState;
        Fade = fade;
    }

    protected override void EngageState()
    {
        //Audio.SoundFX.Stop();

        if (Fade) { FadeToState(SubsequentState); }
        else { SetStateDirectly(SubsequentState); }
    }

    protected override void DisengageState()
    {
        Dialog?.SelfDestruct();
    }
}
