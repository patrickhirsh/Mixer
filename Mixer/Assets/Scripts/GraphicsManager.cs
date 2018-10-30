using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsManager : MonoBehaviour
{
    private static float ORDER_SPRITE_SPACING = .5f;

    private static GameObject currentDrinkPanel;
    private static GameObject currentDrinkTitle;

    public static void Initialize()
    {
        currentDrinkPanel = GameObject.Find("CurrentDrink");
        currentDrinkTitle = GameObject.Find("DrinkTitle");
    }


    // Update is called once per frame
    void Update ()
    {
        renderBartender();
        renderOrders();
        renderCurrentDrinkPanel();
    }


    // renders the bartender's position on-screen based on Bartender.position
    private static void renderBartender()
    {
        Bartender.bartender.transform.position = 
            Level.levels[GameManager.currentLevel].bartenderPositions[Bartender.position];
    }


    // renders the order sprites based on alley position and ORDER_SPRITE_SPACING
    private static void renderOrders()
    {
        foreach (Transform alley in OrderManager.orderAlleys.transform)
        {
            for (int i = 0; i < alley.childCount; i++)
            {
                alley.GetChild(i).position = new Vector2(
                    alley.position.x - ((i + 1) * ORDER_SPRITE_SPACING),
                    alley.position.y);
            }
        }
    }


    private static void renderCurrentDrinkPanel()
    {
        if (OrderManager.orderAlleys.transform.GetChild(Bartender.position).transform.childCount != 0)
            currentDrinkTitle.GetComponent<Text>().text =
                OrderManager.orderAlleys.transform.GetChild(Bartender.position).transform.GetChild(0).GetComponent<Order>().drink.drinkName;

        else
            currentDrinkTitle.GetComponent<Text>().text =
                "";

        Debug.Log(currentDrinkTitle.GetComponent<Text>().text);
    }


}
