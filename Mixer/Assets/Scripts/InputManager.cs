using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool debugMode;      
    private static string[] compKeys = { "A", "B", "C", "D",        // used to check for valid component keystrokes in handleInput()
        "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
        "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
        "Space", "RightAlt", "LeftAlt", "Backspace" };
	

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
            if (Input.GetKeyDown("UpArrow"))
                { Bartender.handleMovement("UpArrow"); return; }
            if (Input.GetKeyDown("DownArrow"))
                { Bartender.handleMovement("DownArrow"); return; }

            // bartender drink submission
            if (Input.GetKeyDown("Keypad0") || Input.GetKeyDown("Alpha0"))
                { Bartender.handleSubmit(); return; }

            // bartender has cleared the drink at his current position
            if (Input.GetKeyDown("Delete"))
            { Bartender.handleClear(); return; }

            // bartender is not currently in a menu
            if (Bartender.state == null)
            {
                // bartender category selection
                if (Input.GetKeyDown("Keypad1") || Input.GetKeyDown("Alpha1"))  // Glassware
                    { Bartender.handleCategorySelection("Alpha1"); return; }

                if (Input.GetKeyDown("Keypad2") || Input.GetKeyDown("Alpha2"))  // Beer
                    { Bartender.handleCategorySelection("Alpha2"); return; }
                    
                if (Input.GetKeyDown("Keypad3") || Input.GetKeyDown("Alpha3"))  // Liquor
                    { Bartender.handleCategorySelection("Alpha3"); return; }

                if (Input.GetKeyDown("Keypad4") || Input.GetKeyDown("Alpha4"))  // Bitters
                    { Bartender.handleCategorySelection("Alpha4"); return; }

                if (Input.GetKeyDown("Keypad5") || Input.GetKeyDown("Alpha5"))  // Non-Alcoholic
                    { Bartender.handleCategorySelection("Alpha5"); return; }

                if (Input.GetKeyDown("Keypad6") || Input.GetKeyDown("Alpha6"))  // Other
                { Bartender.handleCategorySelection("Alpha6"); return; }

                return;
            }

            // bartender is in a menu
            if (Bartender.state != null)
            {
                // look for valid keystrokes. Return when one is found to avoid multiple input acceptance
                foreach (string key in compKeys)
                    if (Input.GetKeyDown(key))
                        { Bartender.handleDrinkConstruction(key); return; }
            }          
        }
    }
}
