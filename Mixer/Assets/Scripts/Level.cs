using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    // STATIC
    public static List<Level> levels;

    // INSTANCE
    public int numBartenderPositions { get; private set; }
    public List<Vector2> bartenderPositions { get; private set; }


    #region STATIC

    public static void Initialize()
    {
        levels = new List<Level>();

        // level 0
        int numPos1 = 3;
        List<Vector2> positions = new List<Vector2>();
        positions.Add(new Vector2(1, 0));
        positions.Add(new Vector2(1, 1));
        positions.Add(new Vector2(1, 2));
        levels.Add(new Level(numPos1, positions));
    }

    #endregion


    #region INSTANCE

    public Level(int numBartenderPositions, List<Vector2> bartenderPositions)
    {
        this.numBartenderPositions = numBartenderPositions;
        this.bartenderPositions = bartenderPositions;
    }

    #endregion
}
