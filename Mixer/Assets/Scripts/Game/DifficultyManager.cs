using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utilizes the simple formula:            y = x / ( x + DIFFICULTY_CURVE ) 
/// to determine the current difficulty
/// 
/// y = current difficulty between 0 and 1 (infinitely approaching 1)
/// x = Time.time
/// a = curve modifier. Larger values of "a" produce a more gradual difficulty curve
/// 
/// This difficulty value is then used to determine various spawning and
/// drink tier selection, allowing for a nice difficulty curve over time.
/// </summary>
public static class DifficultyManager
{
    private static float DIFFICULTY_CURVE = 700f;

    // difficulty value that must be surpassed in order to spawn each tier
    //private static float TIER_1_DIFFICULTY_THRESHOLD = .12f;
    //private static float TIER_2_DIFFICULTY_THRESHOLD = .24f;
    //private static float TIER_3_DIFFICULTY_THRESHOLD = .35f;
    //private static float TIER_1_DIFFICULTY_THRESHOLD = 0f;
    //private static float TIER_2_DIFFICULTY_THRESHOLD = 0f;
    //private static float TIER_3_DIFFICULTY_THRESHOLD = 0f;
    private static float TIER_1_DIFFICULTY_THRESHOLD = .10f;
    private static float TIER_2_DIFFICULTY_THRESHOLD = .18f;
    private static float TIER_3_DIFFICULTY_THRESHOLD = .22f;

    // weight values for each tier (if applicable) when spawning.
    // these values need not sum to any particular value - they're just relative weights.
    // tiers with heavier weights are considered more strongly during tier selection.
    private static float TIER_0_SPAWN_WEIGHT = .5f;
    private static float TIER_1_SPAWN_WEIGHT = .1f;
    private static float TIER_2_SPAWN_WEIGHT = .1f;
    private static float TIER_3_SPAWN_WEIGHT = .05f;
    //private static float TIER_0_SPAWN_WEIGHT = 1f;
    //private static float TIER_1_SPAWN_WEIGHT = 1f;
    //private static float TIER_2_SPAWN_WEIGHT = 1f;
    //private static float TIER_3_SPAWN_WEIGHT = 1f;
    private static float TIER_0_1_WEIGHTSUM = TIER_0_SPAWN_WEIGHT + TIER_1_SPAWN_WEIGHT;
    private static float TIER_0_1_2_WEIGHTSUM = TIER_0_SPAWN_WEIGHT + TIER_1_SPAWN_WEIGHT + TIER_2_SPAWN_WEIGHT;
    private static float TIER_0_1_2_3_WEIGHTSUM = TIER_0_SPAWN_WEIGHT + TIER_1_SPAWN_WEIGHT + TIER_2_SPAWN_WEIGHT + TIER_3_SPAWN_WEIGHT;

    // spawn timer constants. SPAWN_TIMER_SCALEVAL * (1 - difficulty) + (deviation using variance) = next spawn timer
    private static float SPAWN_TIMER_VARIANCE = 1f;
    private static float SPAWN_TIMER_SCALEVAL = 20f;

    // spawn wave size constants. SPAWN_WAVE_SIZE_SCALEVAL * difficulty + (deviation using variance) =  next spawn wave size
    private static int SPAWN_WAVE_SIZE_VARIANCE = 2;
    private static float SPAWN_WAVE_SIZE_SCALEVAL = 20f;


    /// <summary>
    /// Returns a random drink tier based on the valid tiers for the current difficulty 
    /// and the values of each tier's spawn weight.
    /// </summary>
    /// <returns></returns>
    public static int getDrinkTier()
    {
        float difficulty = getCurrentDifficulty();
        int validTiers = 0;

        // determine all valid tiers to consider
        if (difficulty > TIER_1_DIFFICULTY_THRESHOLD)
            validTiers++;
        if (difficulty > TIER_2_DIFFICULTY_THRESHOLD)
            validTiers++;
        if (difficulty > TIER_3_DIFFICULTY_THRESHOLD)
            validTiers++;

        // consider all 4 tiers
        if (validTiers == 3)
        {
            float weightedValue = UnityEngine.Random.Range(0, TIER_0_1_2_3_WEIGHTSUM);

            if (weightedValue >= 0 && weightedValue <= TIER_0_SPAWN_WEIGHT)
                return 0;
            if (weightedValue >= TIER_0_SPAWN_WEIGHT && weightedValue <= TIER_0_1_WEIGHTSUM)
                return 1;
            if (weightedValue >= TIER_0_1_WEIGHTSUM && weightedValue <= TIER_0_1_2_WEIGHTSUM)
                return 2;
            if (weightedValue >= TIER_0_1_2_WEIGHTSUM && weightedValue <= TIER_0_1_2_3_WEIGHTSUM)
                return 3;

            return 0;
        }

        // consider the bottom 3 tiers
        else if (validTiers == 2)
        {
            float weightedValue = UnityEngine.Random.Range(0, TIER_0_1_2_WEIGHTSUM);

            if (weightedValue >= 0 && weightedValue <= TIER_0_SPAWN_WEIGHT)
                return 0;
            if (weightedValue >= TIER_0_SPAWN_WEIGHT && weightedValue <= TIER_0_1_WEIGHTSUM)
                return 1;
            if (weightedValue >= TIER_0_1_WEIGHTSUM && weightedValue <= TIER_0_1_2_WEIGHTSUM)
                return 2;

            return 0;
        }

        // consider the bottom 2 tiers
        else if (validTiers == 1)
        {
            float weightedValue = UnityEngine.Random.Range(0, TIER_0_1_WEIGHTSUM);

            if (weightedValue >= 0 && weightedValue <= TIER_0_SPAWN_WEIGHT)
                return 0;
            if (weightedValue >= TIER_0_SPAWN_WEIGHT && weightedValue <= TIER_0_1_WEIGHTSUM)
                return 1;

            return 0;
        }

        // only tier 0 is valid
        else
            return 0;
    }


    /// <summary>
    /// Returns a spawn timer value based on the current difficulty
    /// </summary>
    public static float getNextSpawnTimer()
    {
        float spawnTimer = (1 - getCurrentDifficulty()) * SPAWN_TIMER_SCALEVAL;
        float deviation = UnityEngine.Random.Range(SPAWN_TIMER_VARIANCE * -1, SPAWN_TIMER_VARIANCE);

        // make sure the deviation in spawnTimer doesn't create an invalid timeLimit
        if ((spawnTimer + deviation) > 0)
            spawnTimer += deviation;

        return spawnTimer;
    }


    public static int getNextSpawnWaveSize()
    {
        int spawnWaveSize = (int)(getCurrentDifficulty() * SPAWN_WAVE_SIZE_SCALEVAL);
        int deviation = UnityEngine.Random.Range(SPAWN_WAVE_SIZE_VARIANCE * 1, SPAWN_WAVE_SIZE_VARIANCE);
        spawnWaveSize += deviation;

        return spawnWaveSize;
    }


    /// <summary>
    /// Uses the formula described in the class summary above to determine the current
    /// difficulty level based on DIFFICULTY_CURVE and the current time.
    /// </summary>
    private static float getCurrentDifficulty()
    {
        return Time.time / (Time.time + DIFFICULTY_CURVE);
    }
}
