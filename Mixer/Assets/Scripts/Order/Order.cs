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

    // distance above the customer sprite in unity units that the order notification icon will be rendered
    private static float ORDER_ICON_Y_OFFSET = 1.7f;

    public Customer customer { get; private set; }                      // the customer who ordered this drink
    public Drink drink { get; private set; }                            // the drink associated with this order  
    public BartenderPosition bartenderPosition { get; private set; }    // the BartenderPosition this Order was placed at
    public float timeLeft { get; private set; }                         // time remaining for the player to complete the order

    private SpriteRenderer orderNotification;                           // the order notification appearing above "customer" when they are next in line
    private SpriteRenderer orderNotificationHighlight;                  // the highlight on the order notification indicating the time remaining on the order
    private Vector2 orderNotificationSize;                              // the order notification's width and height

    // order fulfillment progress, containing only successful submissions
    private List<DrinkComponent> orderProgress;             


    void Start()
    {
        orderProgress = new List<DrinkComponent>();
        timeLeft = ORDER_TIME_LIMIT;
        generateOrderNotification();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        updateOrderNotification();
        if (timeLeft <= 0) { GameManager.orderMiss(); destroy(false); }
    }


    #region INTERNAL METHODS

    /// <summary>
    /// Generates the orderNotification icon associated with this order's drink (along with its 
    /// time remaining highlight indicator). This is part of the initialization process and 
    /// should be called in Awake().
    /// </summary>
    private void generateOrderNotification()
    {
        // get the proper sprite for the drink tier
        switch (drink.drinkTier)
        {
            case 0:
                orderNotification = Instantiate(OrderManager.orderNotificationT0Prefab, this.gameObject.transform).GetComponent<SpriteRenderer>();
                break;

            case 1:
                orderNotification = Instantiate(OrderManager.orderNotificationT1Prefab, this.gameObject.transform).GetComponent<SpriteRenderer>();
                break;

            case 2:
                orderNotification = Instantiate(OrderManager.orderNotificationT2Prefab, this.gameObject.transform).GetComponent<SpriteRenderer>();
                break;

            case 3:
                orderNotification = Instantiate(OrderManager.orderNotificationT3Prefab, this.gameObject.transform).GetComponent<SpriteRenderer>();
                break;
        }

        // position the order notification & highlight above the customer
        orderNotification.transform.position = new Vector3(customer.transform.position.x, customer.transform.position.y + ORDER_ICON_Y_OFFSET, customer.transform.position.z);
        orderNotificationHighlight = Instantiate(OrderManager.orderNotificationHighlightPrefab, this.gameObject.transform).GetComponent<SpriteRenderer>();
        orderNotificationHighlight.transform.position = new Vector3(customer.transform.position.x, customer.transform.position.y, customer.transform.position.z);

        // apply the clipping mask to notificationHighlight
        SpriteMask mask = orderNotification.gameObject.AddComponent<SpriteMask>();
        mask.sprite = orderNotificationHighlight.sprite;
        orderNotificationHighlight.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        // store the icon height for use in updating the highlight position
        orderNotificationSize = orderNotification.GetComponent<SpriteRenderer>().bounds.size;

        // set both to disabled until Update() determines this order is the next in line
        orderNotification.enabled = false;
        orderNotificationHighlight.enabled = false;
        orderNotification.transform.GetComponent<SpriteMask>().enabled = false;
    }


    /// <summary>
    /// Updates the position of the orderNotificationHighlight based on the time remaining.
    /// </summary>
    private void updateOrderNotification()
    {
        // should we render the order notification this frame?
        checkRenderNotification();

        // update the highlight position
        float timeRemainingFactor = 1 - timeLeft / ORDER_TIME_LIMIT;
        float highlightOffset = -1f * (orderNotificationSize.y * timeRemainingFactor);
        orderNotificationHighlight.transform.position = 
            new Vector3(orderNotificationHighlight.transform.position.x, customer.transform.position.y + + ORDER_ICON_Y_OFFSET + highlightOffset, customer.transform.position.z - .01f);
    }


    /// <summary>
    /// Checks to see whether the order notification icon should be rendered during this frame update for
    /// this order and acts accordingly. Should be called every frame
    /// </summary>
    private void checkRenderNotification()
    {
        // never render notifications if the bartender is at this order's position
        if (Bartender.bartenderPositions.transform.GetChild(Bartender.position) == bartenderPosition.transform)
            renderNotification(false);

        else
        {
            // only render this order's notification if it's next in the queue at the current Bartender Position
            if (bartenderPosition.transform.GetChild(0) == this.transform) { renderNotification(true); }                
            else { renderNotification(false); }               
        }
    }


    /// <summary>
    /// Given a directive on whether or not to render the order notification, updates the current
    /// SpriteRenderer.endabled state (if need be)
    /// </summary>
    private void renderNotification(bool render)
    {
        if (render)
        {
            if (orderNotification.enabled == false || orderNotificationHighlight.enabled == false || 
                orderNotification.transform.GetComponent<SpriteMask>().enabled == false)
            {
                orderNotification.enabled = true;
                orderNotification.transform.GetComponent<SpriteMask>().enabled = true;
                orderNotificationHighlight.enabled = true;
            }
            
        }

        else
        {
            if (orderNotification.enabled == true || orderNotificationHighlight.enabled == true || 
                orderNotification.transform.GetComponent<SpriteMask>().enabled == true)
            {
                orderNotification.enabled = false;
                orderNotification.transform.GetComponent<SpriteMask>().enabled = false;
                orderNotificationHighlight.enabled = false;
            }                
        }
    }

    #endregion


    #region EXTERNAL METHODS

    /// <summary>
    /// Initializes the data associated with this order. Should ALWAYS be called after instantiation
    /// </summary>
    public void Initialize(Customer customer, Drink drink, BartenderPosition bartenderPosition)
    {
        this.customer = customer;
        this.drink = drink;
        this.bartenderPosition = bartenderPosition;
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
