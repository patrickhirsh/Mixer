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
    public Customer customer { get; private set; }      // the customer who ordered this drink
    public Drink drink { get; private set; }            // the drink associated with this order  
    public float timeLeft { get; private set; }         // time remaining for the player to complete the order

    void Start()
    {
        // use OrderManager's current timeLimit (adjusted as difficulty increases) as the timeLimit
        timeLeft = OrderManager.timeLimit;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        // TODO: check for if we're out of time and react accordingly.
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
