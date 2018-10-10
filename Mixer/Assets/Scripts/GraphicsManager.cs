using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    public static GameObject bartender;

    private void Start()
    {
        bartender = GameObject.Find("Bartender");
    }


    // Update is called once per frame
    void Update ()
    {
		
	}


    // updates the bartender's position on-screen based on Bartender.position
    public static void updateBartenderPosition()
    {
        bartender.transform.position = Level.levels[GameManager.currentLevel].bartenderPositions[Bartender.position];
    }
}
