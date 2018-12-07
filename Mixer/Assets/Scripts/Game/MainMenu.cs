using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    #region MAIN MENU BUTTON HANDLERS

    /// <summary>
    /// Called on "Play" button select
    /// </summary>
    public void handlePlay()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Called on "Quit" button select
    /// </summary>
    public void handleQuit()
    {
        Application.Quit();
    }

    #endregion
}
