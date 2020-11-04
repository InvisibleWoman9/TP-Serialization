using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public MeshRenderer skin;
    void Start()
    {
        skin = GetComponent<MeshRenderer>();
    }

    
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("PEINTURES"))
        {
            skin.material.color = col.GetComponent<MeshRenderer>().material.color;
        }
    }
}//FIN DU SCRIPT
