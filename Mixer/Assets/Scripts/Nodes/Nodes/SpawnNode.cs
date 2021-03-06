﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents a traversable position for customers during pathing.
/// Customers can spawn and despawn from this node. SpawnNode holds all
/// information about its valid OrderNodes (to path to) and contains logic for
/// selecting these nodes optimally during customer spawning.
/// </summary>
public class SpawnNode : Node
{
    private static float ORDERNODE_SELECTION_COOLDOWN = 4f;         // minimum time getBestOrderNode will wait before selecting the same OrderNode again (not applicable if all are full)

    public enum SpawnType { spawn, despawn }
    public SpawnType spawnType;                                     // indicates whether this is a spawn or despawn location (should be set in inspector)
    public SpawnNode sisterSpawnNode;                               // indicates the paired Spawn or Despawn node associated with this node (should be set in inspector)
    public List<OrderNode> validOrderNodes;                         // indicates which OrderNodes should be considered when spawning from this point
    private Dictionary<OrderNode, float> distanceToOrderNodes;      // stores the distance to all orderNodes from this spawn point
    private Dictionary<OrderNode, float> timeAtLastSelection;       // the Time.time when this orderNode was last selected in getBestOrderNode()


    void Start()
    {
        // populate distanceToOrderNodes
        distanceToOrderNodes = new Dictionary<OrderNode, float>();
        GameObject nodesParent = GameObject.Find("nodes");
        for (int i = 0; i < nodesParent.transform.childCount; i++)
            if (nodesParent.transform.GetChild(i).GetComponent<OrderNode>() != null)
                distanceToOrderNodes.Add(nodesParent.transform.GetChild(i).GetComponent<OrderNode>(),
                    Vector2.Distance(this.transform.position, nodesParent.transform.GetChild(i).transform.position));

        // populate timeAtLastSelection
        timeAtLastSelection = new Dictionary<OrderNode, float>();
        foreach (OrderNode orderNode in distanceToOrderNodes.Keys)
            timeAtLastSelection.Add(orderNode, ORDERNODE_SELECTION_COOLDOWN * -1);

        // validate inspector settings
        Debug.Assert(sisterSpawnNode != null);
    }


    /// <summary>
    /// Gets the closest open OrderNode from validOrderNodes. 
    /// If non are open, selects a random valid OrderNode.
    /// If an OrderNode was selected in the last 
    /// </summary>
    public OrderNode getBestOrderNode()
    {
        OrderNode selected = null;
        foreach (OrderNode orderNode in validOrderNodes)
        {
            // has orderNode been selected recently?
            if ((Time.time - timeAtLastSelection[orderNode]) > ORDERNODE_SELECTION_COOLDOWN)
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
        }

        // couldn't find an open OrderNode
        if (selected == null)
            selected = validOrderNodes[UnityEngine.Random.Range(0, validOrderNodes.Count)];
            

        // mark the chosen OrderNode's time of selection
        timeAtLastSelection[selected] = Time.time;

        return selected;
    }
}
