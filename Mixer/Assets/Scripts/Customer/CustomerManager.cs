using System.Collections;
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
    public static float avgSpawnTimer { get; private set; }                 // the time limit between customer spawns
    public static float avgSpawnTimer_ReductionVal { get; private set; }    // the time in seconds that the avgSpawnTimer is reduced by on difficulty increase. (TODO: scale non-linearly)
    public static float nextSpawnTimer { get; private set; }                // the time remaining before another customer should be spawned


    /// <summary>
    /// initialize static structures in CustomerManager. Should be called once per level load
    /// </summary>
    public static void Initialize()
    {
        avgSpawnTimer = STARTING_SPAWN_TIMER;
        avgSpawnTimer_ReductionVal = STARTING_SPAWN_TIMER_REDUCTION_VAL;
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

    /*
    // since we're casting to int (and thus rounding down), we need the full range of top position -> top position .999... to get an even probability
    int position = (int)Random.Range(0, Level.levels[GameManager.currentLevel].numBartenderPositions - .000001f);
    */
}
