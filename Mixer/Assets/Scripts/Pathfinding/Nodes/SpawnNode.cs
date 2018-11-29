using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents a traversable position for customers during pathing.
/// Customers can spawn and despawn from this node.
/// </summary>
public class SpawnNode : Node
{
    
    public enum SpawnType { spawn, despawn }
    public SpawnType spawnType;                                     // indicates whether this is a spawn or despawn location
    public List<OrderNode> validOrderNodes;                         // indicates which OrderNodes should be considered when spawning from this point
    private Dictionary<OrderNode, float> distanceToOrderNodes;      // stores the distance to all orderNodes from this spawn point

    void Start()
    {
        // populate distanceToOrderNodes
        distanceToOrderNodes = new Dictionary<OrderNode, float>();
        GameObject nodesParent = GameObject.Find("nodes");
        for (int i = 0; i < nodesParent.transform.childCount; i++)
            if (nodesParent.transform.GetChild(i).GetComponent<OrderNode>() != null)
                distanceToOrderNodes.Add(nodesParent.transform.GetChild(i).GetComponent<OrderNode>(),
                    Vector2.Distance(this.transform.position, nodesParent.transform.GetChild(i).transform.position));
    }


    /// <summary>
    /// Gets the closest open OrderNode from validOrderNodes. 
    /// If non are open, selects a random valid OrderNode.
    /// </summary>
    public OrderNode getBestOrderNode()
    {
        OrderNode selected = null;
        foreach (OrderNode orderNode in validOrderNodes)
        {
            // is the orderNode open?
            if (orderNode.bartenderPosition.transform.childCount == 0)
            {
                // is the orderNode closer than selected?
                if (selected == null)
                    selected = orderNode;
                else if (distanceToOrderNodes[orderNode] < distanceToOrderNodes[selected])
                    selected = orderNode;
            }
        }

        // couldn't find an open OrderNode
        if (selected == null)
            selected = validOrderNodes[UnityEngine.Random.Range(0, validOrderNodes.Count)];

        return selected;
    }
}
