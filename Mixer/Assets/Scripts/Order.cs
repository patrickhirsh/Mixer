using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public static List<Queue<Order>> orders;                    // keeps track of the queue of drink orders for each bartender position
    public static List<List<DrinkComponent>> orderProgress;     // keeps track of the DrinkComponents completed for the current drink at each bartender position


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    
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

    public static void newOrder(Order order, int bartenderPosition)
    {

    }

    public static bool submitOrder(int bartenderPosition)
    {

    }

    public static void submitComponent(string keySequence, int bartenderPosition)
    {

    }
}
