using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int gameState;
    public static int numBartenderPositions = 3;


	// Use this for initialization
	void Start ()
    {
        gameState = 1;

        // debug mode settings
        InputManager.debugMode = true;
        Bartender.debugMode = true;
        Order.debugMode = true;
        Drink.debugMode = true;
        DrinkComponent.debugMode = true;

        // initialize class static structures and members
        DrinkComponent.Initialize();
        Drink.Initialize();
        Order.Initialize(numBartenderPositions);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
