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

        // debug mode settings
        InputManager.debugMode = true;
        Bartender.debugMode = true;
        OrderManager.debugMode = true;
        Drink.debugMode = true;
        DrinkComponent.debugMode = true;

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


    public static void loadLevel(int level)
    {
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
}
