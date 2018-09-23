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
        DrinkComponent.Initialize();
        Drink.Initialize();
        Order.Initialize(numBartenderPositions);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
