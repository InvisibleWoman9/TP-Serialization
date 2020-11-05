using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    Color myColor;
    public GameObject particles;
    private List<Collider> objetstouches;
    
    void Start()
    {
        myColor = GetComponent<MeshRenderer>().material.color;
        objetstouches = new List<Collider>();
    }

    
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") || col.CompareTag("MOVETHIS"))
        {
            col.GetComponent<MeshRenderer>().material.color = myColor;
            objetstouches.Add(col);
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player") || col.CompareTag("MOVETHIS"))
        {
            objetstouches.Remove(col);
        }
    }
    
    
    
    
}
