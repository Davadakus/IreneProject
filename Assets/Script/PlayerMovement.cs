using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    
    // public Keyboard keyboard;
    private Rigidbody2D rb;
    private PlayerControls playerControls;

    public int jumpCount = 0;
    public int maxJump = 2;
    public int moveSpeed = 5;
    public float jumpSpeed = 3f; // Higher value == Higher Jump
    public float gravity;
    public bool glide = false;
    public float glidingSpeed = -1f;
    public Vector2 boxSize = new Vector2(1, 0.23f);

    public LayerMask groundLayer;  // Assign this in the Inspector but try to assign this automatically
    public float groundCheck = 0.5f; // Small radius for checking

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
        gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        groundLayer = LayerMask.GetMask("Ground");

        // PlayerControls is the name of the set of control you set 
        playerControls.Player.Enable();
        
        // In this case: Player is the action map; Jump is the Actions 
        // += in C# 'subscribes' the function with the event so multiple functions can be subscribed to the event
        playerControls.Player.Jump.performed += Jump;
        playerControls.Player.Jump.canceled += Jump;
        playerControls.Player.Glide.performed += Glide;
        playerControls.Player.Glide.canceled += Glide;
        playerControls.Player.Movement.canceled+= MoveStop;
    }

    public void Glide(InputAction.CallbackContext context) {
        Debug.Log("Glide: " + context.phase);
        if (context.performed) {
            glide = true;
            //rb.gravityScale = 0.5f;
        }
        else if (context.canceled) {
            glide = false;
            //rb.gravityScale = 2;
        }
    }

    public void Jump(InputAction.CallbackContext context) {
        // Context tells you all the details of the input data
        // Only one instance when button is pressed (Not when let go or started)


        Debug.Log("Jump " + context.phase);
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

            if (jumpCount == 0 && !IsGrounded()) {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                jumpCount = 2;
                //Debug.Log("Second Jump");
            }
            else {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                jumpCount++;
                //Debug.Log("First Jump");

            }
        }

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
        else if (glide) {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, glidingSpeed); // Reduce fall speed

        }
        else {
            rb.gravityScale = 2; // More gravity when falling
        }

        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        rb.velocity = new Vector2(inputVector.x * moveSpeed, rb.velocity.y);

        if (IsGrounded()) {
            jumpCount = 0; // Reset jump count when on ground
        }
        //Debug.Log(rb.velocity.y);
        //rb.MovePosition(new Vector2(inputVector.x, inputVector.y));
        // too slippery
        //rb.AddForce(new Vector3(inputVector.x, 0 , inputVector.y) * moveSpeed, ForceMode2D.Force);   
    }

    void Update() {
        /*
         * Testing raycasting 
         */

        //Debug.DrawRay(transform.position, Vector2.down * groundCheck, Color.red);
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheck, groundLayer);
        //if (hit.collider != null) {
        //    Debug.Log("Hit Ground");
        //}
    }

    //private void OnCollisionEnter2D(Collision2D other) {
    //    if (other.gameObject.CompareTag("Ground")) {
    //        jumpCount = 0;
    //    }
    //}

    public bool IsGrounded() {
        //Debug.Log("Grounded");
        //return Physics2D.Raycast(transform.position, Vector2.down, groundCheck, groundLayer);
        return Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, groundCheck, groundLayer);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, Vector2.down * groundCheck);
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheck, boxSize);
    }
}
