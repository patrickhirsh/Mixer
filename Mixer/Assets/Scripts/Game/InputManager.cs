using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool debugMode;

    // keybindings
    public static KeyCode moveRight = KeyCode.D;
    public static KeyCode moveLeft = KeyCode.A;
    public static KeyCode goBack = KeyCode.Keypad0;
    public static KeyCode category1 = KeyCode.Keypad1;
    public static KeyCode category2 = KeyCode.Keypad2;
    public static KeyCode category3 = KeyCode.Keypad3;
    public static KeyCode category4 = KeyCode.Keypad4;
    public static KeyCode category5 = KeyCode.Keypad5;
    public static KeyCode category6 = KeyCode.Keypad6;
    public static KeyCode category7 = KeyCode.Keypad7;
    public static KeyCode category8 = KeyCode.Keypad8;
    public static KeyCode category9 = KeyCode.Keypad9;

    // used to generate category keybinding hints in GraphicsManager
    public static KeyCode[] categoryKeys =
    {
        category1, category2, category3, category4, category5,
        category6, category7, category8, category9
    };

    // used for highscore initials input validation
    public static KeyCode[] validInitialsInput =
    {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
        KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
        KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O,
        KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
        KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y,
        KeyCode.Z, KeyCode.Backspace, KeyCode.Return
    };


    void Update ()
    {
        switch (GameManager.state)
        {
            case gameState.mainGame:
                handleInput_mainGame();
                break;
        }    
	}


    // handles input during the primary gameplay loop
    private void handleInput_mainGame()
    {
        if (GameManager.state == gameState.mainGame)
        {
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

            // category7 key
            if (Input.GetKeyDown(category7))
                { Bartender.handleMenuSelection(category7); return; }

            // category8 key
            if (Input.GetKeyDown(category8))
                { Bartender.handleMenuSelection(category8); return; }

            // category9 key
            if (Input.GetKeyDown(category9))
                { Bartender.handleMenuSelection(category9); return; }
        }
    }


    // Given a keycode, returns a clean string that's formatted for front-end use
    public static string getStringFromKeyCode(KeyCode key)
    {
        string clean = key.ToString();
        if (clean.StartsWith("ALPHA", true, System.Globalization.CultureInfo.CurrentCulture)) { clean = clean.Substring(5, clean.Length - 5); }
        if (clean.StartsWith("KEYPAD", true, System.Globalization.CultureInfo.CurrentCulture)) { clean = clean.Substring(6, clean.Length - 6); }
        return clean;
    }
}
