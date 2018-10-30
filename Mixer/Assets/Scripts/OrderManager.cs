using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    // deviation allowance from the avgTimeLimit when generating new orders
    public static float TIME_LIMIT_VARIANCE_LOWER = -.5f;
    public static float TIME_LIMIT_VARIANCE_UPPER = 1.5f;

    // deviation allowance from the avgOrderTimer 
    public static float ORDER_TIMER_VARIANCE_LOWER = -1f;
    public static float ORDER_TIMER_VARIANCE_UPPER = 1f;

    public static bool debugMode;
    public static List<Queue<Order>> orders;                                         // the queue of drink orders for each bartender position
    public static List<List<DrinkComponent>> orderProgress;                          // the DrinkComponents completed for the current drink at each bartender position

    public static float avgTimeLimit_ReductionVal { get; private set; }              // the time in seconds that the avgTimeLimit is reduced by every drink. (TODO: scale non-linearly)
    public static float avgOrderTimer_ReductionVal { get; private set; }             // the time in seconds that the avgOrderTimer is reduced by every drink. (TODO: scale non-linearly)
    public static float avgTimeLimit { get; private set; }                           // the base time limit (to be modified with a deviation) used for assigning timeLimits to Orders
    public static float avgOrderTimer { get; private set; }                          // the base time limit (to be modified with a deviation) between order spawns
    public static float nextOrderTimer { get; private set; }                         // the time remaining before another order should be spawned

    /*
     *  OrderManager is responsible for maintaining a list of all current orders
     *  in each bartender position. Furthermore, static Order keeps track of
     *  the player's current order fulfillment for each bartender position,
     *  timelimit assignment, and order generation.
     */
    // initialize static structures in OrderManager. Should be called once per level load
    // difficulty is the time in seconds that the avgTimeLimit should be reduced by every drink submission 
    public static void Initialize()
    {
        // these starting values arbitrary. TODO: pull from an instance of level
        avgTimeLimit = 5;
        avgOrderTimer = 5;
        avgTimeLimit_ReductionVal = .01f;
        avgOrderTimer_ReductionVal = .01f;


        // construct the order queues for each bartender position
        orders = new List<Queue<Order>>();
        for (int i = 0; i < Level.levels[GameManager.currentLevel].numBartenderPositions; i++)
        {
            Queue<Order> bartenderPosition = new Queue<Order>();
            orders.Add(bartenderPosition);
        }

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
    public static bool submitOrder(int position)
    {
        List<DrinkComponent> submittedOrder = orderProgress[position];
        List<DrinkComponent> originalOrder = orders[position].Peek().drink.components;

        // reject if the order and submitted order have different drinkComponent counts
        if (submittedOrder.Count != originalOrder.Count)
        {
            clearOrderProgress(position);
            return false;
        }


        // enumerate through the drinkComponents and ensure they are correct
        for (int i = 0; i < submittedOrder.Count; i++)
            if (submittedOrder[i].keySequence != originalOrder[i].keySequence)
            {
                clearOrderProgress(position);
                return false;
            }

        // the drink was correct!
        clearOrderProgress(position);
        increaseDifficulty();
        orders[position].Dequeue();
        return true;
    }


    // given a keySequence, submits the drinkComponent to the orderProgress list at position
    public static void submitComponent(string keySequence, int position)
    {
        orderProgress[position].Add(DrinkComponent.generateExternalDrinkComponent(keySequence));
    }


    // increases difficulty by reducing the avgTimeLimit and avgOrderTimer
    // this should be called once per order submission
    public static void increaseDifficulty()
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
        int position = Random.Range(0, Level.levels[GameManager.currentLevel].numBartenderPositions - 1);
        Order order = new Order();
        order.drink = drink;
        orders[position].Enqueue(order);

        if (debugMode)
            Debug.Log("Created New Drink: " + drink.drinkName);
    }
}
