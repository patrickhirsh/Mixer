using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// gameState should NEVER have more than one flag set at any given time
public enum gameState { menuScreen = 0, mainGame = 1 }


public class GameManager : MonoBehaviour
{
    public static gameState state { get; private set; }
    public static int playerScore;      

    // Use this for initialization
    void Start ()
    {
        // debug mode settings
        Drink.debugMode = true;
        DrinkComponent.debugMode = true;
        Bartender.debugMode = true;
        InputManager.debugMode = true;
        OrderManager.debugMode = true;
        CustomerManager.debugMode = true;
        GraphicsManager.debugMode = true;

        // initialize class static structures and members
        DrinkComponent.Initialize();
        Drink.Initialize();
        Bartender.Initialize();
        OrderManager.Initialize();
        CustomerManager.Initialize();
        GraphicsManager.Initialize();

        // TODO: don't immediately transition to mainGame (need a main menu)
        state = gameState.mainGame;
        playerScore = 0;
    }


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

}
