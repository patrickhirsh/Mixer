using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// indicates the bartender's current menu state.
/// bartenderState should NEVER have more than one flag set at any given time
/// </summary>
public enum bartenderMenuState { noCategorySelected, glasswareCategory, beersCategory, liquorsCategory, bittersCategory, nonAlcoholicCategory, otherCategory }


/// <summary>
/// Bartender controls the bartender within the scene. Responsibilities include
/// bartender movement, holding a reference to all bartenderositions, and 
/// keeping track of the bartender's menu state.
/// </summary>
public class Bartender : MonoBehaviour
{
    public static bool debugMode;

    public static GameObject bartender { get; private set; }            // the bartender gameObject
    public static GameObject bartenderPositions { get; private set; }   // parent of all bartenderPosition objects, each with their own order children
    public static int position { get; private set; }                    // the bartenders current position (0 to numBartenderPositions - 1)
    public static bartenderMenuState menuState { get; private set; }    // the bartender's current memu state


    void Awake()
    {
        bartender = this.gameObject;
        bartenderPositions = GameObject.Find("bartender_positions");
        position = 0;
        menuState = bartenderMenuState.noCategorySelected;
    }


    #region EXTERNAL FUNCTIONS

    // the player has attempted to change the bartender's position
    // can be fired at any time when gamestate == 1 (takes precedence over menus)
    public static void handleMovement(KeyCode key)
    {
        if (key == InputManager.moveRight)
        {
            // moving causes all menus to close
            menuState = bartenderMenuState.noCategorySelected; 
            
            // handle wrap-around
            position++;
            if (position >= bartenderPositions.transform.childCount) { position = 0; }
            return;
        }

        if (key == InputManager.moveLeft)
        {
            // moving causes all menus to close
            menuState = bartenderMenuState.noCategorySelected;

            // handle wrap-around
            position--;
            if (position < 0) { position = bartenderPositions.transform.childCount - 1; }
            return;
        }
    }



    public static void handleMenuSelection(KeyCode key)
    {
        // bartender doesn't have any categories open
        if (menuState == bartenderMenuState.noCategorySelected)
        {
            if (key == InputManager.goBack)
                menuState = bartenderMenuState.noCategorySelected;

            else if (key == InputManager.category1)
                menuState = bartenderMenuState.glasswareCategory;

            else if (key == InputManager.category2)
                menuState = bartenderMenuState.beersCategory;

            else if (key == InputManager.category3)
                menuState = bartenderMenuState.liquorsCategory;

            else if (key == InputManager.category4)
                menuState = bartenderMenuState.bittersCategory;

            else if (key == InputManager.category5)
                menuState = bartenderMenuState.nonAlcoholicCategory;

            else if (key == InputManager.category6)
                menuState = bartenderMenuState.otherCategory;

            else
                if (debugMode) { Debug.Log("handleCategorySelection() couldn't parse the given key: " + key.ToString()); }

        }

        // bartender has a category open. Try to submit a component
        else
        {
            // player tried to exit the component category
            if (key == InputManager.goBack)
            {
                menuState = bartenderMenuState.noCategorySelected;
                return;
            }

            // player selected a component
            else
            {
                // if an order exists at this position, try to add the selected component
                if (bartenderPositions.transform.GetChild(position).childCount > 0)
                    bartenderPositions.transform.GetChild(position).transform.GetChild(0).GetComponent<Order>().submitDrinkComponent(key);

                // regardless of if an order exists, always step back to categories menu on component selection
                menuState = bartenderMenuState.noCategorySelected;
                return;
            }
        }      
    }

    #endregion
}
