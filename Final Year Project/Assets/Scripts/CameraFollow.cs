using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; //Reference to the player's transform 
    [SerializeField] private float camFollowSpeed = 3f; //The speed at which the camera will follow the player
    [SerializeField] private float CameraOffSet = 5f;
    

    [SerializeField] CinemachineVirtualCamera virtualCam;

    private CinemachineTransposer transposer;

    void Start()
    {
        transposer = virtualCam.GetCinemachineComponent<CinemachineTransposer>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the player is falling by checking the y velocity

        if(player.GetComponent<Rigidbody>().velocity.y < 0) //Check to see if the player is falling
        {
            Debug.Log("Player is falling");

            Vector3 test = transposer.m_FollowOffset;
            test.y = player.position.y - CameraOffSet;

            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset,test, camFollowSpeed * Time.deltaTime);
            //Vector3 targetPosition = new Vector3(player.position.x, player.position.y + CameraOffSet, transform.position.z);

            //Move the player to the player's posiiton

            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, camFollowSpeed * Time.deltaTime);
        }
    }
}
