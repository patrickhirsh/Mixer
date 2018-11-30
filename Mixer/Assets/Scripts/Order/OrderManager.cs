using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// OrderManager handles order spawning. Since orders hold a few invariants that should always be followed
/// upon instantiation (such as: they must always be initialized, and they must always be a child of the associated
/// BartenderPosition), it makes sense to create a simple wrapper for creating new orders to ensure orders
/// are always created correctly.
/// </summary>
public class OrderManager : MonoBehaviour
{
    public static bool debugMode;
    private static GameObject orderPrefab;


    void Start()
    {
        orderPrefab = Resources.Load("Prefabs/order") as GameObject;
    }


    #region EXTERNAL FUNCTIONS

    /// <summary>
    /// add a new order from "customer" containing "drink" at bartender position "position"
    /// </summary>
    public static Order newOrder(Customer customer, Drink drink, GameObject bartenderPosition)
    {
        // create the order and add it to the bartenderPosition indexed by "position"
        GameObject orderObject = Instantiate(orderPrefab, bartenderPosition.transform);
        Order order = orderObject.GetComponent<Order>();
        order.Initialize(customer, drink);

        if (debugMode) { Debug.Log("Created new order: " + drink.drinkName + " at bartender position: " + bartenderPosition); }           
        return order;       
    }

    #endregion
}
