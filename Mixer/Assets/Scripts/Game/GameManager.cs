using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// gameState should NEVER have more than one flag set at any given time
public enum gameState { Paused = 0, GameLoop = 1 }


public class GameManager : MonoBehaviour
{
    public static gameState state { get; private set; }
    public static int playerScore;


    void Start ()
    {
        // debug mode settings
        Drink.debugMode =               true;
        DrinkComponent.debugMode =      true;
        Bartender.debugMode =           true;
        InputManager.debugMode =        true;
        OrderManager.debugMode =        true;
        CustomerManager.debugMode =     true;
        GraphicsManager.debugMode =     true;
        PathfindingManager.debugMode =  true;

        // initialize static internal data structures
        DrinkComponent.Initialize();
        Drink.Initialize();

        state = gameState.GameLoop;
        playerScore = 0;
    }


    #region EXTERNAL FUNCTIONS

    /// <summary>
    /// Awards the player points
    /// TODO: Handle combos - currently just scored the player linearly
    /// </summary>
    public static void awardPoints()
    {
        // this will be changed based on combos later...
        int pointsToAward = 10;
        playerScore += 10;
        GraphicsManager.updateScore();
        GraphicsManager.spawnPointAward(pointsToAward);
    }


    public static void awardBonus()
    {
        // TODO: implement bonus system for clearing all drinks
        // "SWEEP"?
    }

    #endregion
}
