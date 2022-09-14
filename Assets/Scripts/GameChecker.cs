using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChecker : MonoBehaviour
{
    public bool IsStart;
    public bool IsEnd;
    public GameManager gameManager;

    public void Start()
    {
        //Checks for stupid people behavior
        if (IsStart && IsEnd)
        {
            Debug.LogError("You cant have a start and a end on the same object what did you think would happen");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsEnd)
        {
            gameManager.FinishGame();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (IsStart)
        {
            gameManager.StartGame();
        }
    }


}
