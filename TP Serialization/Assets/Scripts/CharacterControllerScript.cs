using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public CharacterController controller;
    Transform cubetouche;
    public Collider zonededetection;
    public Transform camera;

    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 velocity;
    float xRotation = 0f;
    public float mouseSensitivity = 100f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");





        velocity = (transform.right * x + transform.forward * z) * speed + transform.up * velocity.y;  

        velocity.y += gravity * Time.deltaTime;

        if (controller.isGrounded) velocity.y = 0;
            

        controller.Move(velocity * Time.deltaTime);     

        //controller.Move(move * speed * Time.deltaTime);
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        
        //Si le joueur appuie sur le clic de la souris, le cube detecter par son box collider sera soulevé au même niveau que la zonededetection 
        if (Input.GetMouseButtonDown(0) &&  cubetouche != null)
        {
            cubetouche.GetComponent<Rigidbody>().isKinematic = true;
            cubetouche.parent = camera;
            cubetouche.position = zonededetection.transform.position + zonededetection.transform.forward; 
        }

        //Si le joueur relache le clic de la souris, le cube dans les "bras" du personnage sera relaché
        if (Input.GetMouseButtonUp(0) &&  cubetouche != null)
        {
            cubetouche.parent = null;
            cubetouche.GetComponent<Rigidbody>().isKinematic = false;
            cubetouche = null;
        }

        if (Input.GetMouseButton(0) && Input.GetMouseButtonDown(1) && cubetouche != null)
        {
            cubetouche.parent = null;
            cubetouche.GetComponent<Rigidbody>().isKinematic = false;
            cubetouche.GetComponent<Rigidbody>().AddForce(camera.transform.forward * 10,ForceMode.VelocityChange);
            cubetouche = null;
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
        if(objetrelache.transform == cubetouche && Input.GetMouseButton(0) == false)
        {
            cubetouche = null;

        }

       
    }
    
    
    
    
    
    
}//Fin de script
