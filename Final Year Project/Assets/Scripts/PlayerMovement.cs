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
    
    //Variables for Movement
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpPower = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move"); //Find reference to the "Move" action
        jumpAction = playerInput.actions.FindAction("Jump"); //Reference to the Move Action
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        MovePlayer();
        PlayerJump();
        
    }

    void MovePlayer()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();
        rb.position += new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed * Time.deltaTime;
        
    }

    void PlayerJump()
    {
        if(jumpAction.IsPressed()) //if the jump button is pressed
        {
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }
}
