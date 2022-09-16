using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chekpoint : MonoBehaviour
{
    private ChekpointManager chekpointManager;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        chekpointManager = GameObject.FindGameObjectWithTag("ChekpointManager").GetComponent<ChekpointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
            chekpointManager.ChekpointTagged(this.gameObject);   
    } 
}
