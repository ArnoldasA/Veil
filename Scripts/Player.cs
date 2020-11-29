using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
//Declaring all variables for character movement script
{
    public float speed = 4;
    private int rot;
    public float gravity = -9.8f;
    public CharacterController controller;
    public Transform ground;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;//Making sure we are on the floor with layermasks

    private bool paused;
    
    Vector3 velocity;
    bool grounded;


    private void Update()
    {

       
//Checking if the player is touching the ground
        grounded = Physics.CheckSphere(ground.position, groundDistance, groundMask);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //Pausing game and showing mouse // Should be moved to a different function
        if (paused)
        {
            
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
        //Assigning movement vector to the Unity character controller
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
      
      //Gravity vector applied to chracter 
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        
    }
   

}
