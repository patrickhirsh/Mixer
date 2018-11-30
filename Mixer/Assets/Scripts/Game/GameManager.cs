using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// indicates the current state of this scene.
/// GameState should NEVER have more than one flag set at any given time
/// </summary>
public enum GameState { Paused = 0, GameLoop = 1 }


/// <summary>
/// Game Manager is responsible for controlling the primary game loop.
/// Game Manager also keeps track of the player's score, bonuses, and other
/// events occuring during gameplay. Finally, Game Manager is responsible for
/// managing the debug state of ALL other classes as well as ensuring the proper
/// static structures for these classes are initialized upon a scene load.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameState state { get; private set; }         // the game's current state
    public static int playerScore { get; private set; }         // the player's current score
    public static int playerOrderMisses { get; private set; }   // the player's number of missed orders


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
        NodeManager.debugMode =         true;

        // initialize static internal data structures
        DrinkComponent.Initialize();
        Drink.Initialize();

        state = GameState.GameLoop;
        playerScore = 0;
        playerOrderMisses = 0;

        GraphicsManager.updateAllGraphics();
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


    /// <summary>
    /// Called when the player misses an order
    /// </summary>
    public static void orderMiss()
    {
        playerOrderMisses++;  
        
        // don't allow any misses after gameOver() to trigger graphics update
        if (playerOrderMisses == 3) { gameOver(); }
        if (playerOrderMisses < 4) { GraphicsManager.updatePlayerOrderMisses(); }       
    }


    /// <summary>
    /// Ends the game
    /// </summary>
    public static void gameOver()
    {
        // TODO: do cool "Game Over" screen here, with PostProcessing Stack, perhaps?
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}
