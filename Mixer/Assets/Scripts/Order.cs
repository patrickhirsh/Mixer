using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{   
    // STATIC
    public static bool debugMode;  
    public static List<Queue<Order>> orders { get; private set; }                    // keeps track of the queue of drink orders for each bartender position
    public static List<List<DrinkComponent>> orderProgress { get; private set; }     // keeps track of the DrinkComponents completed for the current drink at each bartender position

    // INSTANCE
    public Drink drink { get; private set; }                                         // the drink associated with this order 


    #region STATIC
    /*
     *  STATIC Order is responsible for maintaining a list of all current orders
     *  in each bartender position. Furthermore, static Order keeps track of
     *  the player's current order fulfillment for each bartender position.
     */

    // initialize static structures in Order. Should be called once per level load
    public static void Initialize(int numBartenderPositions)
    {
        // construct the order queues for each bartender position
        orders = new List<Queue<Order>>();
        for (int i = 0; i < numBartenderPositions; i++)
        {
            Queue<Order> bartenderPosition = new Queue<Order>();
            orders.Add(bartenderPosition);
        }

        // construct the order progress list for each bartender position
        orderProgress = new List<List<DrinkComponent>>();
        for (int i = 0; i < numBartenderPositions; i++)
        {
            List<DrinkComponent> bartenderPosition = new List<DrinkComponent>();
            orderProgress.Add(bartenderPosition);
        }
    }


    // "tosses" the drink in progress at position
    public static void clearOrder(int position)
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
            return false;

        // enumerate through the drinkComponents and ensure they are correct
        for (int i = 0; i < submittedOrder.Count; i++)
            if (submittedOrder[i].keySequence != originalOrder[i].keySequence)
                return false;

        // the drink was correct!
        return true;
    }


    // given a keySequence, submits the drinkComponent to the orderProgress list at position
    public static void submitComponent(string keySequence, int position)
    {
        orderProgress[position].Add(DrinkComponent.generateExternalDrinkComponent(keySequence));
    }


    // given a drink, adds the order to the queue at position
    private static void newOrder(Drink drink, int position)
    {
        Order order = new Order();
        order.drink = drink;
        orders[position].Enqueue(order);
    }

    #endregion


    #region INSTANCE
    /*
     *  An INSTANCE of Order contains all information pertaining to a customer's 
     *  drink order including the drink type and time of order.Order is then
     *  responsible for keeping track of the time limit on an order and notifying
     *  GameManager when time has run out.
     */

    // TODO: Implement order timing

    #endregion
}
