using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public GameManager gameManager;
    public bool isQuit;
    public bool isSettings;
    public bool isBackToGame;
    public bool isBackToMenu;
    public bool isRestart;


    public void ButtonPressed ()
    {
        if (isQuit)
        {
            gameManager.Quit();
        }
        if (isSettings)
        {
            gameManager.Settings();
        }
        if (isBackToGame)
        {
            gameManager.BackToGame();
        }
        if (isBackToMenu)
        {
            gameManager.BackToMenu();
        }
        if (isRestart)
        {
            gameManager.Reset();
        }
    }

}
