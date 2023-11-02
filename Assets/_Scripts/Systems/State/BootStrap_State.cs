using System;
using UnityEngine;

public class BootStrap_State : State
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        BootStrap_State state = new();
        state.SetStateDirectly(state);
    }

    protected override void PrepareState(Action callback)
    {
        _ = Cam.Io;
        _ = Audio.AudioParser;
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

        Data.TheoryPuzzleData.PuzzleDifficulty = PuzzleDifficulty.Free;
        FadeToState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));

    }

    PuzzleType RandPuzzleType => UnityEngine.Random.value > .5f ? PuzzleType.Theory : PuzzleType.Aural;

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