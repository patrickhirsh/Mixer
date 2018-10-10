using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int gameState;
    public static int currentLevel;     // index in Level.levels


	// Use this for initialization
	void Start ()
    {
        gameState = 1;
        currentLevel = 0;

        // debug mode settings
        InputManager.debugMode = true;
        Bartender.debugMode = true;
        Order.debugMode = true;
        Drink.debugMode = true;
        DrinkComponent.debugMode = true;

        // initialize class static structures and members
        DrinkComponent.Initialize();
        Drink.Initialize();
        Bartender.Initialize();
        Level.Initialize();
        Order.Initialize();
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
