using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip sound;
    public AudioSource wind;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Source.PlayOneShot(sound);
        }

        wind.pitch = Input.GetAxis("Vertical") * 3;
    }
}
