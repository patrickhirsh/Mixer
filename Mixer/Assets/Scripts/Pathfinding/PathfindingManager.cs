using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Compares two Node objects based on distance value
/// </summary>
public class NodeComparer : IComparer<Node>
{
    public int Compare(Node node1, Node node2)
    {
        if (node1.distance < node2.distance)
            return -1;
        else if (node1.distance > node2.distance)
            return 1;
        else if (node1.distance == node2.distance)
            return 0;

        else return 0;
    }
}


/// <summary>
/// Provides pathfinding algorithms to traverse fields of Nodes. Note that this class is not designed
/// to work asynchronously. This is simply due to the small number of Nodes in any given level 
/// (and the simplicity of design associated with a static implementation)
/// </summary>
public class PathfindingManager : MonoBehaviour
{
    public static bool debugMode;
    private static List<Node> nodes;    // all Nodes ( children of "pathnodes" object)

	// Use this for initialization
	void Start ()
    {
        // construct a list of all Nodes in the scene under the "nodes" GameObject
        GameObject nodesParents = GameObject.Find("nodes");
        for (int i = 0; i < nodesParents.transform.childCount; i++)
        {
            // if this fails, there is a child object of "nodes" that doesn't have the Node component
            Debug.Assert(nodesParents.transform.GetChild(i).GetComponent<Node>() != null);
            nodes.Add(nodesParents.transform.GetChild(i).GetComponent<Node>());
        }            
	}
	

    /// <summary>
    /// Given a "start" and "destination" Node, tries to find the shortest path between these
    /// Nodes (and places this path in "path"). Returns true if a path is found and false otherwise.
    /// If start == destination, returns true with an empty "path".
    /// </summary>
    /// <returns></returns>
    public static bool findPath(Node start, Node destination, out List<Node> path)
    {

        #region INPUT PROCESSING/VALIDATION

        path = new List<Node>();

        // The given connections must not be null
        if ((start == null) || (destination == null))
        {
            Debug.LogError("PathfindingManager.findPath() was given a null Node");
            return false;
        }

        // equivalent start/end returns an empty path
        if (start == destination) return true;

        #endregion


        #region SETUP

        NodeComparer nodeComparer = new NodeComparer();         // used by .Sort() to sort by Node.distance
        Node currentNode = start;                               // start with the start node
        List<Node> processed = new List<Node>();                // all "visited" nodes
        List<Node> frontier = new List<Node>();                 // all "unvisited" nodes

        // populate the frontier
        foreach (Node node in nodes)
        {
            // start node initial distance is 0
            if (node.transform.position == currentNode.transform.position)
                node.distance = 0;  
            
            // all other nodes have an initial distance of infinity
            else { node.distance = float.MaxValue; }

            node.prevNode = null;
            frontier.Add(node);
        }

        frontier.Sort(nodeComparer);

        #endregion


        #region CORE ALGORITHM

        // explore the frontier
        // these are the voyages of the Starship Enterprise...
        while (frontier.Count > 0)
        {
            // process the node with the next lowest tentative distance from start
            currentNode = frontier[0];
            frontier.RemoveAt(0);

            // the remaining nodes in the frontier are unreachable and we didn't find "destination". Return false
            if (currentNode.distance == float.MaxValue)
                return false;

            // if the destination has the shortest tentative distance from start (and is therefore being processed), we're done.
            if (currentNode == destination)
                break;

            foreach (Node node in currentNode.outboundConnections)
            {
                float distance = Vector3.Distance(currentNode.transform.position, node.transform.position) + currentNode.distance;
                if (node.distance > distance)
                {
                    node.distance = distance;
                    node.prevNode = currentNode;
                }                    
            }

            // re-sort the frontier after altering distance values
            frontier.Sort(nodeComparer);
        }

        #endregion


        #region PATH CONSTRUCTION

        // traverse backwards through the optimal path linked-list
        currentNode = destination;
        while (currentNode.prevNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.prevNode;
        }

        // the final node (which doesn't have a prevNode) should be the start node
        Debug.Assert(currentNode == start);
        path.Add(currentNode);
        path.Reverse();
        return true;

        #endregion
    }
}
