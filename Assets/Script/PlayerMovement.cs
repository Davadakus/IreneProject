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
    public float jumpSpeed = 1f; // Higher value == Higher Jump
    public float gravity;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
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
        //Debug.Log(context);
        //Debug.Log("Jump " + context.phase);
        // Only one instance when button is pressed (Not when let go or started)
        if (context.performed && jumpCount < maxJump){
            //rb.velocity = new Vector2(rb.velocity.x, 0);
            //Experimenting with jumping using ForceMode

                //if (rb.velocity.y < 0) { // midair jump when falling count as 2 jumps
                //    Debug.Log("MidAir");
                //    jumpCount++;
                //}
                //if (jumpCount > 0) { // double jump
                //    rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity
                //    rb.AddForce(Vector3.up * jumpSpeed*2f, ForceMode2D.Impulse); //Double jump height to conteract gravity
                //}
                //else { // First jump
                //    rb.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
                //}

            float jumpVelocity = Mathf.Sqrt(gravity * jumpSpeed);
            if (jumpCount > 0) {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            }
            else { // Second jump (airborne)
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity); // Higher boost for double jump
            }
            // Implement isGrounded() check for grounded jump vs midair jump
            //edge case when player's first jump is a midair jump


            jumpCount++;
            Debug.Log(jumpCount);
        }
    }

    public void JumpStop(InputAction.CallbackContext context) {
        if (context.canceled && rb.velocity.y > 0) {
            //rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
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
        //Less Floaty
        if (rb.velocity.y > 0) {
            rb.gravityScale = 1; // Less gravity while going up
        }
        else {
            rb.gravityScale = 2; // More gravity when falling
        }

        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        rb.velocity = new Vector2(inputVector.x * moveSpeed, rb.velocity.y);
        //rb.MovePosition(new Vector2(inputVector.x, inputVector.y));
        // too slippery
        //rb.AddForce(new Vector3(inputVector.x, 0 , inputVector.y) * moveSpeed, ForceMode2D.Force);   
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            jumpCount = 0;
        }
    }


}
