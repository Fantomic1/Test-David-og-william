using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class HandRocket : MonoBehaviour
{
    
    public Rigidbody body;
    private const float SPEED = 600;

    public Transform leftHand;
    public Transform rightHand;

    public SpringJoint leftSpring;
    public SpringJoint rightSpring;
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            leftSpring.spring = 10;
        }
        else
        {
            leftSpring.spring = 0;
        }

        if(OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            rightSpring.spring = 10;
        }
        else
        {
            rightSpring.spring = 0;
        }
        rightSpring.gameObject.SetActive(OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch));

        //Rocket
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            
            body.AddForce(leftHand.forward * SPEED * Time.deltaTime);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            body.AddForce(rightHand.forward * SPEED * Time.deltaTime);
        }

        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            RaycastHit hit;
            Physics.Raycast(leftHand.position + leftHand.forward, leftHand.forward, out hit);
            leftSpring.connectedAnchor = hit.point;
        }

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            RaycastHit hit;
            Physics.Raycast(rightHand.position + rightHand.forward, rightHand.forward, out hit);
            rightSpring.connectedAnchor = hit.point;
        }

    }
}
