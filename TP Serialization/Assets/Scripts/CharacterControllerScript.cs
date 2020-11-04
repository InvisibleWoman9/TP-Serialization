using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public CharacterController controller;
    Transform cubetouche;
    public Collider zonededetection;

    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 velocity;


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");





        velocity = (transform.right * x + transform.forward * z) * speed + transform.up * velocity.y;  

        velocity.y += gravity * Time.deltaTime;

        if (controller.isGrounded) velocity.y = 0;
            

        controller.Move(velocity * Time.deltaTime);     

        //controller.Move(move * speed * Time.deltaTime);
        
        //Si le joueur appuie sur la touche espace, le cube detecter par son box collider sera soulevé au même niveau que la zonededetection 
        if (Input.GetButtonDown("Jump") &&  cubetouche != null)
        {
            cubetouche.GetComponent<Rigidbody>().isKinematic = true;
            cubetouche.parent = this.transform;
            cubetouche.position = zonededetection.transform.position; 
        }

        //Si le joueur relache la touche espace, le cube dans les "bras" du personnage sera relaché
        if (Input.GetButtonUp("Jump") &&  cubetouche != null)
        {
            cubetouche.parent = null;
            cubetouche.GetComponent<Rigidbody>().isKinematic = false;
        }

    }//FIN UPDATE
    
    
    
    //Si l'objet touche par le box collider du perso à le Tag correspondant à l'action voulu, alors il est mis en mémoire
    void OnTriggerEnter(Collider objettouche)
    {
        
        if(objettouche.gameObject.CompareTag("MOVETHIS") && cubetouche == null)
        {
            
            cubetouche = objettouche.transform;

        }

         
        
    }

    
    void OnTriggerExit(Collider objetrelache)
    {
        if(objetrelache.transform == cubetouche)
        {
            cubetouche = null;

        }

       
    }
    
    
    
    
    
    
}//Fin de script
