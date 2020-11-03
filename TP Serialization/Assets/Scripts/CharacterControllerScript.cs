using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public CharacterController controller;

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

    }
    
    
    
    
    
    
    
    
}//Fin de script
