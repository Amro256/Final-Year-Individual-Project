using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    //Variables

    [SerializeField] float SpringForce = 3f; //Force of the spring
    [SerializeField] Rigidbody playerRigidBody;


    void Start()
    {
        playerRigidBody = FindFirstObjectByType<Rigidbody>(); // Will find a component with a Rigidbody component
    }

    
    void OnTriggerEnter(Collider other)
    {   
        //Check if the thing being collided with is the player
        if(other.CompareTag("Player"))
        {
            //Debug.Log("Detected Player");
            playerRigidBody.AddForce(Vector3.up * SpringForce, ForceMode.Impulse);
        }

    }
}
