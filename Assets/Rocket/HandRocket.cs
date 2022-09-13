using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class HandRocket : MonoBehaviour
{
    public bool left;
    public Rigidbody body;
    private const float SPEED = 600;
    public GameObject hitGraple;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) && left)
        {
            
            body.AddForce(transform.forward * SPEED * Time.deltaTime);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && !left)
        {
            body.AddForce(transform.forward * SPEED * Time.deltaTime);
        }


        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch) && left)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + transform.forward, transform.forward, out hit);
            Debug.Log(hit.point);
        }
    }
}
