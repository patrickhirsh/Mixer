using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    // STATIC
    public static List<Level> levels;

    // INSTANCE
    public int numBartenderPositions { get; private set; }
    public List<Vector2> bartenderPositions;
    public List<Vector2> alleyPositions;

    #region STATIC

    public static void Initialize()
    {
        levels = new List<Level>();

        // level 0
        int numPos1 = 3;
        List<Vector2> bartenderPositions1 = new List<Vector2>();
        bartenderPositions1.Add(new Vector2(5, -1));
        bartenderPositions1.Add(new Vector2(5, 0));
        bartenderPositions1.Add(new Vector2(5, 1));
        List<Vector2> alleyPositions1 = new List<Vector2>();
        alleyPositions1.Add(new Vector2(4, -1));
        alleyPositions1.Add(new Vector2(4, 0));
        alleyPositions1.Add(new Vector2(4, 1));
        levels.Add(new Level(numPos1, bartenderPositions1, alleyPositions1));
    }

    #endregion


    #region INSTANCE

    public Level(int numBartenderPositions, List<Vector2> bartenderPositions, List<Vector2> alleyPositions)
    {
        this.numBartenderPositions = numBartenderPositions;
        this.bartenderPositions = bartenderPositions;
        this.alleyPositions = alleyPositions;
    }

    #endregion
}
