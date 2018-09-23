using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // used to check for component keystrokes in handleInput()
    private static string[] compKeys = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
        "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "Space", "RightAlt", "LeftAlt", "Backspace" };

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    private void handleInput()
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

            // bartender is not currently in a menu
            if (Bartender.componentMenuState == null)
            {
                // bartender category selection
                if (Input.GetKeyDown("Keypad1") || Input.GetKeyDown("Alpha1"))  // Glassware
                    { Bartender.handleCategorySelection("Alpha1"); return; }

                if (Input.GetKeyDown("Keypad2") || Input.GetKeyDown("Alpha2"))  // Beer
                    { Bartender.handleCategorySelection("Alpha2"); return; }
                    
                if (Input.GetKeyDown("Keypad3") || Input.GetKeyDown("Alpha3"))  // Liquor
                    { Bartender.handleCategorySelection("Alpha3"); return; }

                if (Input.GetKeyDown("Keypad4") || Input.GetKeyDown("Alpha4"))  // Bitters/Syrups
                    { Bartender.handleCategorySelection("Alpha4"); return; }

                if (Input.GetKeyDown("Keypad5") || Input.GetKeyDown("Alpha5"))  // Other Mixers
                    { Bartender.handleCategorySelection("Alpha5"); return; }

                return;
            }

            // bartender is in a menu
            if (Bartender.componentMenuState != null)
            {
                // look for valid keystrokes
                foreach (string key in compKeys)
                    if (Input.GetKeyDown(key))
                        { Bartender.handleDrinkConstruction(key); return; }
            }          
        }
    }
}
