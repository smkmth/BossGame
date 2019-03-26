using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VerticalAndHorizontalMovement : MonoBehaviour
{
    //Anything with the two slashes infront of it is a comment, which means it wont be read by the computer.
    //public variables can be accessed in the editor, and also by other classes.

    // here i declare variables that i will use later in the code. you can do this anywhere but it is convention
    //to do this at the start. 

    //this is a floating point value controlling movement speed, setting this higher will
    //make the character move faster. the actual value is set in the editor.
    public float MovementSpeed;
    public float AirMovementSpeed;

    //this is a floating point value controlling jump force, the higher this is the higher 
    //our guy jumps.
    public float JumpForce;

    public float DistanceToGround;

    //these variables are private. that means i will set them in the code itself. 
    // by writing private on these they will not show up in the edior but i can still 
    // set and use them in the code. 

    //this is a vector3  (three numbers that indicate a position in 3d space) that i am adding
    //to this ridgidbody to make it move
    private Vector3 movepos;

    //this is a ridgidbody, attached to the same gameobject this script is on. 
    private Rigidbody rb;

    private float currentMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //I 'get' a reference to the ridgidbody attached to this gameobject
        //so i can use it later
        rb = GetComponent<Rigidbody>();

        
    }


    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, DistanceToGround + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            //rb.AddForce(movepos + transform.forward * Input.GetAxis("Vertical") * MovementSpeed);
            transform.position += movepos + transform.forward * Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime;

        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            //rb.AddForce(movepos + transform.right * Input.GetAxis("Horizontal") * MovementSpeed);
            transform.position += movepos + transform.right * Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime;

        }
        //same logic for movement but applied veritcally. the get button down returns true 
        //when a button is pressed and released. 
        if (Input.GetButtonDown("Jump"))
        {
            movepos.y = 1;
            //here i apply force as a single impulse all at once, instead of continuously over time 
            rb.AddForce(movepos * JumpForce, ForceMode.Impulse);
        }
        else
        {
            movepos.y = 0;
        }

        if (isGrounded())
        {
            currentMoveSpeed = MovementSpeed;
            rb.drag = 10.0f;
        }
        else
        {
            rb.drag = 0.0f;
            currentMoveSpeed = AirMovementSpeed;
        }
    }
}
