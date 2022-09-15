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

    int timer;

    [Header("Important stuff")]
    [SerializeField]
    GameObject Player;

    


    private void Update()
    {
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
