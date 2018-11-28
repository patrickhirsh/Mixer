using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An instance of Order contains all information pertaining to a customer's 
/// drink order including the drink type and time of order. Order is then
/// responsible for keeping track of the time limit on an order and notifying
/// the customer when the order is removed (on either a successful submission,
/// or a removal due to running out of time)
/// </summary>
public class Order : MonoBehaviour
{
    private static readonly float ORDER_TIME_LIMIT = 10f;   // starting time limit assigned to all new orders

    public Customer customer { get; private set; }          // the customer who ordered this drink
    public Drink drink { get; private set; }                // the drink associated with this order  
    public float timeLeft { get; private set; }             // time remaining for the player to complete the order


    void Start()
    {
        timeLeft = ORDER_TIME_LIMIT;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        
        // the order has timed out
        if (timeLeft <= 0)
        {
            GameManager.orderMiss();
            destroy(false);
        }
    }


    /// <summary>
    /// Initializes the data associated with this order. Should ALWAYS be called after instantiation
    /// </summary>
    public void Initialize(Customer customer, Drink drink)
    {
        this.customer = customer;
        this.drink = drink;
    }


    /// <summary>
    /// Remove this order. Destroy() is responsible for signaling the associated customer
    /// regarding the outcome of their drink.
    /// </summary>
    public void destroy(bool drinkCompleted)
    {
        customer.orderCallback(true);
        Destroy(this.gameObject);
    }
}
