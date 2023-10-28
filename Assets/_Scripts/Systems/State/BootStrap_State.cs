using System;
using System.Collections;
using UnityEngine;
using Audio;

public class BootStrap_State : State
{
    private BootStrap_State() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        BootStrap_State state = new();
        state.SetStateDirectly(state);
    }

    protected override void PrepareState(Action callback)
    {
        _ = Cam.Io;
        _ = AudioManager.Io.AudioParser;
        callback();
    }



    protected override void EngageState()
    {

#if PLATFORM_WEBGL
        if (SystemInfo.operatingSystem.Contains("Android"))
        {
            _ = Reload.SetTextString("Unfortunately WebGL is not supported on mobile browsers."
                + "\nPlease try again on a PC.");
            return;
        }
#endif

        if (Cam.Io.Camera.aspect < 1)
        {
            _ = Reload;
            return;
        }

        //PlayNotes().StartCoroutine();

        //IEnumerator PlayNotes()
        //{
        //    float timer = 0f;
        //    int note = 0;
        //    for (; ; )
        //    {
        //        if ((timer += Time.deltaTime) > 5f)
        //        {
        //            AudioManager.Io.KBAudio.PlayNote(AudioManager.Io.AudioParser.GetAudioClipFromKey((KeyboardNoteName)note++));
        //            timer -= 5f;
        //        }
        //        yield return null;
        //    }
        //}

        //SetStateDirectly(new DialogStart_State(new WelcomeDialogue()));

        SetStateDirectly(new InvertedSeventhChordsDescriptionPuzzle_State());
        //FadeToState(new IntervalAuralPuzzle_State());
        //SetStateDirectly(new TriadAuralPuzzle_State());
        //SetStateDirectly(new IntervalDescriptionPuzzle_State());

        //SetStateDirectly(new KeyboardTest_State());
        //SetStateDirectly(new TheoryPuzzleState());
        //SetStateDirectly(new MusicTheoryTest_State());
    }

    Card _reload;
    Card Reload => _reload ??= new Card(nameof(Reload), null)
        .SetTextString(
        "This app is not meant to be viewed in portait mode." +
        "\n\nWebGL does not allow forced screen rotation.\n\n" +
        "Please reload in landscape to continue...")
        .SetTMPPosition(Vector2.zero)
        .SetFontScale(1.1f, 1.1f)
        .AutoSizeFont(true)
        .SetTMPSize(new Vector2(Cam.Io.UICamera.aspect * 4, 4))
        .AutoSizeTextContainer(false)
        .AllowWordWrap(true);
}