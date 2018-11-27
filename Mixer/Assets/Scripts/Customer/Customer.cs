using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CustomerTask
{
    FindingOrderNode,
    GettingDrink,
    Leaving,
    Despawn
}


public class Customer : MonoBehaviour
{
    public CustomerTask currentTask { get; private set; }
    public Node currentNode;

    private Order order;
    private OrderNode orderNode;
    private SpawnNode spawnNode;
    private float walkSpeed;


	// Use this for initialization
	void Start ()
    {        
        walkSpeed = CustomerManager.AVG_WALK_SPEED + Random.Range(CustomerManager.AVG_WALK_SPEED_VARIANCE * -1, CustomerManager.AVG_WALK_SPEED_VARIANCE);
        spawnNode = CustomerManager.getRandomSpawnNode();
        currentNode = spawnNode;
        this.transform.position = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y, currentNode.transform.position.z);
        currentTask = CustomerTask.FindingOrderNode;
        executeTask();
	}


    /// <summary>
    /// Should be called by the order associated with this customer upon order completion
    /// (or failure). 
    /// </summary>
    public void orderCallback(bool success)
    {
        // Do something different if the order times out, and is not successful..
        order = null;
        currentTask = CustomerTask.Leaving;
        executeTask();
    }


    /// <summary>
    /// Executes this customer's currentTask
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
                orderNode = CustomerManager.getRandomOrderNode(spawnNode.validOrderNodes);
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
                    order = OrderManager.newOrder(this, Drink.getRandomDrink(0), ((OrderNode)currentNode).bartenderPosition);
                else { Debug.LogError("Customer switched to a GettingDrink state on a non-OrderNode"); }

                // determine where the customer should stand while they wait
                this.transform.position = CustomerManager.getRandomWaitingPosition(orderNode);
                break;

           
            /*
             * Tells the customer to locate a random spawn node (to despawn from) 
             * and paths this customer to that location.
            */
            case CustomerTask.Leaving:

                // move customer from their waiting position back to their current node
                this.transform.position = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y, GraphicsManager.calculateZValue(currentNode.transform.position.y));

                // determine the despawn node this customer should leave from
                SpawnNode destination = CustomerManager.getRandomDespawnNode();
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
}
