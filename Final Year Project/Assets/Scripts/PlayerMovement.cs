using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEditor.Build.Content;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem; //Namespace for the next input system

public class PlayerMovement : MonoBehaviour
{

    // General Variables
    private PlayerInput playerInput;  // Reference to the player Input 
    private Rigidbody rb;
    Vector2 moveDirection;
    Vector3 movement;

    
     [Header("Camera ")]
    [SerializeField] Transform cam;


    [Header("Gravity Scale Settings")]
    [SerializeField] float defaultGravityScale = 1f; //Default gravity when the player is not moving
    [SerializeField] float fallingGravityScale = 3.1f; //Gravity when the player is falling after a jump, for a more snappier jump
    
    [Header("3D Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float sprintSpeed = 20f;

    [Header("2D Movement")]
    [SerializeField] float moveSpeed2D;
    [SerializeField] float jumpPower2D;
    // private Quaternion  test = Quaternion.Euler(0f,90f,0f);
    // private Quaternion test2 = Quaternion.Euler(0f,-90f,0);
    private float roationSpeed = 5f;

    [Header("Ground Checks")]
    [SerializeField] bool isGrounded;
    [SerializeField] private LayerMask groundLayer;

    [Header("Raycast GameObject")]
    [SerializeField] GameObject rayPostion;

    

    [SerializeField] private bool is2D = false; //Bool to check if the player is in 2D or not
    [SerializeField] bool isSprinting = false;

    
    

    void Awake() //Happens just before start
    {
         rb = GetComponent<Rigidbody>();
         playerInput = GetComponent<PlayerInput>();
         playerInput.actions.Enable(); //Enables the controls
    }

    // Start is called before the first frame update
    void Start()
    {  
        
    }

    void OnDisable() 
    {
         playerInput.actions.Disable(); //Will disable the controls is the gameObejct is disabled
    }

    
    // Update is called once per frame
    void Update()
    {
        CheckGround(); //Call the CheckGround method to keep firing raycast
       
    }

    private void FixedUpdate() 
    {
        //rb.position += new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed * Time.deltaTime;
        playerMovement();
        ApplyGravity();
        
        
    }

    public void OnMove (InputAction.CallbackContext context) //Changed to using the Invoke Unity behaviour, so this method will need to take in a CallBackContext to trigger this method
    {
        //Using "Invoke" Unity events does not take continuous hold into account
        moveDirection = context.ReadValue<Vector2>(); //Works but it need to be called every frame to update movement 
    }

    private void playerMovement()
    {
        //ternary conditional operator to check if the player is currently sprinting 
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        if(is2D)
        {
            //Restrict Movement
            movement = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, 0); //Restrict controls to the X axis only
            Debug.Log("Controls restirct to 2D");
            Smooth2DRot();
        }
        else
        {
            //Camera direction
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();


            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            //Camera relative direction 
            Vector3 moveDirection3D = (cameraForward * moveDirection.y + cameraRight * moveDirection.x).normalized;

            //3D controls
            movement = new Vector3(moveDirection3D.x * currentSpeed, rb.velocity.y, moveDirection3D.z * currentSpeed); 
            Debug.Log("Full 3D controls!");

            
            //Rotate the player based on movement direction - also just good for visualisation
            if(moveDirection3D.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection3D);
            }
        }

        rb.velocity = movement;
        
    }


    public void OnJump(InputAction.CallbackContext context)
    {

        if(context.performed && isGrounded) // Will only jump is the player presses the jump button AND if the player is grounded
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); //Adds a force on the y axis for jumping 
            isGrounded = false; //Sets the bool to false as the player is no longer grounded 

        }

        //Variable Jump Height
        if(context.canceled && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f, rb.velocity.z);
        }
   
    }

    public void OnSprint(InputAction.CallbackContext context) //When creating new actions, make sure to manaully assign it in the inspector because Unity might not automatically do it
    {
        if(context.started && isGrounded) //Check to see if the shift (sprint input ) button has been pressed while the player is grounded, allow the player to sprint 
        {
            Debug.Log("Player is sprinting");
            isSprinting = true;
        } 

        if(context.canceled) //Check to see if sprint has stopped by checking if the key is not being held down
        {
            Debug.Log("Player is no longer sprinting");
            isSprinting = false;
        }
    }

    //Method that fires of a raycast to check if the player is colliding with the Ground layer mask
     void CheckGround() 
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(rayPostion.transform.position, Vector3.down, out hit, 0.5f, groundLayer);

        Debug.DrawRay(rayPostion.transform.position, Vector3.down, Color.red, 0.5f);
        Debug.Log ("Ray fired and hit ground");

    }

    public void Switchto2DMode()
    {
        //Call the bool
        is2D = true;
    }

    public void SwitchBackTo3D()
    {
        is2D = false;
    }

    public void ApplyGravity() //Method that will check the player's gravity on the ground and when jumping 
    {
        if(!isGrounded) //if the player is not grounded, apply a custom gravity scale for a snappier jump
        {
            if(rb.velocity.y < 0)
            {
                rb.AddForce(Physics.gravity * (fallingGravityScale -1), ForceMode.Acceleration);
            }
        
        }
    }

    private void Smooth2DRot()
    {
        Quaternion targetRotation = transform.rotation;

        if(movement.x > 0)
        {
            targetRotation = Quaternion.Euler(0f, 90f,0f); //Facing right 
        }
        else if(movement.x < 0) //Facing left
        {
            targetRotation = Quaternion.Euler(0f,-90f,0f);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.time * roationSpeed);
    }
    
}
