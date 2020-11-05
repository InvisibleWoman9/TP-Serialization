using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControllerScript : MonoBehaviour
{
    public CharacterController controller;
    Transform cubetouche;
    public Transform camera;
    public Transform epauleG, epauleD;

    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 velocity;
    float xRotation = 0f;
    public float mouseSensitivity = 100f;
    public Image mask;
    
    
    
    
    
    
    

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
        
        
        if (Input.GetMouseButtonDown(0))
        {
            epauleG.Rotate(Vector3.up, 10f);
            epauleD.Rotate(Vector3.up, -10f);
            RaycastHit hit;
            if (Physics.Raycast(camera.position, camera.forward, out hit, 3f))
            {
                Debug.Log(hit.transform.name+hit.transform.tag);
                if (hit.transform.tag == "MOVETHIS")
                {
                   Ramassercube(true, hit.transform);
                }
            }
        }

        
        if (Input.GetMouseButtonUp(0))
        {
            epauleG.Rotate(Vector3.up, -10f);
            epauleD.Rotate(Vector3.up, 10f);
            if(cubetouche != null) Ramassercube(false, cubetouche);
        }

        if (Input.GetMouseButtonDown(1) && cubetouche != null)
        {
            Rigidbody cubelance = cubetouche.GetComponent<Rigidbody>();
            Ramassercube(false, cubetouche);
            cubelance.AddForce(camera.transform.forward * 10,ForceMode.VelocityChange);

        }

    }//FIN UPDATE


    void Ramassercube(bool wanted, Transform cube)
    {

        if (wanted)
        {
            cube.parent = camera;
            cube.localPosition = new Vector3(0 , 0, 2.2f);
            cubetouche = cube;
        }
        else
        {
            cube.parent = null;
            cubetouche = null;
        }  
        cube.GetComponent<Rigidbody>().isKinematic = wanted;
        
    }
    
    

    



    public void ActivateMask(bool wanted, Color desiredcolor)
    {
        mask.color = desiredcolor;
        mask.gameObject.SetActive(wanted);
    }
    

    
    
    
    
    
    
}//Fin de script
