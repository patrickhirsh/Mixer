using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool debugMode;
    private static KeyCode[] compKeys =
    {   // used to check for valid component keystrokes in handleInput()
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
        KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
        KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O,
        KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
        KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y,
        KeyCode.Z, KeyCode.Space, KeyCode.RightAlt, KeyCode.LeftAlt,
        KeyCode.Backspace
    };
	

	void Update ()
    {
        switch (GameManager.gameState)
        {
            case 1:
                handleInput1();
                break;
        }
        
	}


    // handles input during the primary gameplay loop
    private void handleInput1()
    {
        if (GameManager.gameState == 1)
        {
            // bartender movement
            if (Input.GetKeyDown(KeyCode.UpArrow))
                { Bartender.handleMovement("UpArrow"); return; }
            if (Input.GetKeyDown(KeyCode.DownArrow))
                { Bartender.handleMovement("DownArrow"); return; }

            // bartender drink submission
            if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
                { Bartender.handleSubmit(); return; }

            // bartender has cleared the drink at his current position
            if (Input.GetKeyDown(KeyCode.Delete))
            { Bartender.handleClear(); return; }

            // bartender category selection
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))  // Glassware
            { Bartender.handleCategorySelection("Alpha1"); return; }

            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))  // Beer
            { Bartender.handleCategorySelection("Alpha2"); return; }

            if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))  // Liquor
            { Bartender.handleCategorySelection("Alpha3"); return; }

            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))  // Bitters
            { Bartender.handleCategorySelection("Alpha4"); return; }

            if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))  // Non-Alcoholic
            { Bartender.handleCategorySelection("Alpha5"); return; }

            if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))  // Other
            { Bartender.handleCategorySelection("Alpha6"); return; }

            // bartender is in a menu
            if (Bartender.state != null)
            {
                // look for valid keystrokes. Return when one is found to avoid multiple input acceptance
                foreach (KeyCode key in compKeys)
                    if (Input.GetKeyDown(key))
                        { Bartender.handleDrinkConstruction(key.ToString()); return; }
            }
        }
    }
}
