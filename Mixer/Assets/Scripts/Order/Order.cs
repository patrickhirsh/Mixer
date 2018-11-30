using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An instance of Order contains all information pertaining to a customer's 
/// drink order including the drink type and time of order. Order is then
/// responsible for keeping track of the time limit on an order and notifying
/// the customer when the order is destroyed (on either a successful submission,
/// or a removal due to running out of time). Order also keeps track of its own
/// fulfillment status, signaling the proper classes on a successfully completed
/// order.
/// </summary>
public class Order : MonoBehaviour
{
    // starting time limit assigned to all new orders
    private static readonly float ORDER_TIME_LIMIT = 30f;   

    public Customer customer { get; private set; }      // the customer who ordered this drink
    public Drink drink { get; private set; }            // the drink associated with this order  
    public float timeLeft { get; private set; }         // time remaining for the player to complete the order

    // order fulfillment progress, containing only successful submissions
    private List<DrinkComponent> orderProgress;             


    void Start()
    {
        orderProgress = new List<DrinkComponent>();
        timeLeft = ORDER_TIME_LIMIT;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;       
        if (timeLeft <= 0) { GameManager.orderMiss(); destroy(false); }
    }


    #region EXTERNAL METHODS

    /// <summary>
    /// Initializes the data associated with this order. Should ALWAYS be called after instantiation
    /// </summary>
    public void Initialize(Customer customer, Drink drink)
    {
        this.customer = customer;
        this.drink = drink;
    }


    /// <summary>
    /// given a KeyCode, checks if this submitted DrinkComponent matches the next component in
    /// this order's drink recipe. If valid, the component is accepted. Otherwise, the component is
    /// rejected and the orderProgress is cleared for this order.
    /// </summary>
    public bool submitDrinkComponent(KeyCode key)
    {
        // add this new component to orderProgress
        orderProgress.Add(DrinkComponent.generateExternalDrinkComponent(key, DrinkComponent.categoryLabels[Bartender.menuState]));

        // if the submitted component doesn't have the same key input OR doesn't have the same category as the recipe...
        if (orderProgress[orderProgress.Count - 1].key.ToString().ToUpper() != this.drink.components[orderProgress.Count - 1].key.ToString().ToUpper() ||   // same key?
            (orderProgress[orderProgress.Count - 1].category.ToUpper() != this.drink.components[orderProgress.Count - 1].category.ToUpper()))               // same category?
        {
            orderProgress.Clear();
            if (OrderManager.debugMode) { Debug.Log("Invalid drink component submitted. Clearing drink progress"); }
            return false;
        }

        // otherwise, the component was valid
        // check to see if this was the last component (completing the drink)
        if (orderProgress.Count == this.drink.components.Count)
        {
            // drink completed successfully!
            GameManager.awardPoints();
            orderProgress.Clear();
            destroy(true);
            if (OrderManager.debugMode) { Debug.Log("Successfully completed a drink!"); }
        }

        return true;
    }


    /// <summary>
    /// Returns the number of components completed for this order
    /// </summary>
    public int getOrderProgressCount()
    {
        return orderProgress.Count;
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

    #endregion
}
