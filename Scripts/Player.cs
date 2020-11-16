using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4;
    private int rot;
    public float gravity = -9.8f;
    public CharacterController controller;
    public Transform ground;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool paused;
    
     private float walkAudioSpeed = 0.4f;
    private float walkAudioTimer = 0.0f;

     private bool isWalking  = false;

    private Vector3 lastpos = new Vector3(0, 0, 0);
    

    Vector3 velocity;
    bool grounded;

    private void Start()
    {
        
    }

    private void Update()
    {

       

        grounded = Physics.CheckSphere(ground.position, groundDistance, groundMask);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
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
        
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        
    }
        /*
    public void SavePlayer()
    {
        SaveGame.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveGame.LoadPlayer();
            if (data != null)
        {
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;
        }
    }
    */

}
