using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    
    // public Keyboard keyboard;
    private Rigidbody2D rb;
    private PlayerControls playerControls;

    public int jumpCount = 0;
    public int maxJump = 2;
    public int moveSpeed = 5;
    public int jumpSpeed = 3;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        // PlayerControls is the name of the set of control you set 
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
        
        // In this case: Player is the action map; Jump is the Actions 
        // += in C# 'subscribes' the function with the event so multiple functions can be subscribed to the event
        playerControls.Player.Jump.performed += Jump;
        playerControls.Player.Jump.canceled += JumpStop;
        playerControls.Player.Movement.canceled+= MoveStop;
    }

    public void Jump(InputAction.CallbackContext context) {
        // Context tells you all the details of the input data
        Debug.Log(context);
        // Only one instance when button is pressed (Not when let go or started)
        if (context.performed && jumpCount < maxJump){
            //Debug.Log("Jump " + context.phase);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    public void JumpStop(InputAction.CallbackContext context) {
        if (context.canceled) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    public void MoveStop(InputAction.CallbackContext context) {
        if (context.canceled) {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
    }


     //public void Move(InputAction.CallbackContext context) {
     //   Debug.Log(context);
     //   Vector2 inputVector = context.ReadValue<Vector2>();
     //   rb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * moveSpeed, ForceMode2D.Force);
     //}


    // Update is called once per frame
    void FixedUpdate() {
        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        
        rb.velocity = new Vector2(inputVector.x * moveSpeed, rb.velocity.y);
        // too slippery
        //rb.AddForce(new Vector3(inputVector.x, 0 , inputVector.y) * moveSpeed, ForceMode2D.Force);   
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            jumpCount = 0;
        }
    }


}
