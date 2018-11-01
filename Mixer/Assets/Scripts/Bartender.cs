using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bartender : MonoBehaviour
{
    public static GameObject bartender;

    public static bool debugMode;
    public static int position { get; private set; }            // the bartenders current position (0 to level.numbartenderpositions-1)
    public static string state { get; private set; }            // null when the bartender has no menus open. Otherwise, stores the name of the bartender's current DrinkComponent category


    // initialize static members of the Bartender class
    public static void Initialize()
    {
        bartender = GameObject.Find("Bartender");
        position = 0;
        state = null;
    }


    // the player has attempted to change the bartender's position
    // can be fired at any time when gamestate == 1
    // fires when the player hits space. Always moves the bartender up (with wrap around)
    public static void handleMovement()
    {
        state = null;                   // moving causes all menus to close
        position++;
        if (position >= Level.levels[GameManager.currentLevel].numBartenderPositions)
            position = 0;
        if (debugMode) { Debug.Log(state); }
    }


    // the player has elected to clear the drink at this position
    // can be fired at any time when gamestate == 1
    // fires when the player hits the Delete key
    public static void handleClear()
    {
        state = null;                           // clearing causes all menus to close
        OrderManager.clearOrderProgress(position);
        if (debugMode)
        {
            Debug.Log("Drink cleared at bartender position " + position);
            Debug.Log(state);
        }
    }


    // the player has opened a drink components category
    // can only be fired when componentMenuState == null
    // fires when the player hits keypad OR alpha 1-6 (keystroke should be Alpha#)
    public static void handleCategorySelection(string keystroke)
    {
        switch (keystroke)
        {
            case "Alpha1":
                state = "Glassware";
                break;

            case "Alpha2":
                state = "Beers";
                break;

            case "Alpha3":
                state = "Liquors";
                break;

            case "Alpha4":
                state = "Bitters";
                break;

            case "Alpha5":
                state = "NonAlcoholic";
                break;

            case "Alpha6":
                state = "Other";
                break;
        }

        if (debugMode) { Debug.Log(state); }
    }


    // the player is selecting a component
    // can only fire if componentMenuState != null
    // valid keys are ALL in InputManager.compKeys
    public static void handleDrinkConstruction(string keystroke)
    {
        // player tried to exit the component category
        if ((keystroke == "LeftAlt") || (keystroke == "RightAlt"))
        {
            state = null;                           
            if (debugMode) { Debug.Log(state); }
            return;
        }

        // player selected a component
        else
        {                       
            OrderManager.submitComponent(keystroke, position);
            state = null;
            if (debugMode) { Debug.Log(state); }
            return;
        }
    }
}
