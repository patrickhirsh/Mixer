using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// OrderManager keeps track of the player's current order fulfillment for each bartender position. OrderManager 
/// is also responsible for keeping track of timing standards for generating new order timeLimits and new order 
/// spacing as well as actually generating these new orders (based on increasing difficulty curves)
/// </summary>
public class OrderManager : MonoBehaviour
{
    // traverse this object to find organized "queues" of orders
    public static GameObject orderAlleys;                                           // (This Object) -> orderAlley -> Order GameObjects for that position

    // deviation allowance from the avgTimeLimit when generating new orders
    public static float TIME_LIMIT_VARIANCE_LOWER = -.5f;
    public static float TIME_LIMIT_VARIANCE_UPPER = 1.5f;

    // deviation allowance from the avgOrderTimer 
    public static float ORDER_TIMER_VARIANCE_LOWER = -1f;
    public static float ORDER_TIMER_VARIANCE_UPPER = 1f;

    public static bool debugMode;
    public static List<List<DrinkComponent>> orderProgress;                          // the DrinkComponents completed for the current drink at each bartender position

    public static float avgTimeLimit_ReductionVal { get; private set; }              // the time in seconds that the avgTimeLimit is reduced by every drink. (TODO: scale non-linearly)
    public static float avgOrderTimer_ReductionVal { get; private set; }             // the time in seconds that the avgOrderTimer is reduced by every drink. (TODO: scale non-linearly)
    public static float avgTimeLimit { get; private set; }                           // the base time limit (to be modified with a deviation) used for assigning timeLimits to Orders
    public static float avgOrderTimer { get; private set; }                          // the base time limit (to be modified with a deviation) between order spawns
    public static float nextOrderTimer { get; private set; }                         // the time remaining before another order should be spawned
    
    
    // initialize static structures in OrderManager. Should be called once per level load
    // difficulty is the time in seconds that the avgTimeLimit should be reduced by every drink submission 
    public static void Initialize()
    {
        orderAlleys = GameObject.Find("OrderAlleys");

        // these starting values arbitrary. TODO: pull these from an instance of level
        avgTimeLimit = 10;
        avgOrderTimer = 10;
        avgTimeLimit_ReductionVal = .01f;
        avgOrderTimer_ReductionVal = .01f;

        // construct the order progress list for each bartender position
        orderProgress = new List<List<DrinkComponent>>();
        for (int i = 0; i < Level.levels[GameManager.currentLevel].numBartenderPositions; i++)
        {
            List<DrinkComponent> bartenderPosition = new List<DrinkComponent>();
            orderProgress.Add(bartenderPosition);
        }
    }


    // Update is called once per frame
    void Update()
    {
        nextOrderTimer -= Time.deltaTime;

        // spawn a new order and reset the timer (timer is at 0)
        if (nextOrderTimer <= 0)
        {
            // TODO: add drink tier difficulty ramp based on level difficulty (constant, for now)
            newOrder(Drink.getRandomDrink(0));

            // reset nextOrderTimer
            float deviation = UnityEngine.Random.Range(ORDER_TIMER_VARIANCE_LOWER, ORDER_TIMER_VARIANCE_UPPER);

            // make sure the deviation in avgOrderTimer doesn't create an invalid timeLimit
            if ((avgOrderTimer + deviation) <= 0)
                nextOrderTimer = avgOrderTimer;
            else { nextOrderTimer = avgOrderTimer + deviation; }
        }
    }


    // clears the drink in progress at position
    public static void clearOrderProgress(int position)
    {
        orderProgress[position].Clear();
    }


    // attempts to submit the order at position
    // returns true if the submission was accepted and false if it was rejected
    // if the submission was empty, accept
    public static bool submitOrder(int position)
    {
        // always accept empty submissions
        if (orderProgress[position].Count == 0)
        {
            if (debugMode) { Debug.Log("Empty submission. Accepted"); }
            return true;
        }

        // if there isn't an order at this position... reject
        if (orderAlleys.transform.GetChild(position).transform.childCount == 0)
        {
            clearOrderProgress(position);
            if (debugMode) { Debug.Log("An order doesn't exist at this location. Rejected."); }
            return false;
        }

        List<DrinkComponent> submittedOrder = orderProgress[position];
        List<DrinkComponent> originalOrder = 
            orderAlleys.transform.GetChild(position).transform.GetChild(orderAlleys.transform.GetChild(position).transform.childCount - 1).GetComponent<Order>().drink.components;

        // reject if the order and submitted order have different drinkComponent counts
        if (submittedOrder.Count != originalOrder.Count)
        {
            clearOrderProgress(position);
            if (debugMode) { Debug.Log("Invalid number of drink components"); }
            return false;
        }


        // enumerate through the drinkComponents and ensure they are correct
        for (int i = 0; i < submittedOrder.Count; i++)
            if (submittedOrder[i].keySequence.ToLower() != originalOrder[i].keySequence.ToLower())
            {
                clearOrderProgress(position);
                if (debugMode) { Debug.Log("Incorrect drink components"); }
                return false;
            }

        // the drink was correct!
        clearOrderProgress(position);
        increaseDifficulty();
        orderAlleys.transform.GetChild(position).GetComponent<OrderAlley>().removeCurrentOrder();
        if (debugMode) { Debug.Log("Successfully submitted a drink!"); }
        return true;
    }


    // given a keySequence, submits the drinkComponent to the orderProgress list at position
    public static void submitComponent(string keySequence, int position)
    {
        orderProgress[position].Add(DrinkComponent.generateExternalDrinkComponent(keySequence));
    }


    // increases difficulty by reducing the avgTimeLimit and avgOrderTimer
    // this should be called once per order submission
    private static void increaseDifficulty()
    {
        // reduce the average amount of time the player has to submit an order
        if ((avgTimeLimit - avgTimeLimit_ReductionVal) > 0)
            avgTimeLimit -= avgTimeLimit_ReductionVal;

        // reduce the average amount of time between order spawns
        if ((avgOrderTimer - avgOrderTimer_ReductionVal) > 0)
            avgOrderTimer -= avgOrderTimer_ReductionVal;
    }


    // generates and adds a new order to the queue
    private static void newOrder(Drink drink)
    {
        // since we're casting to int (and thus rounding down), we need the full range of top position -> top position .999... to get an even probability
        int position = (int)Random.Range(0, Level.levels[GameManager.currentLevel].numBartenderPositions - .000001f);

        // create the order and add it the orderAlleys at position
        Instantiate(GameObject.Find("Order"), GameObject.Find("OrderAlleys").transform.GetChild(position));

        // assign drink to our new order
        orderAlleys.transform.GetChild(position).GetChild(orderAlleys.transform.GetChild(position).transform.childCount - 1).GetComponent<Order>().drink = drink;

        if (debugMode)
            Debug.Log("Created new drink: " + drink.drinkName +  " at bartender position: " + position);
    }
}
