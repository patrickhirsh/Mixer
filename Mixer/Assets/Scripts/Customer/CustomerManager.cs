using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CustomerManager : MonoBehaviour
{
    public static bool debugMode;

    // constants for customer behavior
    private static float AVG_WALK_SPEED = 1f;
    private static float AVG_WALK_SPEED_VARIANCE = 0f;
    private static float WAITING_POSITION_X_VARIANCE = 0f;               // variance in the X position of standing positions chosen for customers waiting for drinks
    private static float WAITING_POSITION_Y_GAP = .25f;                  // distance in the Y direction between each customer waiting for a drink at any given OrderNode
    private static int SPAWN_WAVE_MAX_OFFSET = 1000;                     // maximum time in ms between customer spawns during a spawn wave
    private static int SPAWN_WAVE_MIN_OFFSET = 500;                      // minimum time in ms between customer spawns during a spawn wave

    private static System.Random rnd;
    private static float spawnTimer;                        // the time remaining before another customer should be spawned
    private static GameObject CustomersParent;              // parent object in which all customers should be instantiated under                                          
    private static List<GameObject> customerPrefabs;        // array of all customer prefabs in Assets/Prefabs/Customers
    private static List<OrderNode> orderNodes;              // list of all orderNodes
    private static List<SpawnNode> spawnNodes_spawn;        // list of all spawnNodes of type "spawn"
    private static List<SpawnNode> spawnNodes_despawn;      // list of all spawnNodes of type "despawn"


    void Start()
    {
        orderNodes = new List<OrderNode>();
        spawnNodes_spawn = new List<SpawnNode>();
        spawnNodes_despawn = new List<SpawnNode>();

        rnd = new System.Random();
        CustomersParent = GameObject.Find("Customers");
        spawnTimer = 3f;
        populateCustomerPrefabs();
        populateNodesLists();
    }


	void Update ()
    {
        spawnTimer -= Time.deltaTime;

        // when timer reaches 0, spawn a new customer and reset the timer
        if (spawnTimer <= 0)
        {
            StartCoroutine(spawnCustomerWave());
            spawnTimer = DifficultyManager.getNextSpawnTimer();
        }
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


    /// <summary>
    /// based on the number of people currently waiting at "orderNode", returns a position
    /// that the customer calling this method should stand while they wait for their drink.
    /// Should be called after the customer's drink has been ordered
    /// </summary>
    public static Vector3 getRandomWaitingPosition(OrderNode orderNode)
    {
        float x = orderNode.transform.position.x + UnityEngine.Random.Range(WAITING_POSITION_X_VARIANCE * -1f, WAITING_POSITION_X_VARIANCE);
        float y = orderNode.transform.position.y - ((orderNode.bartenderPosition.transform.childCount - 1) * WAITING_POSITION_Y_GAP);
        float z = GraphicsManager.calculateZValue(y);

        // first customer doesn't have any X offset
        if (orderNode.bartenderPosition.transform.childCount == 1)
            x = orderNode.transform.position.x;

        return new Vector3(x, y, z);
    }


    public static float getRandomWalkSpeed()
    {
        return AVG_WALK_SPEED + UnityEngine.Random.Range(AVG_WALK_SPEED_VARIANCE * -1, AVG_WALK_SPEED_VARIANCE);
    }

    #endregion


    #region INTERNAL FUNCTIONS


    /// <summary>
    /// spawns a customer at a random spawn location
    /// </summary>
    private static void spawnCustomer()
    {
        // TODO: add customer sprite variation
        Instantiate(customerPrefabs[0], CustomersParent.transform);
    }


    /// <summary>
    /// coroutine that spawns a wave of customers at random spawn locations
    /// </summary>
    private static IEnumerator spawnCustomerWave()
    {
        int count = DifficultyManager.getNextSpawnWaveSize();

        for (int i = 0; i < count; i++)
        {
            spawnCustomer();
            yield return new WaitForSeconds(UnityEngine.Random.Range(SPAWN_WAVE_MIN_OFFSET, SPAWN_WAVE_MAX_OFFSET));
        }           
    }


    /// <summary>
    /// coroutine that spawns a wave of customers at random spawn locations
    /// </summary>
    private static IEnumerator spawnCustomerWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            spawnCustomer();
            yield return new WaitForSeconds(rnd.Next(SPAWN_WAVE_MIN_OFFSET, SPAWN_WAVE_MAX_OFFSET)/1000);
        }
    }


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


    /// <summary>
    /// Pulls all customer prefabs from Assets/Prefabs/Customers for customer spawning.
    /// Should be called once before attempting to spawn any customers. Assumes customer prefabs
    /// follow the name convention "customer0, customer1, customer2, etc.."
    /// </summary>
    private static void populateCustomerPrefabs()
    {
        customerPrefabs = new List<GameObject>(Resources.LoadAll("Prefabs/Customers", typeof(GameObject)).Cast<GameObject>());
    }

    #endregion
}
