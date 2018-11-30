using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Input Manager is responsible for handling all user input.
/// Input Manager is also responsible for keeping track of the current key bindings,
/// cleaning key binding names for front-end use, changing key bindings, 
/// and storing various "valid" key arrays for certain input fields.
/// </summary>
public class InputManager : MonoBehaviour
{
    public static bool debugMode;

    // keybindings
    public static KeyCode moveRight { get; private set; }
    public static KeyCode moveLeft { get; private set; }
    public static KeyCode goBack { get; private set; }
    public static KeyCode category1 { get; private set; }
    public static KeyCode category2 { get; private set; }
    public static KeyCode category3 { get; private set; }
    public static KeyCode category4 { get; private set; }
    public static KeyCode category5 { get; private set; }
    public static KeyCode category6 { get; private set; }


    // FOR FUTURE USE

    /*
    // used to generate category keybinding hints in GraphicsManager
    private static KeyCode[] categoryKeys = { category1, category2, category3, category4, category5, category6 };

    // used for highscore initials input validation
    private static KeyCode[] validInitialsInput =
    {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
        KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
        KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O,
        KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
        KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y,
        KeyCode.Z, KeyCode.Backspace, KeyCode.Return
    };
    */


    void Start()
    {
        setDefaultKeyBindings();
    }

    void Update ()
    {
        handleInput();
    }

    #region EXTERNAL FUNCTIONS

    /// <summary>
    /// given a keycode, returns a clean string that's formatted for front-end use
    /// </summary>
    public static string getStringFromKeyCode(KeyCode key)
    {
        string clean = key.ToString();
        if (clean.StartsWith("ALPHA", true, System.Globalization.CultureInfo.CurrentCulture)) { clean = clean.Substring(5, clean.Length - 5); }
        if (clean.StartsWith("KEYPAD", true, System.Globalization.CultureInfo.CurrentCulture)) { clean = clean.Substring(6, clean.Length - 6); }
        return clean;
    }


    /// <summary>
    /// sets keybindings back to their default values
    /// </summary>
    public static void setDefaultKeyBindings()
    {
        moveRight = KeyCode.D;
        moveLeft = KeyCode.A;
        goBack = KeyCode.Keypad0;
        category1 = KeyCode.Keypad1;
        category2 = KeyCode.Keypad2;
        category3 = KeyCode.Keypad3;
        category4 = KeyCode.Keypad4;
        category5 = KeyCode.Keypad5;
        category6 = KeyCode.Keypad6;
    }

    #endregion


    #region INTERNAL FUNCTIONS

    /// <summary>
    /// handles all input for this scene
    /// </summary>
    private static void handleInput()
    {
        switch (GameManager.state)
        {
            case GameState.GameLoop:
                
                // bartender movement
                if (Input.GetKeyDown(moveRight))
                    { Bartender.handleMovement(moveRight); return; }
                if (Input.GetKeyDown(moveLeft))
                    { Bartender.handleMovement(moveLeft); return; }

                // goBack key
                if (Input.GetKeyDown(goBack))
                    { Bartender.handleMenuSelection(goBack); return; }

                // category1 key
                if (Input.GetKeyDown(category1))
                    { Bartender.handleMenuSelection(category1); return; }

                // category2 key
                if (Input.GetKeyDown(category2))
                    { Bartender.handleMenuSelection(category2); return; }

                // category3 key
                if (Input.GetKeyDown(category3))
                    { Bartender.handleMenuSelection(category3); return; }

                // category4 key
                if (Input.GetKeyDown(category4))
                    { Bartender.handleMenuSelection(category4); return; }

                // category5 key
                if (Input.GetKeyDown(category5))
                    { Bartender.handleMenuSelection(category5); return; }

                // category6 key
                if (Input.GetKeyDown(category6))
                    { Bartender.handleMenuSelection(category6); return; }

                break;
        }
    }

    #endregion
}
