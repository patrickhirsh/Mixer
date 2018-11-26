using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CustomerTask
{
    FindingOrderNode,
    GettingDrink,
    WaitingForDrink,
    Leaving,
    Despawn
}


public class Customer : MonoBehaviour
{
    public CustomerTask currentTask { get; private set; }
    public Node currentNode;

    Order order;
    OrderNode orderNode;
    float walkSpeed;

	// Use this for initialization
	void Start ()
    {
        currentTask = CustomerTask.FindingOrderNode;
        walkSpeed = CustomerManager.AVG_WALK_SPEED + Random.Range(-CustomerManager.AVG_WALK_SPEED_VARIANCE, CustomerManager.AVG_WALK_SPEED_VARIANCE);
        currentNode = CustomerManager.getRandomSpawnNode();
	}



    /// <summary>
    /// Coroutine that moves this customer along a given path
    /// </summary>
    /// <returns></returns>
    private IEnumerator travelPath(List<Node> path)
    {
        foreach (Node node in path)
        {
            while (Vector3.Distance(node.transform.position, this.transform.position) > .2f)
            {
                float step = walkSpeed * Time.deltaTime;
                Vector3.MoveTowards(this.transform.position, node.transform.position, step);
                yield return null;
            }
        }
        taskTransition();
    }


    /// <summary>
    /// Calling this method implies that the currentTask has been completed.
    /// taskTransition() takes proper action upon completion of any CustomerTask, then switches to the next task. 
    /// </summary>
    private void taskTransition()
    {
        switch(this.currentTask)
        {

            /* 
             * Selects a random OrderNode to order from and paths the customer to this location 
             * from their current node. Upon completion, transitions to "GettingDrink".
            */
            case CustomerTask.FindingOrderNode:

                // determine the orderNode the customer should order from
                orderNode = CustomerManager.getRandomOrderNode();
                List<Node> path;

                // attempt to find a path to this node (and start pathing)
                if (PathfindingManager.findPath(currentNode, orderNode, out path))
                    StartCoroutine(travelPath(path));
                else { Debug.LogError("findPath() couldn't determine a path for a customer."); }

                // mark the new state we're in
                currentTask = CustomerTask.GettingDrink;
                break;


            /*
             * Assumes the customer is currently at an OrderNode. Orders a random drink
             * at this BartenderPosition and transitions to "WaitingForDrink".
            */
            case CustomerTask.GettingDrink:

                currentNode = currentNode as OrderNode;
                if (currentNode != null)
                    order = OrderManager.newOrder(this, Drink.getRandomDrink(0), ((OrderNode)currentNode).bartenderPosition);
                else { Debug.LogError("Customer switched to a GettingDrink state on a non-OrderNode"); }

                currentTask = CustomerTask.WaitingForDrink;
                break;


            /*
             * Assumes the customer has already ordered a drink.
             * This case should switch on a customer callback, indicating
             * that a drink order was completed (successfully OR on a timeout)
            */
            case CustomerTask.WaitingForDrink:

                // reset the customer position (from wherever they were standing to ordre a drink)
                this.transform.position = this.orderNode.transform.position;
                currentTask = CustomerTask.Leaving;
                break;

            
            /*
             * A transition on this task causes the customer to locate a random 
             * Spawn node (to despawn from) and paths this customer to that location.
            */
            case CustomerTask.Leaving:

                // ensure that the customer is located at their currentNode before pathing
                this.transform.position = this.currentNode.transform.position;

                SpawnNode destination = CustomerManager.getRandomDespawnNode();
                List<Node> exitPath;

                // attempt to find a path to this node (and start pathing)
                if (PathfindingManager.findPath(this.currentNode, destination, out exitPath))
                    StartCoroutine(travelPath(exitPath));
                else { Debug.LogError("findPath() couldn't determine a path for a customer."); }

                // upon the taskTransition() call within travelPath(), switch to Despawn
                currentTask = CustomerTask.Despawn;
                break;


            /*
             * A transition onto this task immediately destroys this object
            */
            case CustomerTask.Despawn:
                // TODO: Figure out how to destroy this gameObject here...
                this.Destroy();
                break;
        }
    }


    /// <summary>
    /// Should be called by the order associated with this customer upon order completion
    /// (or failure). 
    /// </summary>
    public void orderCallback(bool success)
    {
        // Do something different if the order times out, and is not successful..
        taskTransition();
    }
}
