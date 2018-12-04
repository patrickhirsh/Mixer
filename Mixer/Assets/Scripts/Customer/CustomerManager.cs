using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


/// <summary>
/// Customer Manager is responsible for spawning new customers. This class utilizes
/// DifficultyManager to produce a nice difficulty curve during spawning.
/// </summary>
public class CustomerManager : MonoBehaviour
{
    public static bool debugMode;

    // minimum and maximum time (in ms) between customer spawns during a spawn wave
    // used to create natural-looking clusters upon spawning.
    private static int SPAWN_WAVE_MAX_OFFSET = 2000;            
    private static int SPAWN_WAVE_MIN_OFFSET = 700;             

    private static float spawnTimer = 3f;                       // the time remaining before another customer should be spawned
    private static GameObject customersParent;                  // parent object in which all customers should be instantiated under                                          
    private static List<GameObject> customerPrefabs;            // array of all customer prefabs in Assets/Prefabs/Customers
    private static System.Random rnd;


    void Start()
    {
        rnd = new System.Random();
        customersParent = GameObject.Find("customers");
        populateCustomerPrefabs();        
    }


	void Update ()
    {
        // handle customer spawning
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0) { StartCoroutine(spawnCustomerWave()); spawnTimer = DifficultyManager.getNextSpawnTimer(); }
    }


    #region INTERNAL FUNCTIONS

    /// <summary>
    /// spawns a customer at a random spawn location
    /// </summary>
    private static void spawnCustomer()
    {
        Instantiate(customerPrefabs[UnityEngine.Random.Range(0, customerPrefabs.Count)], customersParent.transform);
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
            yield return new WaitForSeconds((float)rnd.Next(SPAWN_WAVE_MIN_OFFSET, SPAWN_WAVE_MAX_OFFSET) / 1000f);
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
            yield return new WaitForSeconds((float)rnd.Next(SPAWN_WAVE_MIN_OFFSET, SPAWN_WAVE_MAX_OFFSET) / 1000f);
        }
    }


    /// <summary>
    /// Pulls all customer prefabs from Assets/Prefabs/Customers for customer spawning.
    /// Should be called once before attempting to spawn any customers.
    /// </summary>
    private static void populateCustomerPrefabs()
    {
        customerPrefabs = new List<GameObject>(Resources.LoadAll("Prefabs/Customers", typeof(GameObject)).Cast<GameObject>());
    }

    #endregion
}
