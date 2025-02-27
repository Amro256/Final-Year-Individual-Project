using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class PerspectiveChange : MonoBehaviour
{
    //Variables
    [SerializeField] CinemachineVirtualCamera mainCam; //Reference to the main 
    [SerializeField] CinemachineVirtualCamera cam2D; //Reference to the 2D virtual camera

    [SerializeField] public bool Switch2D = true;
    private bool is2D = false; //Bool to check if the player is in 2D or not
    private bool isTransitioning = false; //Bool to check if transitioning is taking place
    private Camera mainCamera; //Reference to the actual Unity camera
    PlayerMovement playerMovement; //Reference to the player movement script    


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); //On start, this script will find the 
        mainCamera = Camera.main; //Get the main camera reference
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.CompareTag("Player")&& !isTransitioning) //
        {
            if(Switch2D) //if the switch to 2D is true
            {
                Switchto2D();

                //Call Coroutine here
                //StartCoroutine(GameManager.instance.modeTransition());

                StartCoroutine(GameManager.instance.modeTransition());
            } 
            else //Switch back to 3D
            {
                Switchto3D();
                ///Call Coroutine here
                
             //StartCoroutine(GameManager.instance.modeTransition());
                
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
