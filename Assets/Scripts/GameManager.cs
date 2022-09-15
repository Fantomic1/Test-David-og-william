using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
        
    }
    public void FinishGame ()
    {
        if(Clock != null)
        {
            Clock.text = "";
        }
        
    }
    public void LostGame()
    {

    }

    public void StartGame ()
    {
        if (Clock != null)
        {
            StartCoroutine(Timer());
        }
        
    }

    //Resets scene
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //pluses every second
    IEnumerator Timer ()
    {
        timer++;
        yield return new WaitForSeconds(1);
    }


}
