﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static bool debugMode;

    // constants for spawn interval algorithm (all in seconds)
    private static float STARTING_SPAWN_TIMER = 20f;                        // time between customer spawns (adjusted over time)
    private static float STARTING_SPAWN_TIMER_REDUCTION_VAL = .05f;         // value at which to reduce spawnTimer by each increaseDifficulty() call
    private static float SPAWN_TIMER_VARIANCE_UPPER = 1f;                   // deviation allowance for nextSpawnTimer when resetting the timer (upper bound)
    private static float SPAWN_TIMER_VARIANCE_LOWER = -1f;                  // deviation allowance for nextSpawnTimer when resetting the timer (lower bound)


    // variables for spawn interval algorithm
    private static float avgSpawnTimer;                 // the time limit between customer spawns
    private static float avgSpawnTimer_ReductionVal;    // the time in seconds that the avgSpawnTimer is reduced by on difficulty increase. (TODO: scale non-linearly)
    private static float nextSpawnTimer;                // the time remaining before another customer should be spawned

    private static List<OrderNode> orderNodes;          // list of all orderNodes
    private static List<SpawnNode> spawnNodes_spawn;    // list of all spawnNodes of type "spawn"
    private static List<SpawnNode> spawnNodes_despawn;  // list of all spawnNodes of type "despawn"


    /// <summary>
    /// initialize static structures in CustomerManager. Should be called once per level load
    /// </summary>
    public static void Initialize()
    {
        avgSpawnTimer = STARTING_SPAWN_TIMER;
        avgSpawnTimer_ReductionVal = STARTING_SPAWN_TIMER_REDUCTION_VAL;

        populateNodesLists();
    }

	// Update is called once per frame
	void Update ()
    {
        // TODO: Handle customer spawning

        // We'll need: 
        // Pathfinding
        // Node System
        // Nodes that are associated with a bartender position

        /*
        nextSpawnTimer -= Time.deltaTime;

        // spawn a new order and reset the timer (timer is at 0)
        if (nextSpawnTimer <= 0)
        {
            // reset nextOrderTimer
            float deviation = UnityEngine.Random.Range(SPAWN_TIMER_VARIANCE_LOWER, SPAWN_TIMER_VARIANCE_UPPER);

            // make sure the deviation in avgOrderTimer doesn't create an invalid timeLimit
            if ((avgSpawnTimer + deviation) <= 0)
                nextSpawnTimer = avgSpawnTimer;
            else { nextSpawnTimer = avgSpawnTimer + deviation; }
        }
        */
    }


    public static void increaseDifficulty()
    {
        // reduce the average amount of time between customer spawns
        if ((avgSpawnTimer - avgSpawnTimer_ReductionVal) > 0)
            avgSpawnTimer -= avgSpawnTimer_ReductionVal;
    }


    #region HELPER METHODS

    /// <summary>
    /// Populates lists of nodes for each node type. Should be called in Start()
    /// Only nodes that are children of the "nodes" object will be observed
    /// </summary>
    private static void populateNodesLists()
    {
        // populate lists of all different node types (under "nodes" object)
        GameObject nodesParent = GameObject.Find("nodes");
        for (int i = 0; i < nodesParent.transform.childCount; i++)
        {

            // ORDER NODES
            if (nodesParent.transform.GetChild(i).GetComponent<OrderNode>() != null)
            {
                if (!nodesParent.transform.GetChild(i).GetComponent<OrderNode>().isLinked())
                    if (debugMode) { Debug.LogWarning("Detected an OrderNode without an associated bartenderPosition"); }
                    else
                        orderNodes.Add(nodesParent.transform.GetChild(i).GetComponent<OrderNode>());
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

    /*
    // since we're casting to int (and thus rounding down), we need the full range of top position -> top position .999... to get an even probability
    int position = (int)Random.Range(0, Level.levels[GameManager.currentLevel].numBartenderPositions - .000001f);
    */
}
