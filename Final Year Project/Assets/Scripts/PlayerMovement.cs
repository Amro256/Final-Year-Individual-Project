using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Namespace for the next iput system
public class PlayerMovement : MonoBehaviour
{

    private PlayerInput playerInput;  //Reference to the player Input 


    //Variables for Movement
    [SerializeField] private Rigidbody rb;
    [SerializeField] float moveSpeed = 5f;
    Vector2 moveDirection;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] bool isGrounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] GameObject castPostion;


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
        rb.position += new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed * Time.deltaTime;
        
    }

    public void OnMove (InputAction.CallbackContext context) //Changed to using the Invoke Unity behaviour, so this method will need to take in a CallBackContext to trigger this method
    {
        //Using "Invoke" Unity events does not take continuous hold into account

        moveDirection = context.ReadValue<Vector2>(); //Works but it need to be called every frame to update movement 

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

        isGrounded = Physics.Raycast(castPostion.transform.position, Vector3.down, out hit, 0.5f, groundLayer);

        Debug.DrawRay(castPostion.transform.position, Vector3.down, Color.red, 0.5f);
        Debug.Log ("Ray fired and hit ground");

    }
    
}
