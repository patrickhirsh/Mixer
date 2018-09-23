using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bartender : MonoBehaviour
{
    public static int position;                 // the bartenders current position (0 - GameManager.numBartenderPositions)
    public static string componentMenuState;    // the current component category. null = none.
   
    // Component Categories:
    // "Glassware"
    // "Beer"
    // "Liquor"
    // "BittersAndSyrups"
    // "OtherMixers"

    private static string keySequence;          // the players current entered keySequence relative to position. null when nothing has been entered


    // the player has attempted to change the bartender's position
    // can be fired at any time when gamestate == 1.
    // fires when the player hits the keys: UpArrow, DownArrow
    public static void handleMovement(string keystroke)
    {
        switch (keystroke)
        {
            case "UpArrow":
                componentMenuState = null;      // moving causes all menus to close
                keySequence = null;             // moving clears the current keySequence
                position++;
                if (position >= GameManager.numBartenderPositions)
                    position = GameManager.numBartenderPositions - 1;
                break;

            case "DownArrow":
                componentMenuState = null;      // moving causes all menus to close
                keySequence = null;             // moving clears the current keySequence
                position--;
                if (position < 0)
                    position = 0;
                break;
        }
    }


    // the player has tried to submit a drink at this position
    // can be fired at any time when gamestate == 1.
    // fires when the player hits the keys: UpArrow, DownArrow
    public static void handleSubmit()
    {
        componentMenuState = null;              // submitting causes all menus to close
        keySequence = null;                     // submitting clears the current keySequence
        Order.submitOrder(position);
    }


    // the player has opened a drink components category
    // can only be fired when componentMenuState == null
    // fires when the player hits keypad OR alpha 1-5 (keystroke should be Alpha#)
    public static void handleCategorySelection(string keystroke)
    {
        switch (keystroke)
        {
            case "Alpha1":
                componentMenuState = "Glassware";
                break;

            case "Alpha2":
                componentMenuState = "Beer";
                break;

            case "Alpha3":
                componentMenuState = "Liquor";
                break;

            case "Alpha4":
                componentMenuState = "BittersAndSyrups";
                break;

            case "Alpha5":
                componentMenuState = "OtherMixers";
                break;
        }
    }


    // the player is entering a component keySequence
    // can only fire if componentMenuState != null
    // valid keys are ALL in InputManager.compKeys
    public static void handleDrinkConstruction(string keystroke)
    {
        // submit the component
        if (keystroke == "Space")
        {
            Order.submitComponent(keySequence, position);
            componentMenuState = null;          // submitting a component causes all menus to close
            keySequence = null;                 // submitting a component clears the current keySequence
            return;
        }

        // exit the component category
        if ((keystroke == "LeftAlt") || (keystroke == "RightAlt"))
        {
            componentMenuState = null;          // exit the component category
            keySequence = null;                 // exiting the category clears the current keySequence
            return;
        }

        // remove the last character from keySequence and keep listening for more
        if (keystroke == "Backspace")
            if ((keySequence != null) && (keySequence.Length > 0))
                { keySequence.Remove(keySequence.Length - 1); return; }

        // otherwise, accept the keystroke as an addition to keySequence and keep listening for more
        if (keySequence == null) { keySequence = keystroke; }          
        else { keySequence = keySequence + keystroke; }
            
    }
}
