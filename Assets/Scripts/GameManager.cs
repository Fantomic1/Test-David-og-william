using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Timeline;


public class GameManager : MonoBehaviour
{


    [Header("GamePlay")]
    [SerializeField]
    Collider StartChecker;
    [SerializeField]
    Collider EndChecker;
    [SerializeField]
    private TMP_Text Clock;
    [SerializeField]
    private Slider TimeLimit;
    [SerializeField]
    private int MaxTimeToComplete;
    int timer;

    [Header("Important stuff")]
    [SerializeField]
    private int MaxFallIntoVoid = -10;
    [SerializeField]
    private int MaxAmountAwayFromZero = 3000;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject ZeroPoint;
    [SerializeField]
    private GameObject RightHand;
    [SerializeField]
    private GameObject LeftHand;
    [SerializeField]
    private GameObject RightController;
    [SerializeField]
    private GameObject LeftController;

    [Header("Settings=1, Pause=2, Lost=3, Won=4")]
    [SerializeField]
    private List <GameObject> MenuList;



    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, ZeroPoint.transform.position) < MaxAmountAwayFromZero || Player.transform.position.y < MaxFallIntoVoid)
        {
            Reset();
        }

        if(Clock != null)
        {
            Clock.text = timer.ToString();
        }

        //If you have run out of time
        if (timer <= MaxTimeToComplete)
        {
            LostGame();
        }

        
    }
    public void FinishGame ()
    {
        MenuList[3].SetActive(true);
        if (Clock != null)
        {
            Clock.text = "";
        }
        PauseTime();
        Menucontrol();
    }
    public void LostGame()
    {
        MenuList[3].SetActive(true);
        PauseTime();
        Menucontrol();
    }

    //settings menu
    public void Settings ()
    {
        MenuList[1].SetActive(true);
    }
    //pause menu
    public void Pause ()
    {
        MenuList[2].SetActive(true);
        PauseTime();
        Menucontrol();
    }
    public void Menucontrol ()
    {
        RightHand.SetActive(false);
        LeftHand.SetActive(false);
        RightController.SetActive(true);
        LeftController.SetActive(true);
    }
    public void OutOfMenucontrol()
    {
        RightHand.SetActive(true);
        LeftHand.SetActive(true);
        RightController.SetActive(false);
        LeftController.SetActive(false);
    }


    public void PauseTime()
    {
        Time.timeScale = 0;
    }


    public void StartGame ()
    {
        if (Clock != null)
        {
            TimeLimit.maxValue = MaxTimeToComplete;
            StartCoroutine(Timer());
            TimeLimit.value = timer;
        }

        
    }
    //Gets you back to menu
    public void BackToMenu()
    {
        MenuList[1].SetActive(false);
        MenuList[2].SetActive(true);
        MenuList[1].SetActive(false);
        MenuList[1].SetActive(false);
    }
    public void BackToGame()
    {
        MenuList[1].SetActive(false);
        MenuList[2].SetActive(false);
        MenuList[3].SetActive(false);
        MenuList[4].SetActive(false);
        Time.timeScale = 1;
        OutOfMenucontrol();

    }



    //Resets scene
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        OutOfMenucontrol();

    }
    //Quits game
    public void Quit()
    {
        Application.Quit();
    }

    //pluses every second
    IEnumerator Timer ()
    {
        while (timer < MaxTimeToComplete)
        {
            timer++;
            TimeLimit.value = timer;
            yield return new WaitForSeconds(1);
        }
       
    }


}
