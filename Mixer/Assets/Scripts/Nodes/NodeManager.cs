using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Node Manager keeps track of all the path nodes in the scene.
/// This class provides functionality for getting nodes of certain types.
/// </summary>
public class NodeManager : MonoBehaviour
{
    public static bool debugMode;

    private static System.Random rnd;
    private static List<OrderNode> orderNodes;              // list of all orderNodes
    private static List<SpawnNode> spawnNodes_spawn;        // list of all spawnNodes of type "spawn"
    private static List<SpawnNode> spawnNodes_despawn;      // list of all spawnNodes of type "despawn"

    void Start ()
    {
        rnd = new System.Random();
        orderNodes = new List<OrderNode>();
        spawnNodes_spawn = new List<SpawnNode>();
        spawnNodes_despawn = new List<SpawnNode>();
        populateNodesLists();
    }


    #region EXTERNAL FUNCTIONS

    /// <summary>
    /// returns a random spawnNode from the list of valid spawn points "spawnNodes_spawn"
    /// </summary>
    public static SpawnNode getRandomSpawnNode()
    {
        return spawnNodes_spawn[rnd.Next(0, spawnNodes_spawn.Count)];
    }


    /// <summary>
    /// returns a random spawnNode from the list of valid despawn points "spawnNodes_despawn"
    /// </summary>
    public static SpawnNode getRandomDespawnNode()
    {
        return spawnNodes_despawn[rnd.Next(0, spawnNodes_despawn.Count)];
    }

    #endregion


    #region INTERNAL FUNCTIONS

    /// <summary>
    /// Populates lists of nodes for each node type. Should be called in Start()
    /// Only nodes that are children of the "nodes" object will be observed
    /// </summary>
    private static void populateNodesLists()
    {
        orderNodes.Clear();
        spawnNodes_spawn.Clear();
        spawnNodes_despawn.Clear();

        // populate lists of all different node types (under "nodes" object)
        GameObject nodesParent = GameObject.Find("nodes");
        for (int i = 0; i < nodesParent.transform.childCount; i++)
        {

            // ORDER NODES
            if (nodesParent.transform.GetChild(i).GetComponent<OrderNode>() != null)
            {
                if (!nodesParent.transform.GetChild(i).GetComponent<OrderNode>().isLinked())
                {
                    if (debugMode) { Debug.LogWarning("Detected an OrderNode without an associated bartenderPosition"); }
                }
                else { orderNodes.Add(nodesParent.transform.GetChild(i).GetComponent<OrderNode>()); }
            }

            // SPAWN NODES
            if (nodesParent.transform.GetChild(i).GetComponent<SpawnNode>() != null)
            {
                if (nodesParent.transform.GetChild(i).GetComponent<SpawnNode>().spawnType == SpawnNode.SpawnType.spawn)
                    spawnNodes_spawn.Add(nodesParent.transform.GetChild(i).GetComponent<SpawnNode>());
                else
                    spawnNodes_despawn.Add(nodesParent.transform.GetChild(i).GetComponent<SpawnNode>());
            }
        }
    }

    #endregion
}
