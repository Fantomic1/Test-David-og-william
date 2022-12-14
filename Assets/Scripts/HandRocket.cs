using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class HandRocket : MonoBehaviour
{
    [Header("UI")]
    public GameManager gameManager;
    public BGMusic bgmusic; 
    


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
    private float RocketFuel;
    const float MAXFUEl = 10;
    const float ROCKETCONSUMPTION = 1.5f;
    private bool OverHeat = false;
    [SerializeField]
    private Image fillRight;
    [SerializeField]
    private Slider FuelUIRight;
    public Rigidbody body;
    private const float SPEED = 2000;



    [Header("Spiderman")]
    [SerializeField]
    private float SwingDistance;
    public SpringJoint leftSpring;
    public SpringJoint rightSpring;
    public LineRenderer leftLine;
    public LineRenderer rightLine;
    public Material rope;
    [SerializeField]
    private Transform RightLaserPos;
    [SerializeField]
    private Transform LeftLaserPos;
    [SerializeField]
    private LayerMask SwingLayers;
    [SerializeField]
    private Material CanSwing;
    [SerializeField]
    private Material CanNotSwing;





    private bool leftOn;
    private bool rightOn;



    private void Start()
    {
        FuelUIRight.maxValue = MAXFUEl;
        FuelUIRight.value = RocketFuel;
        fillRight.color = Color.green;

        leftSpring.connectedAnchor = transform.position;
        rightSpring.connectedAnchor = transform.position;

        rightSpring.connectedBody = body;
        rightOn = true;

        leftSpring.connectedBody = body;
        leftOn = true;
    }

    // Update is called once per frame
    void Update()
    {

            FuelUIRight.value = RocketFuel;

            //maxpower add
            if (RocketFuel < MAXFUEl)
            {
                RocketFuel += Time.deltaTime;
            }

            if (RocketFuel < 0)
            {
                OverHeat = true;
            }

            if (RocketFuel > MAXFUEl - 1)
            {
                OverHeat = false;
            }

            if (OverHeat == true)
            {
                fillRight.color = Color.red;
            }
            else
            {
                fillRight.color = Color.green;
            }



            //Rocket
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) && RocketFuel > 0 && !OverHeat)
            {
                bgmusic.leftOn = true;
                body.AddForce(LeftLaserPos.forward * SPEED * Time.deltaTime);
                RocketFuel -= ROCKETCONSUMPTION * Time.deltaTime;
                LeftRocketFlames.Play();
                LeftRocketLight.SetActive(true);
            }
            else
            {
                bgmusic.leftOn = false;
                LeftRocketFlames.Stop();
                LeftRocketLight.SetActive(false);
            }
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && RocketFuel > 0 && !OverHeat)
            {
                body.AddForce(RightLaserPos.forward * SPEED * Time.deltaTime);
                bgmusic.rightOn = true;
                RocketFuel -= ROCKETCONSUMPTION * Time.deltaTime;
                RightRocketFlames.Play();
                RightRocketLight.SetActive(true);
            }
            else
            {
                bgmusic.rightOn = false;
                RightRocketFlames.Stop();
                RightRocketLight.SetActive(false);
            }


            //graple

            //line renderer
            //left

            RaycastHit leftRender;
            Physics.Raycast(LeftLaserPos.position + LeftLaserPos.forward, LeftLaserPos.forward, out leftRender);
            leftLine.SetPosition(0, LeftLaserPos.transform.position);
            if (leftOn)
            {
                if (leftRender.collider != null && leftRender.transform.gameObject.layer == LayerMask.NameToLayer("Swingable") && Vector3.Distance(leftRender.transform.position, LeftLaserPos.transform.position) <= SwingDistance)
                {
                    leftLine.SetPosition(1, leftRender.point);
                    leftLine.material = CanSwing;

                }
                else
                {
                    leftLine.SetPosition(1, LeftLaserPos.position + LeftLaserPos.forward * 50);
                    leftLine.material = CanNotSwing;
                }
            }
            else
            {
                leftLine.material = rope;
                leftLine.SetPosition(1, leftSpring.connectedAnchor);
            }


            //right
            RaycastHit rightRender;
            Physics.Raycast(RightLaserPos.position + RightLaserPos.forward, RightLaserPos.forward, out rightRender);
            rightLine.SetPosition(0, RightLaserPos.transform.position);
            if (rightOn)
            {
                if (rightRender.collider != null && rightRender.transform.gameObject.layer == LayerMask.NameToLayer("Swingable") && Vector3.Distance(rightRender.transform.position, RightLaserPos.transform.position) <= SwingDistance)
                {
                    rightLine.SetPosition(1, rightRender.point);
                    rightLine.material = CanSwing;
                }
                else
                {
                    rightLine.SetPosition(1, RightLaserPos.position + RightLaserPos.forward * 50);
                    rightLine.material = CanNotSwing;
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
            if (gameManager.isPaused == false)
            {
                RaycastHit hit;
                Physics.Raycast(LeftLaserPos.position + LeftLaserPos.forward, LeftLaserPos.forward, out hit);
                leftSpring.connectedAnchor = hit.point;
                if(hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Buttons"))
                {
                    Buttons buttons = hit.collider.GetComponent<Buttons>();
                    buttons.ButtonPressed();
                }else if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Swingable") && Vector3.Distance(hit.transform.position, LeftLaserPos.transform.position) <= SwingDistance)
                {
                    leftSpring.connectedBody = null;
                    body.WakeUp();
                    leftOn = false;
                }
            }
       

            }

            if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
            if (gameManager.isPaused == false)
            {
                RaycastHit hit;
                Physics.Raycast(RightLaserPos.position + RightLaserPos.forward, RightLaserPos.forward, out hit);
                rightSpring.connectedAnchor = hit.point;
                if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Buttons"))
                {
                    Buttons buttons = hit.collider.GetComponent<Buttons>();
                    buttons.ButtonPressed();
                }
                else if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Swingable") && Vector3.Distance(hit.transform.position, RightLaserPos.transform.position) <= SwingDistance)
                {
                    rightSpring.connectedBody = null;
                    body.WakeUp();
                    rightOn = false;
                }
            }
             
            }


            if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
            {
            gameManager.Reset();
<<<<<<< HEAD
            
           

=======
>>>>>>> e8fc3488ad7909dc9673d58aab9dfb648f073906
            }
    }
}
