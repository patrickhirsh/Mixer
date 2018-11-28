using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// OrderManager keeps track of the player's current order fulfillment for each bartender position. OrderManager 
/// is also responsible for keeping track of timing standards for generating new order timeLimits and new order 
/// spacing as well as actually generating these new orders (based on increasing difficulty curves)
/// </summary>
public class OrderManager : MonoBehaviour
{
    public static bool debugMode;

    // prefab used to spawn orders
    private static GameObject orderPrefab;

    // constants for order timing algorithm (all in seconds)
    private static float STARTING_TIME_LIMIT = 20f;                         // starting time limit on new orders (reduced over time)
    private static float STARTING_TIME_LIMIT_REDUCTION_VAL = .01f;          // value at which to reduce timeLimit by each increaseDifficulty() call

    // variables for spawn order timing algorithm 
    public static float timeLimit { get; private set; }                     // used for assigning timeLimits to Orders
    private static float timeLimeLimit_ReductionVal;                        // the time in seconds that the avgTimeLimit is reduced by every drink. (TODO: scale non-linearly)

    // the DrinkComponents completed for the current drink at each bartender position
    public static List<List<DrinkComponent>> orderProgress;


    /// <summary>
    /// initialize static structures in OrderManager. Should be called once per level load
    /// </summary>
    public static void Initialize()
    {
        timeLimit = STARTING_TIME_LIMIT;       
        timeLimeLimit_ReductionVal = STARTING_TIME_LIMIT_REDUCTION_VAL;
        orderPrefab = Resources.Load("Prefabs/order") as GameObject;

        // construct the order progress list for each bartender position
        orderProgress = new List<List<DrinkComponent>>();
        for (int i = 0; i < Bartender.numPositions; i++)
        {
            List<DrinkComponent> bartenderPosition = new List<DrinkComponent>();
            orderProgress.Add(bartenderPosition);
        }
    }


    /// <summary>
    /// add a new order from "customer" containing "drink" at bartender position "position"
    /// </summary>
    public static Order newOrder(Customer customer, Drink drink, GameObject bartenderPosition)
    {
        // create the order and add it to the bartenderPosition indexed by "position"
        GameObject orderObject = Instantiate(orderPrefab, bartenderPosition.transform);
        Order order = orderObject.GetComponent<Order>();
        order.Initialize(customer, drink);

        if (debugMode)
            Debug.Log("Created new order: " + drink.drinkName + " at bartender position: " + bartenderPosition);

        return order;       
    }


    /// <summary>
    /// clears the drink in progress at position.
    /// happens either when a drink is failed, or submitted successfully.
    /// each drink clear increases difficulty.
    /// </summary>
    public static void clearOrderProgress(int position)
    {
        orderProgress[position].Clear();
        increaseDifficulty();
    }


    /// <summary>
    /// given a KeyCode, submits the drinkComponent to the orderProgress list at position.
    /// validates the submitted component and reacts accordingly.
    /// </summary>
    public static void submitComponent(KeyCode key, int position)
    {
        // add the component to the order in progress and note its index
        orderProgress[position].Add(DrinkComponent.generateExternalDrinkComponent(key, DrinkComponent.categoryLabels[Bartender.menuState]));
        int compIndex = orderProgress[position].Count - 1;

        // if there isn't an order at this position... reject
        if (Bartender.bartenderPositions.transform.GetChild(position).transform.childCount == 0)
        {
            clearOrderProgress(position);
            if (debugMode) { Debug.Log("A component was submitted to a position that contains no orders"); }
        }

        // obtain a reference to the order we're checking against
        Order order = Bartender.bartenderPositions.transform.GetChild(position).transform.GetChild(0).GetComponent<Order>();

        // if the submitted component doesn't have the same key input OR doesn't have the same category as the recipe...
        if ((orderProgress[position][compIndex].key.ToString().ToUpper() != order.drink.components[compIndex].key.ToString().ToUpper()) ||      // same key?
            (orderProgress[position][compIndex].category.ToUpper() != order.drink.components[compIndex].category.ToUpper()))                    // same category?
        {
            clearOrderProgress(position);
            if (debugMode) { Debug.Log("Invalid drink component submitted. Clearing drink progress"); }
        }

        // otherwise, the component was valid
        // check to see if this was the last component (completing the drink)
        if (orderProgress[position].Count == order.drink.components.Count)
        {
            // drink completed successfully!
            GameManager.awardPoints();
            clearOrderProgress(position);
            order.destroy(true);
            CustomerManager.increaseDifficulty();
            if (debugMode) { Debug.Log("Successfully completed a drink!"); }
        }
    }


    /// <summary>
    /// Decreases the ammount of time the player has to submit each new order
    /// Currently called every time a new order is submitted (this should probably change)
    /// </summary>
    public static void increaseDifficulty()
    {
        // reduce the average amount of time the player has to submit an order
        if ((timeLimit - timeLimeLimit_ReductionVal) > 0)
            timeLimit -= timeLimeLimit_ReductionVal;
    }
}
