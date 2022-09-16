using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekpointManager : MonoBehaviour
{
    public GameObject[] Chekpoints;
    private int currentchekpoint = 0;
    public Material Shine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChekpointTagged(GameObject Chekpoint)
    {
        if(Chekpoint == Chekpoints[currentchekpoint])
        {
            Destroy(Chekpoints[currentchekpoint]);
            Chekpoints[currentchekpoint + 1].GetComponent<MeshRenderer>().material = Shine;
            currentchekpoint++;
        }
    }
}
