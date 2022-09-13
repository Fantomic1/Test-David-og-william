using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;


/// <summary>
/// you can do the spiderman.
/// </summary>
public class Grabling_gun : MonoBehaviour
{
    


    [SerializeField]
    GameObject player;
    [SerializeField]
    Transform Firepoint;
    [SerializeField]
    GameObject Hook;
    [SerializeField]
    float DistanceAtGoal;
    [HideInInspector]
    public bool IsInHand = false;


    private void Update()
    {
        if (GetComponent<OVRGrabbable>().isgrabling == true)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
            {
                Fire();
            }
        }

    }

    void Fire()
    {

        RaycastHit hit;

        if(Physics.Raycast(Firepoint.position, Firepoint.forward, out hit))
        {
            Vector3 target = hit.point;
            StartCoroutine(GrablingRutine(target));

        }
    }


    IEnumerator GrablingRutine (Vector3 target)
    {
        //gets the distance between the firepoint and the feet of the player
        float distanceToFeet = Vector3.Distance(Firepoint.transform.position, player.transform.position);

        //this runs alsong as we are away from the grabling object/wall
        while(Vector3.Distance(target, Hook.transform.position) > DistanceAtGoal)
        {
           Vector3 pos = Hook.transform.position += new Vector3(0, 0, 1);
            pos.y -= distanceToFeet;
            yield return new WaitForSeconds(0.1f);
        }

        while(Vector3.Distance(target, player.transform.position) > DistanceAtGoal)
        {
            Vector3 pos = player.transform.position += new Vector3(0, 0, 1);
            pos.y -= distanceToFeet;
            yield return new WaitForSeconds(0.1f);
        }


    }

    void reload ()
    {
        Hook.transform.position = Firepoint.transform.position;
        Hook.transform.rotation = Firepoint.transform.rotation;
    }


}
