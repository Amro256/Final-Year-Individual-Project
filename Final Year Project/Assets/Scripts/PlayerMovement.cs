using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem; //Namespace for the next iput system

public class PlayerMovement : MonoBehaviour
{

    // General Variables
    private PlayerInput playerInput;  // Reference to the player Input 
    private Rigidbody rb;
    Vector2 moveDirection;
    
    
    [Header("3D Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpPower = 5f;

    [Header("2D Movement")]
    [SerializeField] float moveSpeed2D;
    [SerializeField] float jumpPower2D;

    [Header("Ground Checks")]
    [SerializeField] bool isGrounded;
    [SerializeField] private LayerMask groundLayer;

    [Header("Raycast GameObject")]
    [SerializeField] GameObject rayPostion;

    [SerializeField] private bool is2D = false; //Bool to check if the player is in 2D or not

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
        moveTest();
        
    }

    public void OnMove (InputAction.CallbackContext context) //Changed to using the Invoke Unity behaviour, so this method will need to take in a CallBackContext to trigger this method
    {
        //Using "Invoke" Unity events does not take continuous hold into account

        moveDirection = context.ReadValue<Vector2>(); //Works but it need to be called every frame to update movement 
    
    }

    private void moveTest()
    {
        if(is2D)
        {
            //Restrict Movement
            rb.position += new Vector3(moveDirection.x, 0,0) * moveSpeed * Time.deltaTime; //Restrict controls to the X axis only
            Debug.Log($"Controls restirct to 2D");
        }
        else
        {
            //3D controls
            rb.position += new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed * Time.deltaTime;
            Debug.Log("Full 3D controls!");
        }
    }


    public void OnJump(InputAction.CallbackContext context)
    {

        if(context.performed && isGrounded) // Will only jump is the player presses the jump button AND if the player is grounded
        {
            rb.AddForce(new Vector3(0,jumpPower,0 ), ForceMode.Impulse);
            isGrounded = false;
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
    
}
