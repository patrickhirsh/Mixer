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
    private static float DIFFICULTY_CURVE = 1000f;

    // difficulty value that must be surpassed in order to spawn each tier
    private static float TIER_1_DIFFICULTY_THRESHOLD = .12f;
    private static float TIER_2_DIFFICULTY_THRESHOLD = .24f;
    private static float TIER_3_DIFFICULTY_THRESHOLD = .35f;

    // weight values for each tier (if applicable) when spawning.
    // these values need not sum to any particular value - they're just relative weights.
    // tiers with heavier weights are considered more strongly during tier selection.
    private static float TIER_0_SPAWN_WEIGHT = .4f;
    private static float TIER_1_SPAWN_WEIGHT = .2f;
    private static float TIER_2_SPAWN_WEIGHT = .2f;
    private static float TIER_3_SPAWN_WEIGHT = .1f;
    private static float TIER_0_1_WEIGHTSUM = TIER_0_SPAWN_WEIGHT + TIER_1_SPAWN_WEIGHT;
    private static float TIER_0_1_2_WEIGHTSUM = TIER_0_SPAWN_WEIGHT + TIER_1_SPAWN_WEIGHT + TIER_2_SPAWN_WEIGHT;
    private static float TIER_0_1_2_3_WEIGHTSUM = TIER_0_SPAWN_WEIGHT + TIER_1_SPAWN_WEIGHT + TIER_2_SPAWN_WEIGHT + TIER_3_SPAWN_WEIGHT;


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


    public static float getNextSpawnTimer()
    {
        return 0;
    }


    public static int getNextSpawnWaveSize()
    {
        return 0;
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
