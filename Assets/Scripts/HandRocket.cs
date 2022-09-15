using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using TMPro;
using UnityEngine.UI;

public class HandRocket : MonoBehaviour
{
    [Header("Particle System")]
    [SerializeField]
    private ParticleSystem RightRocketFlames;
    [SerializeField]
    private ParticleSystem LeftRocketFlames;
    [SerializeField]
    private GameObject RightRocketLight;
    [SerializeField]
    private GameObject LeftRocketLight;


    [Header("Rocket")]
    public float RocketFuel;
    const float MAXFUEl = 10;
    const float ROCKETCONSUMPTION = 1.5f;
    private bool RocketOF = false;
    [SerializeField]
    private Slider FuelUI;
    public Rigidbody body;
    private const float SPEED = 2000;



    [Header("Spiderman")]
    public Transform leftHand;
    public Transform rightHand;
    public SpringJoint leftSpring;
    public SpringJoint rightSpring;
    public LineRenderer leftLine;
    public LineRenderer rightLine;
    public Material laser;
    public Material rope;
    [SerializeField]
    private Transform RightLaserPos;
    [SerializeField]
    private Transform LeftLaserPos;




    private bool leftOn;
    private bool rightOn;



    private void Start()
    {
        FuelUI.maxValue = MAXFUEl;
        FuelUI.value = RocketFuel;
    }

    // Update is called once per frame
    void Update()
    {
        FuelUI.value = RocketFuel;

        //maxpower add
        if (RocketFuel < MAXFUEl)
        {
            RocketFuel += Time.deltaTime;
        }
        
        if(RocketFuel < 0)
        {
            RocketOF = true;
            
        }

        if(RocketFuel > MAXFUEl - 1)
        {
            RocketOF = false;
        }

        //Rocket
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) && RocketFuel > 0 && !RocketOF)
        {
            body.AddForce(leftHand.forward * SPEED * Time.deltaTime);
            RocketFuel -= ROCKETCONSUMPTION * Time.deltaTime;
            LeftRocketFlames.Play();
            LeftRocketLight.SetActive(true);
        }
        else
        {
            LeftRocketFlames.Stop();
            LeftRocketLight.SetActive(false);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && RocketFuel > 0 && !RocketOF)
        {
            body.AddForce(rightHand.forward * SPEED * Time.deltaTime);
            RocketFuel -= ROCKETCONSUMPTION * Time.deltaTime;
            RightRocketFlames.Play();
            RightRocketLight.SetActive(true);
        }
        else
        {
            RightRocketFlames.Stop();
            RightRocketLight.SetActive(false);
        }


        //graple

        //line renderer
        //left

        RaycastHit leftRender;
        Physics.Raycast(leftHand.position + leftHand.forward, leftHand.forward, out leftRender);
        leftLine.SetPosition(0, LeftLaserPos.transform.position);
        if (leftOn)
        {
            leftLine.material = laser;
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
            leftLine.material = rope;
            leftLine.SetPosition(1, leftSpring.connectedAnchor);
        }
        

        //right
        RaycastHit rightRender;
        Physics.Raycast(rightHand.position + rightHand.forward, rightHand.forward, out rightRender);
        rightLine.SetPosition(0, RightLaserPos.transform.position);
        if (rightOn)
        {
            rightLine.material = laser;
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
            rightLine.material = rope;
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