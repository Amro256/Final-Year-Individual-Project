using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    //Variables
    [SerializeField] float SpringForce = 3f; //Force of the spring
    private Rigidbody playerRigidBody;


    void Start()
    {
        playerRigidBody = FindObjectOfType<Rigidbody>(); // Will find a component with a Rigidbody component
    }

    
    void OnCollisionEnter(Collision collision)
    {   
        //Check if the thing being collided with is the player
        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("Detected Player");
            playerRigidBody.AddForce(Vector3.up * SpringForce, ForceMode.Impulse);
        }

    }
}
