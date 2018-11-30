using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// indicates a customer's current task.
/// CustomerTask should NEVER have more than one flag set at any given time
/// </summary>
public enum CustomerTask
{
    FindingOrderNode,
    GettingDrink,
    Leaving,
    Despawn
}


/// <summary>
/// Customer controls an instance of a customer in the scene. This class acts as
/// a CustomerTask "state machine", executing a series of tasks that trigger new
/// task executions upon completion. Customer behavior is entirely self-regulated
/// within this class.
/// </summary>
public class Customer : MonoBehaviour
{
 
    // average walk speed + (deviation using variance) = walk speed
    private static float AVG_WALK_SPEED = 1f;                   
    private static float AVG_WALK_SPEED_VARIANCE = 0f;    
    
    // X variance represents the maximum absolute deviation selected for the X position while waiting for a drink
    // Y gap is the constant (applied negatively to y position) gap placed between customers waiting for a drink
    private static float WAITING_POSITION_X_VARIANCE = 0f;      
    private static float WAITING_POSITION_Y_GAP = .25f;         

    
    // this customer's current task (acting as a state in the state machine)
    public CustomerTask currentTask { get; private set; }

    private Node currentNode;           // this customer's current path node
    private Drink drink;                // the drink this customer will order. null after the customer's order is completed (or failed)
    private OrderNode orderNode;        // the OrderNode this customer ordered from if applicable. Otherwise, null
    private SpawnNode spawnNode;        // the node this customer spawned from
    private float walkSpeed;            // this customer's walk speed


	// Use this for initialization
	void Start ()
    {
        // determine the customer's walk speed and spawn position
        walkSpeed = AVG_WALK_SPEED + UnityEngine.Random.Range(AVG_WALK_SPEED_VARIANCE * -1, AVG_WALK_SPEED_VARIANCE);
        spawnNode = NodeManager.getRandomSpawnNode();
        currentNode = spawnNode;
        drink = Drink.getRandomDrink();

        // move the customer to the determined spawn position and set thier outline color
        this.transform.position = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y, currentNode.transform.position.z);
        GraphicsManager.updateCustomerOutlineColor(this, this.drink);

        // begin the CustomerTask "state machine"
        currentTask = CustomerTask.FindingOrderNode;
        executeTask();
	}


    #region EXTERNAL METHODS

    /// <summary>
    /// Should be called by the order associated with this customer upon order completion
    /// (or failure). 
    /// </summary>
    public void orderCallback(bool success)
    {
        drink = null;
        currentTask = CustomerTask.Leaving;
        executeTask();
    }

    #endregion


    #region INTERNAL METHODS

    /// <summary>
    /// Executes this customer's currentTask. This is the core of the customer state machine.
    /// Each possible CustomerTask will always either: set a new task and execute it, start a
    /// coroutine that will then set a new task and execute it (on completion), or despawn and
    /// destroy the customer.
    /// </summary>
    private void executeTask()
    {
        switch(this.currentTask)
        {

            /* 
             * Selects a random OrderNode to order from and paths the customer to this location 
             * from their current node. Upon completion, transitions to "GettingDrink".
            */
            case CustomerTask.FindingOrderNode:

                // determine the orderNode the customer should order from
                orderNode = spawnNode.getBestOrderNode();
                List<Node> path;

                // attempt to find a path to this node (and start pathing)
                if (PathfindingManager.findPath(currentNode, orderNode, out path))
                    StartCoroutine(travelPath(path, CustomerTask.GettingDrink));
                else { Debug.LogError("findPath() couldn't determine a path for a customer."); }
                break;


            /*
             * Assumes the customer is currently at an OrderNode. Orders a random drink
             * at this BartenderPosition and waits for orderCallback() to proceed.
            */
            case CustomerTask.GettingDrink:

                // order a drink
                currentNode = currentNode as OrderNode;
                if (currentNode != null)
                OrderManager.newOrder(this, this.drink, ((OrderNode)currentNode).bartenderPosition);
                else { Debug.LogError("Customer switched to a GettingDrink state on a non-OrderNode"); }

                // determine where the customer should stand while they wait
                this.transform.position = getRandomWaitingPosition();
                break;

           
            /*
             * Tells the customer to locate a random spawn node (to despawn from) 
             * and paths this customer to that location.
            */
            case CustomerTask.Leaving:

                // move customer from their waiting position back to their current node
                this.transform.position = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y, GraphicsManager.calculateZValue(currentNode.transform.position.y));

                // determine the despawn node this customer should leave from
                SpawnNode destination = spawnNode.sisterSpawnNode;
                List<Node> exitPath;

                // attempt to find a path to this node (and start pathing)
                if (PathfindingManager.findPath(this.currentNode, destination, out exitPath))
                    StartCoroutine(travelPath(exitPath, CustomerTask.Despawn));
                else { Debug.LogError("findPath() couldn't determine a path for a customer."); }
                break;


            /*
             * A transition onto this task immediately destroys this object
            */
            case CustomerTask.Despawn:
                Destroy(this.gameObject);
                break;
        }
    }


    /// <summary>
    /// Coroutine that moves this customer along a given path. Upon completion,
    /// executed "nextTask". Automatically adjusts the customer's Z paramater to keep
    /// them in the proper "Z" layer relative to their Y position. This keeps all
    /// objects in the scene layered properly and shows the customer "under" or "over"
    /// sprites depedning on their Y position.
    /// </summary>
    /// <returns></returns>
    private IEnumerator travelPath(List<Node> path, CustomerTask nextTask)
    {
        foreach (Node node in path)
        {
            // we don't use Vector3.Distance() here because the Z value of the customer need not match the Z of the node.
            while ((Mathf.Abs((node.transform.position.x - this.transform.position.x)) > .02f) ||
                (Mathf.Abs((node.transform.position.y - this.transform.position.y)) > .02f))
            {               
                float step = walkSpeed * Time.deltaTime;
                this.transform.position = Vector2.MoveTowards(this.transform.position, node.transform.position, step);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, GraphicsManager.calculateZValue(this.transform.position.y));
                yield return null;
            }
            currentNode = node;
        }
        currentTask = nextTask;
        executeTask();
    }


    /// <summary>
    /// based on the number of people currently waiting at "orderNode", returns a position
    /// that the customer should stand while they wait for their drink.
    /// Should be called after the customer's drink has been ordered
    /// </summary>
    private Vector3 getRandomWaitingPosition()
    {
        float x = orderNode.transform.position.x + UnityEngine.Random.Range(WAITING_POSITION_X_VARIANCE * -1f, WAITING_POSITION_X_VARIANCE);
        float y = orderNode.transform.position.y - ((orderNode.bartenderPosition.transform.childCount - 1) * WAITING_POSITION_Y_GAP);
        float z = GraphicsManager.calculateZValue(y);

        // first customer doesn't have any X offset
        if (orderNode.bartenderPosition.transform.childCount == 1)
            x = orderNode.transform.position.x;

        return new Vector3(x, y, z);
    }

    #endregion
}
