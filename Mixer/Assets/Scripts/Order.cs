using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public Drink drink;         // the drink associated with this order  
    public float timeLeft;      // time remaining for the player to complete the order

    /*
     *  An instance of Order contains all information pertaining to a customer's 
     *  drink order including the drink type and time of order. Order is then
     *  responsible for keeping track of the time limit on an order and notifying
     *  GameManager when time has run out.
     */

    void Start()
    {
        // determine the order's timeLimit using OrderManager's variance and avgTimeLimit
        float deviation = UnityEngine.Random.Range(OrderManager.TIME_LIMIT_VARIANCE_LOWER, OrderManager.TIME_LIMIT_VARIANCE_UPPER);

        // make sure the deviation doesn't create an invalid timeLimit
        if ((OrderManager.avgTimeLimit + deviation) <= 0)
            timeLeft = OrderManager.avgTimeLimit;
        else { timeLeft = OrderManager.avgTimeLimit + deviation; }
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        // check for if we're out of time and react accordingly.
    }
}
