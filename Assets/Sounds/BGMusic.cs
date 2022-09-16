using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class BGMusic : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip sound;
    public AudioSource wind;
    public AudioSource rocketLeft;
    public AudioSource rocketRight;
    public Rigidbody body;
    public bool leftOn = false;
    public bool rightOn = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float Speed = (Mathf.Clamp(body.velocity.magnitude, 0, 100) / 100) * 3;
        wind.pitch =  Speed;
        if (leftOn)
        {
            rocketLeft.pitch = 3;
        }
        else
        {
            rocketLeft.pitch = 0;
        }

        if (rightOn)
        {
            rocketRight.pitch = 3;
        }
        else
        {
            rocketRight.pitch = 0;
        }
    }

    public void ChekSound()
    {
            Source.PlayOneShot(sound);
    }
}
