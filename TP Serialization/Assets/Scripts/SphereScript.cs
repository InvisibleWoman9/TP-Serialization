using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    Color myColor;
    public GameObject particles;
    private List<Collider> objetstouches;
    Collider spherecol;
    private ParticleSystem mypParticles;
    
    
    void Start()
    {
        myColor = GetComponent<MeshRenderer>().material.color;
        spherecol = GetComponent<SphereCollider>();
        objetstouches = new List<Collider>();
        mypParticles = Instantiate(particles, transform.position, transform.rotation).GetComponent<ParticleSystem>();
        mypParticles.Stop();
    }

    
    void Update()
    {
        if (objetstouches.Count > 0)
        {
            Vector3 partpos = objetstouches[objetstouches.Count - 1].transform.position - transform.position;
            partpos.Normalize();
            partpos *= 1.25f;
            partpos += transform.position;
            mypParticles.transform.position = partpos;

        }
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") || col.CompareTag("MOVETHIS"))
        {
            col.GetComponent<MeshRenderer>().material.color = myColor;
            objetstouches.Add(col);
            mypParticles.transform.position = col.ClosestPointOnBounds(transform.position);
            mypParticles.Play();
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player") || col.CompareTag("MOVETHIS"))
        {
            objetstouches.Remove(col);
            if (objetstouches.Count < 1)
            {
                mypParticles.Stop();
            }
        }
    }
    
    
    
    
}
