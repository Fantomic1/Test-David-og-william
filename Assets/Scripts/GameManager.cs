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
    private TMP_Text WinScore;
    [SerializeField]
    private List<GameObject> Stars;
    [SerializeField]
    private TMP_Text LoseScore;


    [Header("GamePlay")]
    [SerializeField]
    Collider StartChecker;
    [SerializeField]
    Collider EndChecker;
    [SerializeField]
    private TMP_Text Clock;
    [SerializeField]
    private int MaxTimeToComplete;
    int timer;
    [SerializeField]
    private Slider TimeUI;

    [Header("Important stuff")]
    [SerializeField]
    private int MaxFallIntoVoid = -10;
    [SerializeField]
    private int MaxAmountAwayFromZero = 3000;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject ZeroPoint;

    [HideInInspector]
    public bool isPaused = false;

    [Header("Settings=1, Pause=2, Lost=3, Won=4")]
    [SerializeField]
    private List <GameObject> MenuList;



    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, ZeroPoint.transform.position) < MaxAmountAwayFromZero || Player.transform.position.y < MaxFallIntoVoid)
        {
            Reset();
        }

            Clock.text = timer.ToString();
        

        //If you have run out of time
        if (timer <= MaxTimeToComplete)
        {
            LostGame();
        }

        
    }
    public void FinishGame ()
    {
        MenuList[3].SetActive(true);
  
            Clock.text = "";
       
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
    }
    public void LostGame()
    {
        MenuList[3].SetActive(true);
        PauseTime();
        LoseScore.text = ("Your Time: " + timer);
    }

    void CloseALL ()
    {
        MenuList[0].SetActive(false);
        MenuList[1].SetActive(false);
        MenuList[2].SetActive(false);
        MenuList[3].SetActive(false);
    }


    //settings menu
    public void Settings ()
    {
        CloseALL();
        MenuList[0].SetActive(true);
    }
    //pause menu
    public void Pause ()
    {
        MenuList[1].SetActive(true);
        PauseTime();
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
            TimeUI.maxValue = MaxTimeToComplete;
            StartCoroutine(Timer());
            TimeUI.value = timer;


        
    }
    //Gets you back to menu
    public void BackToMenu()
    {
        MenuList[0].SetActive(false);
        MenuList[1].SetActive(true);
        MenuList[2].SetActive(false);
        MenuList[3].SetActive(false);
    }
    public void BackToGame()
    {
        MenuList[0].SetActive(false);
        MenuList[1].SetActive(false);
        MenuList[2].SetActive(false);
        MenuList[3].SetActive(false);
        UnPauseTime();

    }



    //Resets scene
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UnPauseTime();
            Stars[0].SetActive(false);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
        
        
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
            TimeUI.value = timer;
            yield return new WaitForSeconds(1);
        }
       
    }


}
