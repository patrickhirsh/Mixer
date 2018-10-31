using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsManager : MonoBehaviour
{
    private static float ORDER_SPRITE_SPACING = .5f;
    private static float CURRENT_DRINK_COMPONENT_SPACING = .3f;

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
        // does there exist an order at the current bartending position?
        if (OrderManager.orderAlleys.transform.GetChild(Bartender.position).transform.childCount != 0)
        {
            // set the title at the top of the panel
            currentDrinkTitle.GetComponent<Text>().text =
                OrderManager.orderAlleys.transform.GetChild(Bartender.position).transform.GetChild(0).GetComponent<Order>().drink.drinkName;

            // destroy the existing child objects (ingredients)
            List<GameObject> childrenToDestroy = new List<GameObject>();
            for (int i = 1; i < currentDrinkPanel.transform.childCount; i++)
                childrenToDestroy.Add(currentDrinkPanel.transform.GetChild(i).gameObject);
            foreach (GameObject child in childrenToDestroy)
                Destroy(child);

            // enumerate through the DrinkComponents in the order at the bartender's positions' current drink
            int pos = 1;
            foreach (DrinkComponent component in OrderManager.orderAlleys.transform.GetChild(Bartender.position).GetChild(0).GetComponent<Order>().drink.components)
            {
                GameObject comp = Instantiate(GameObject.Find("DrinkTitle"), GameObject.Find("CurrentDrink").transform);
                comp.GetComponent<Text>().text = component.component;
                comp.transform.position = new Vector2(
                    currentDrinkTitle.transform.position.x + .5f,
                    currentDrinkTitle.transform.position.y - (pos * CURRENT_DRINK_COMPONENT_SPACING));
                pos++;
            }
        }

        else
        {
            // clear current drink title
            currentDrinkTitle.GetComponent<Text>().text = "";

            // remove existing ingredients when no order exists
            List<GameObject> childrenToDestroy = new List<GameObject>();
            for (int i = 1; i < currentDrinkPanel.transform.childCount; i++)
                childrenToDestroy.Add(currentDrinkPanel.transform.GetChild(i).gameObject);
            foreach (GameObject child in childrenToDestroy)
                Destroy(child);
        }
            


    }


}
