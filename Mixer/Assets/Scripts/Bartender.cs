using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bartender : MonoBehaviour
{
    public static GameObject bartender;

    public static bool debugMode;
    public static int position { get; private set; }            // the bartenders current position (0 to level.numbartenderpositions-1)
    public static string state { get; private set; }            // null when the bartender has no menus open. Otherwise, stores the name of the bartender's current DrinkComponent category
    public static string keySequence { get; private set; }      // the players current entered keySequence relative to position. null when nothing has been entered


    // initialize static members of the Bartender class
    public static void Initialize()
    {
        bartender = GameObject.Find("Bartender");
        position = 0;
        state = null;
        keySequence = null;
    }


    // the player has attempted to change the bartender's position
    // can be fired at any time when gamestate == 1
    // fires when the player hits the keys: UpArrow, DownArrow
    public static void handleMovement(string keystroke)
    {
        switch (keystroke)
        {
            case "UpArrow":
                state = null;                   // moving causes all menus to close
                keySequence = null;             // moving clears the current keySequence
                position++;
                if (position >= Level.levels[GameManager.currentLevel].numBartenderPositions)
                    position = 0;
                if (debugMode) { Debug.Log(state); }
                break;

            case "DownArrow":
                state = null;                   // moving causes all menus to close
                keySequence = null;             // moving clears the current keySequence
                position--;
                if (position < 0)
                    position = Level.levels[GameManager.currentLevel].numBartenderPositions - 1;
                if (debugMode) { Debug.Log(state); }
                break;
        }
    }


    // the player has tried to submit a drink at this position
    // can be fired at any time when gamestate == 1
    // fires when the player hits the key: 0
    public static void handleSubmit()
    {
        state = null;                           // submitting causes all menus to close
        keySequence = null;                     // submitting clears the current keySequence
        bool result = OrderManager.submitOrder(position);

        if (debugMode)
        {
            if (result) { Debug.Log("The submitted drink was valid!"); }
            if (!result) { Debug.Log("The submitted drink was invalid"); }
            Debug.Log(state);
        }
    }


    // the player has elected to clear the drink at this position
    // can be fired at any time when gamestate == 1
    // fires when the player hits the Delete key
    public static void handleClear()
    {
        state = null;                           // clearing causes all menus to close
        keySequence = null;                     // clear the current keySequence
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
                keySequence = null;
                break;

            case "Alpha2":
                state = "Beer";
                keySequence = null;
                break;

            case "Alpha3":
                state = "Liquor";
                keySequence = null;
                break;

            case "Alpha4":
                state = "Bitters";
                keySequence = null;
                break;

            case "Alpha5":
                state = "NonAlcoholic";
                keySequence = null;
                break;

            case "Alpha6":
                state = "Other";
                keySequence = null;
                break;
        }

        if (debugMode) { Debug.Log(state); }
    }


    // the player is entering a component keySequence
    // can only fire if componentMenuState != null
    // valid keys are ALL in InputManager.compKeys
    public static void handleDrinkConstruction(string keystroke)
    {
        // submit the component
        if (keystroke == "Space")
        {
            OrderManager.submitComponent(keySequence, position);
            state = null;                       // submitting a component causes all menus to close
            keySequence = null;                 // submitting a component clears the current keySequence
            if (debugMode) { Debug.Log(state); }
            return;
        }

        // exit the component category
        if ((keystroke == "LeftAlt") || (keystroke == "RightAlt"))
        {
            state = null;                       // exit the component category
            keySequence = null;                 // exiting the category clears the current keySequence
            if (debugMode) { Debug.Log(state); }
            return;
        }

        // remove the last character from keySequence and keep listening for more
        if (keystroke == "Backspace")
            if ((keySequence != null) && (keySequence.Length > 0))
                { keySequence.Remove(keySequence.Length - 1); return; }

        // otherwise, accept the keystroke as an addition to keySequence and keep listening for more
        if (keySequence == null) { keySequence = keystroke; }          
        else { keySequence = keySequence + keystroke; }       
        
        if (debugMode) { Debug.Log("Current Input: " + keySequence); }
    }
}
