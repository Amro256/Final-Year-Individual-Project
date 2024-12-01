using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PerspectiveChange : MonoBehaviour
{
    //Variables
    [SerializeField] CinemachineVirtualCamera mainCam; //Reference to the main 
    [SerializeField] CinemachineVirtualCamera cam2D; //Reference to the 2D virtual camera
    private bool is2D; //Bool to check if the player is in 2D or not
    [SerializeField] public bool Switch2D = true;

    PlayerMovement playerMovement; //Reference to the player movement script

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); //On start, this script will find the 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player")) //
        {
            if(Switch2D) //if the switch to 2D is true
            {
                //Call the switch to 2D method 
                Switchto2D();
            } 
            else
            {
                Switchto3D();
            }
        }
    }


    //Method that will switch to 2D
    void Switchto2D()
    {
        cam2D.Priority = 10;
        mainCam.Priority = 0;
        is2D = true;
        playerMovement.Switchto2DMode();
        Debug.Log("You are now in 2D!");
    }


    //Method to switch to 3D
    void Switchto3D()
    {
        mainCam.Priority = 10;
        cam2D.Priority = 0;
        is2D = false;
        playerMovement.SwitchBackTo3D();
        Debug.Log("You are now back in 3D!");
    }
}
