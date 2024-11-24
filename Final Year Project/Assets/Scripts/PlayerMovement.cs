using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Namespace for the next iput system
public class PlayerMovement : MonoBehaviour
{
    //Variables to store Actions / Player Input referneces
    InputAction moveAction;
    InputAction jumpAction;
    PlayerInput playerInput; 
    private LayerMask groundmask;
    [SerializeField] bool isGrounded = false;

    //Variables for Movement
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpPower = 5f;

    void OnEnable()
    {
        playerInput.actions.Enable(); //Will enable the controls if the gameObject is enable 
    }

    void OnDisable() 
    {
         playerInput.actions.Disable(); //Will disable the controls is the gameObejct is disabled
    }


    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move"); //Find reference to the "Move" action
        jumpAction = playerInput.actions.FindAction("Jump"); //Reference to the Move Action
        groundmask = LayerMask.GetMask("Ground"); //For the raycast to detect the correct layer mask for player jumping
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        MovePlayer();
        CheckGround();
        PlayerJump();
    }

    

    void MovePlayer()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();
        rb.position += new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed * Time.deltaTime;
    }

    void PlayerJump()
    {
        if(jumpAction.IsPressed() ) //if the jump button is pressed
        {
            //isGrounded = false;
            Debug.Log("Jump!");
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);   
            
        } 
            
    }

     void CheckGround()
    {
        
        RaycastHit hit;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundmask);

        Debug.DrawRay(rb.position, Vector3.down, Color.red, 3f);
        Debug.Log ("Ray fired and hit ground");

    }
    
}
