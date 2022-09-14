using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class HandRocket : MonoBehaviour
{
    
    public Rigidbody body;
    private const float SPEED = 800;

    public Transform leftHand;
    public Transform rightHand;

    public SpringJoint leftSpring;
    public SpringJoint rightSpring;

    public LineRenderer leftLine;
    public LineRenderer rightLine;

    private bool leftOn;
    private bool rightOn;
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //Rocket
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            
            body.AddForce(leftHand.forward * SPEED * Time.deltaTime);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            body.AddForce(rightHand.forward * SPEED * Time.deltaTime);
        }

        //graple

        //line renderer
        //left
        
        RaycastHit leftRender;
        Physics.Raycast(leftHand.position + leftHand.forward, leftHand.forward, out leftRender);
        leftLine.SetPosition(0, leftHand.transform.position);
        if (leftOn)
        {
            if (leftRender.collider != null)
            {
                leftLine.SetPosition(1, leftRender.point);
            }
            else
            {
                leftLine.SetPosition(1, leftHand.position + leftHand.forward * 50);
            }
        }
        else
        {
            leftLine.SetPosition(1, leftSpring.connectedAnchor);
        }
        

        //right
        RaycastHit rightRender;
        Physics.Raycast(rightHand.position + rightHand.forward, rightHand.forward, out rightRender);
        rightLine.SetPosition(0, rightHand.transform.position);
        if (rightOn)
        {
            if (rightRender.collider != null)
            {
                rightLine.SetPosition(1, rightRender.point);
            }
            else
            {
                rightLine.SetPosition(1, rightHand.position + rightHand.forward * 50);
            }
        }
        else
        {
            rightLine.SetPosition(1, rightSpring.connectedAnchor);
        }
       



        //graple turn off spring.
        if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            leftSpring.connectedBody = body;
            leftOn = true;  
        }


        if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            rightSpring.connectedBody = body;
            rightOn = true;
        }

        //graple turn on spring.
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            RaycastHit hit;
            Physics.Raycast(leftHand.position + leftHand.forward, leftHand.forward, out hit);
            leftSpring.connectedAnchor = hit.point;
            if(hit.collider != null)
            {
                leftSpring.connectedBody = null;
                body.WakeUp();
                leftOn = false;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            RaycastHit hit;
            Physics.Raycast(rightHand.position + rightHand.forward, rightHand.forward, out hit);
            rightSpring.connectedAnchor = hit.point;

            if (hit.collider != null)
            {
                rightSpring.connectedBody = null;
                body.WakeUp();
                rightOn = false;
            }
        }

    }
}
