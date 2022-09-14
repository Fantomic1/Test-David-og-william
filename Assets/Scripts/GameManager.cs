using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    Collider StartChecker;
    [SerializeField]
    Collider EndChecker;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    private TMP_Text Clock;

    int timer;


    private void Update()
    {
        Clock.text = timer.ToString();
    }
    public void FinishGame ()
    {
        Clock.text = "";
    }

    public void StartGame ()
    {
        StartCoroutine(Timer());
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
