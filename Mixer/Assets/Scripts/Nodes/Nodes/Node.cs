using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents a traversable position for customers during pathing.
/// </summary>
public class Node : MonoBehaviour
{
    public List<Node> outboundConnections;          // list of all nodes that can be traversed to from this node
    public float distance;                          // tentative distance from the origin node through the shortest path
    public Node prevNode;                           // previous node in the shortest path chain to this node
}
