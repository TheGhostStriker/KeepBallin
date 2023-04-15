using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController charController;

    public float runSpeed = 40f;

    public float horizontalMove = 0f;
    public bool jump = false;
    public bool crouch = false;

    public Joystick joystick;

    
  

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        if(joystick.Horizontal >= .2f)
        {
            horizontalMove = runSpeed;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            horizontalMove = -runSpeed;
        }
        else
        {
            horizontalMove = 0f;
        }

        float verticalMove = joystick.Vertical;

        if (verticalMove >= .2f)
        {
            verticalMove = runSpeed;
        }

        if (verticalMove <= -.2f)
        {
            verticalMove = -runSpeed;
        }
        

        
    }
}
