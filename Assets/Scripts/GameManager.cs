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
    [Header("UI")]
    [SerializeField]
    private Text WinScore;
    [SerializeField]
    private List<GameObject> Stars;
    [SerializeField]
    private Text LoseScore;


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
    [SerializeField]
    private GameObject RightLineRenderer;
    [SerializeField]
    private GameObject LeftLineRenderer;



    public bool isPaused;

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
        WinScore.text = ("Your Time: " + timer);
        if(timer < MaxTimeToComplete / 2)
        {
            Stars[1].SetActive(true);
            if (timer < MaxTimeToComplete / 3)
            {
                Stars[2].SetActive(true);
                if (timer < MaxTimeToComplete / 4)
                {
                    Stars[3].SetActive(true);
                }
            }
            
        }

        PauseTime();
        Menucontrol();
    }
    public void LostGame()
    {
        MenuList[3].SetActive(true);
        PauseTime();
        Menucontrol();
        LoseScore.text = ("Your Time: " + timer);
    }

    void CloseALL ()
    {
        MenuList[1].SetActive(false);
        MenuList[2].SetActive(false);
        MenuList[1].SetActive(false);
        MenuList[1].SetActive(false);
    }


    //settings menu
    public void Settings ()
    {
        CloseALL();
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
        RightLineRenderer.SetActive(false);
        LeftLineRenderer.SetActive(false);
    }
    public void OutOfMenucontrol()
    {
        RightHand.SetActive(true);
        LeftHand.SetActive(true);
        RightController.SetActive(false);
        LeftController.SetActive(false);
        RightLineRenderer.SetActive(true);
        LeftLineRenderer.SetActive(true);
    }


    public void PauseTime()
    {
        isPaused = true;
        Time.timeScale = 0;
    }
    public void UnPauseTime()
    {
        isPaused = false;
        Time.timeScale = 1;
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
        UnPauseTime();
        OutOfMenucontrol();

    }



    //Resets scene
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UnPauseTime();
        OutOfMenucontrol();
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            Stars[3].SetActive(false);
        
        
    }
    //Quits game
    public void Quit()
    {
        Application.Quit();
    }

    //pluses every second
    IEnumerator Timer ()
    {
        while (timer < MaxTimeToComplete && isPaused == false)
        {
            timer++;
            TimeLimit.value = timer;
            yield return new WaitForSeconds(1);
        }
       
    }


}
