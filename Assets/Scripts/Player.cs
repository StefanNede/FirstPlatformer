using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour // inheriting from monoBehaviour -> gives access to Start(), Update()...
{

    // we're gonna create a child object that we're gonna set to the feet of the bean 
    // serialising it allows us to then drag the child object onto this in the inspector
    [SerializeField] private Transform groundCheckTransform = null; // SerializeField makes it visible in the unity inspector
    [SerializeField] private LayerMask playerMask; // making it public has the same effect as serializeField
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;
    private int superJumpsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame - be careful (on slower computer this will be called less times)
    // any key presses or mouse clicks must be in update because they could be missed in fixed update
    // depending on how good your computer is 
    void Update()
    {
        // check if space bar is pressed down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // Fixed update is called once every physics update - the physics engine updates at 100hz
    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 2, rigidbodyComponent.velocity.y, 0); // Vector3(x,y,z)
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            // if 0 there are 0 things it is colliding with -> in the air 
            // in the unity inspector we went down to player mask and selected everything but the player 
            // so that the OverlapSphere returns a list of everything the player is colliding with, not including itself
            return;
        }

        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) // the layer for the coin is 9 - check in unity 
        {
            // every time coin collected increase coins collected counter and give a super jump
            Destroy(other.gameObject);
            superJumpsRemaining++;
            ScoreManager.instance.AddPoint();
        }

    }

}
