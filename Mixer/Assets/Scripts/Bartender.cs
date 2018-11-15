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
        bartender = GameObject.Find("bartender");
        position = 0;
        state = null;
    }


    // the player has attempted to change the bartender's position
    // can be fired at any time when gamestate == 1 (takes precedence over menus)
    public static void handleMovement(KeyCode key)
    {
        if (key == InputManager.moveRight)
        {
            // moving causes all menus to close
            state = null; 
            
            // handle wrap-around
            position++;
            if (position >= Level.levels[GameManager.currentLevel].numBartenderPositions)
                position = 0;

            if (debugMode)
                Debug.Log("Bartender State: " + state); ;
            return;
        }

        if (key == InputManager.moveLeft)
        {
            // moving causes all menus to close
            state = null;

            // handle wrap-around
            position--;
            if (position < 0)               
                position = Level.levels[GameManager.currentLevel].numBartenderPositions - 1;

            if (debugMode)
                Debug.Log("Bartender State: " + state); ;
            return;
        }
    }


    // the player has opened a drink components category
    // can only be fired when componentMenuState == null
    public static void handleCategorySelection(KeyCode key)
    {
        if (key == InputManager.goBack)
            state = null;

        else if (key == InputManager.category1)
            state = "Glassware";

        else if (key == InputManager.category2)
            state = "Beers";

        else if (key == InputManager.category3)
            state = "Liquors";

        else if (key == InputManager.category4)
            state = "Bitters";

        else if (key == InputManager.category5)
            state = "NonAlcoholic";

        else if (key == InputManager.category6)
            state = "Other";

        else if (key == InputManager.category7)
            state = null;   // unused

        else if (key == InputManager.category8)
            state = null;   // unused

        else if (key == InputManager.category9)
            state = null;   // unused

        else
            if (debugMode) { Debug.Log("handleCategorySelection() couldn't parse the given key: " + key.ToString()); }

        if (debugMode) { Debug.Log("Bartender State: " + state); }
    }


    // the player is selecting a component
    // can only fire if componentMenuState != null
    public static void handleComponentSelection(KeyCode key)
    {
        // player tried to exit the component category
        if (key == InputManager.goBack)
        {
            state = null;                           
            if (debugMode) { Debug.Log("Bartender State: " + state); ; }
            return;
        }

        // player selected a component
        else
        {                       
            OrderManager.submitComponent(key, position);
            state = null;
            if (debugMode) { Debug.Log("Bartender State: " + state); ; }
            return;
        }
    }
}
