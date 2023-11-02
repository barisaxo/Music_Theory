using UnityEngine;
using System;

public static class InputKey
{
    private static InputActions _inputActions;
    public static InputActions InputActions => _inputActions ??= new InputActions();

    public static event Action<GamePadButton> ButtonEvent;
    public static event Action<GamePadButton, Vector2> StickEvent;
    //public static event Action<GamePadButton, Vector2> RStickAltEvent;
    public static event Action<MouseAction, Vector3> MouseClickEvent;


    //public static event Action<float> RStickAltXEvent;
    //public static event Action<float> RStickAltYEvent;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInit()
    {
        //button press
        InputActions.Map.DUp.performed += _ => ButtonEvent?.Invoke(GamePadButton.Up_Press);
        InputActions.Map.DDown.performed += _ => ButtonEvent?.Invoke(GamePadButton.Down_Press);
        InputActions.Map.DLeft.performed += _ => ButtonEvent?.Invoke(GamePadButton.Left_Press);
        InputActions.Map.DRight.performed += _ => ButtonEvent?.Invoke(GamePadButton.Right_Press);

        InputActions.Map.East.performed += _ => ButtonEvent?.Invoke(GamePadButton.East_Press);
        InputActions.Map.South.performed += _ => ButtonEvent?.Invoke(GamePadButton.South_Press);
        InputActions.Map.North.performed += _ => ButtonEvent?.Invoke(GamePadButton.North_Press);
        InputActions.Map.West.performed += _ => ButtonEvent?.Invoke(GamePadButton.West_Press);

        InputActions.Map.R1.performed += _ => ButtonEvent?.Invoke(GamePadButton.R1_Press);
        InputActions.Map.R2.performed += _ => ButtonEvent?.Invoke(GamePadButton.R2_Press);
        InputActions.Map.R3.performed += _ => ButtonEvent?.Invoke(GamePadButton.R3_Press);
        InputActions.Map.L1.performed += _ => ButtonEvent?.Invoke(GamePadButton.L1_Press);
        InputActions.Map.L2.performed += _ => ButtonEvent?.Invoke(GamePadButton.L2_Press);
        InputActions.Map.L3.performed += _ => ButtonEvent?.Invoke(GamePadButton.L3_Press);

        InputActions.Map.Start.performed += _ => ButtonEvent?.Invoke(GamePadButton.Start_Press);
        InputActions.Map.Select.performed += _ => ButtonEvent?.Invoke(GamePadButton.Select_Press);

        //button release
        InputActions.Map.DUp.canceled += _ => ButtonEvent?.Invoke(GamePadButton.Up_Release);
        InputActions.Map.DDown.canceled += _ => ButtonEvent?.Invoke(GamePadButton.Down_Release);
        InputActions.Map.DLeft.canceled += _ => ButtonEvent?.Invoke(GamePadButton.Left_Release);
        InputActions.Map.DRight.canceled += _ => ButtonEvent?.Invoke(GamePadButton.Right_Release);

        InputActions.Map.East.canceled += _ => ButtonEvent?.Invoke(GamePadButton.East_Release);
        InputActions.Map.South.canceled += _ => ButtonEvent?.Invoke(GamePadButton.South_Release);
        InputActions.Map.North.canceled += _ => ButtonEvent?.Invoke(GamePadButton.North_Release);
        InputActions.Map.West.canceled += _ => ButtonEvent?.Invoke(GamePadButton.West_Release);

        InputActions.Map.R1.canceled += _ => ButtonEvent?.Invoke(GamePadButton.R1_Release);
        InputActions.Map.R2.canceled += _ => ButtonEvent?.Invoke(GamePadButton.R2_Release);
        InputActions.Map.R3.canceled += _ => ButtonEvent?.Invoke(GamePadButton.R3_Release);
        InputActions.Map.L1.canceled += _ => ButtonEvent?.Invoke(GamePadButton.L1_Release);
        InputActions.Map.L2.canceled += _ => ButtonEvent?.Invoke(GamePadButton.L2_Release);
        InputActions.Map.L3.canceled += _ => ButtonEvent?.Invoke(GamePadButton.L3_Release);

        InputActions.Map.Start.canceled += _ => ButtonEvent?.Invoke(GamePadButton.Start_Release);
        InputActions.Map.Select.canceled += _ => ButtonEvent?.Invoke(GamePadButton.Select_Release);

        //stick input
        InputActions.Map.LStick.performed += _ => StickEvent?.Invoke(GamePadButton.LStick, _.ReadValue<Vector2>());
        InputActions.Map.LStick.canceled += _ => StickEvent?.Invoke(GamePadButton.LStick, Vector2.zero);
        InputActions.Map.RStick.performed += _ => StickEvent?.Invoke(GamePadButton.RStick, _.ReadValue<Vector2>());
        InputActions.Map.RStick.canceled += _ => StickEvent?.Invoke(GamePadButton.RStick, Vector2.zero);

        //Nintendo Switch RSticks are weird, and the Y is inverted (up is negative, so inverting the sign with minus read value).
        InputActions.Map.RStickAltX.performed += _ => RAltXInput(_.ReadValue<float>());
        InputActions.Map.RStickAltX.canceled += _ => RAltXInput(0);
        InputActions.Map.RStickAltY.performed += _ => RAltYInput(-_.ReadValue<float>());
        InputActions.Map.RStickAltY.canceled += _ => RAltYInput(0);

        MonoHelper.OnUpdate += CheckForMouseClick;
        MonoHelper.OnUpdate += RStickAltReadLoop;
        StickEvent += DebugStick;
        InputActions.Map.Enable();
    }

    static void DebugStick(GamePadButton gpi, Vector2 v3) { Debug.Log(gpi + " " + v3); }

    static void CheckForMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) { MouseClickEvent?.Invoke(MouseAction.LDown, Input.mousePosition); }
        else if (Input.GetMouseButtonUp(0)) { MouseClickEvent?.Invoke(MouseAction.LUp, Input.mousePosition); }
        else if (Input.GetMouseButton(0)) { MouseClickEvent?.Invoke(MouseAction.LHold, Input.mousePosition); }
    }

    ///nintendo switch R sticks are weird
    private static bool NewRStickAltThisFrame;

    private static Vector2 RStickAlt => new(RStickAltX, RStickAltY);
    private static float _rStickAltX;

    private static float RStickAltX
    {
        get => _rStickAltX;
        set
        {
            NewRStickAltThisFrame = true;
            _rStickAltX = value;
        }
    }

    private static float _rStickAltY;

    private static float RStickAltY
    {
        get => _rStickAltY;
        set
        {
            NewRStickAltThisFrame = true;
            _rStickAltY = value;
        }
    }

    private static void RAltXInput(float f)
    {
        RStickAltX = f;
    }

    private static void RAltYInput(float f)
    {
        RStickAltY = f;
    }

    private static void RStickAltReadLoop()
    {
        if (!NewRStickAltThisFrame) return;
        StickEvent?.Invoke(GamePadButton.RStick, RStickAlt);
        NewRStickAltThisFrame = false;
    }

}

public enum MouseAction { LDown, LUp, LHold }

/// <summary>
/// GamePad Buttons
/// </summary>
public enum GamePadButton
{
    None = -1,

    Up_Press, Down_Press, Left_Press, Right_Press,
    North_Press, East_Press, South_Press, West_Press,
    R1_Press, R2_Press, R3_Press,
    L1_Press, L2_Press, L3_Press,
    Select_Press, Start_Press,

    Up_Release, Down_Release, Left_Release, Right_Release,
    North_Release, East_Release, South_Release, West_Release,
    R1_Release, R2_Release, R3_Release,
    L1_Release, L2_Release, L3_Release,
    Select_Release, Start_Release,

    LStick, RStick,
}

/// <summary>
/// Directional input for DPad.
/// </summary>
public enum Dir { Reset = -1, Up, Down, Left, Right }