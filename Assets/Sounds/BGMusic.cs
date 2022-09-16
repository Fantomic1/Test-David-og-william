using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class BGMusic : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip sound;
    public AudioSource wind;
    public AudioSource rocket;
    public Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float Speed = (Mathf.Clamp(body.velocity.magnitude, 0, 100) / 100) * 3;
        wind.pitch =  Speed;
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)  || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            rocket.pitch = 3;
        }
        else
        {
            rocket.pitch = 0;
        }
    }

    public void ChekSound()
    {
            Source.PlayOneShot(sound);
    }
}
