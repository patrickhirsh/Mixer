using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int gameState;
    public static int currentLevel;     // index in Level.levels
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
        GraphicsManager.debugMode = true;

        // initialize class static structures and members
        DrinkComponent.Initialize();
        Drink.Initialize();
        Bartender.Initialize();
        Level.Initialize();
        OrderManager.Initialize();
        GraphicsManager.Initialize();

        loadLevel(0);
    }


    // Update is called once per frame
    void Update()
    {

    }


    // given a level, resets the game and loads that level
    public static void loadLevel(int level)
    {
        gameState = 1;
        playerScore = 0;
        GraphicsManager.updateScore();
        GameManager.currentLevel = level;
        Level currentLevel = Level.levels[GameManager.currentLevel];

        // clear existing alleys and orders
        List<GameObject> childrenToDestroy = new List<GameObject>();

        foreach (Transform alley in OrderManager.orderAlleys.transform)
        {
            foreach (Transform order in alley.transform)
                childrenToDestroy.Add(order.gameObject);
            childrenToDestroy.Add(alley.gameObject);
        }

        foreach (GameObject child in childrenToDestroy)
            Destroy(child);

        // load new alleys
        for (int i = 0; i < currentLevel.alleyPositions.Count; i++)
        {
            Instantiate(GameObject.Find("OrderAlley"), GameObject.Find("OrderAlleys").transform);
            OrderManager.orderAlleys.transform.GetChild(i).transform.position = new Vector2(currentLevel.alleyPositions[i].x, currentLevel.alleyPositions[i].y);
        }       
    } 


    // Scores the player based on their current combo
    // TODO: Handle combos - currently just scored the player linearly
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
    }

}
