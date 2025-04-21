using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Events;

public class PerspectiveChange : MonoBehaviour
{
    //Variables
    [SerializeField] CinemachineFreeLook mainCam; //Reference to the main 3D cam
    [SerializeField] CinemachineVirtualCamera cam2D; //Reference to the 2D virtual camera

    //Unity event that will allow the transition type to be selected in the inspector
     [Header("Transition Selection")]
    [SerializeField] private UnityEvent transitionTrigger;
    

    [SerializeField] public bool Switch2D = true; //Bool that controls whether the trigger will transition to 2D or not
    private bool is2D = false; //Bool to check if the player is in 2D or not
    private bool isTransitioning = false; //Bool to check if the player is current transitioning
    private Camera mainCamera; //Reference to the actual Unity camera
    PlayerMovement playerMovement; //Reference to the player movement script
    [SerializeField] private AudioClip SwitchSFX;    


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); //On start, this script will find the gameobject with the player movement script attached to it
        mainCamera = Camera.main; //Get the main camera reference
    }

    //OnTriggerEnter for dimension swaps
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player")&& !isTransitioning) //
        {
            isTransitioning = true;

            if(Switch2D) //if the switch to 2D is true
            {
                Switchto2D();

                transitionTrigger.Invoke(); // Now using unity events, you can select the transition type by finding the transtion point (under transition objects) and set the Coroutine

                // No Coroutine selected = Transition 1 - Smooth Camera
                // StartBlackScreenAnim (method) = Transition 2 - Black Screen
                // StartCountdownAnim (method) = Transition 3 - Countdown


                //-------------------------Old Method(Hard coded)-----------------------------------------------------
                //This is the the transition type is set. 
                //StartCoroutine(GameManager.instance.modeTransition()); //This is hard-coded way to change the transitions, use Unity events so it can be selected in the inspector

            } 
            else //Switch back to 3D
            {
                Switchto3D();
                ///Call Coroutine here //
                
                 transitionTrigger.Invoke(); // Now using unity events, you can select the transition type by finding the transtion point (under transition objects) and set the Coroutine

                // No Coroutine selected = Transition 1 - Smooth Camera
                // StartBlackScreenAnim (method) = Transition 2 - Black Screen
                // StartCountdownAnim (method) = Transition 3 - Countdown


                //-------------------------Old Method(Hard coded)-----------------------------------------------------

                //This is the the transition type is set.
                //StartCoroutine(GameManager.instance.modeTransition());
            }

            AudioManager.instance.playSFX(SwitchSFX, transform, 0.35f);
        }
    }

    //----------------------------------------------------------------------------------- 2D / 3D Methods ------------------------------------------------------------//
    //Method that will switch to 2D
    void Switchto2D()
    {
        cam2D.Priority = 10;
        mainCam.Priority = 0;
        is2D = true;
        playerMovement.Switchto2DMode();
        //Debug.Log("You are now in 2D!");
    }

    //Method to switch to 3D
    void Switchto3D()
    {
        mainCam.Priority = 10;
        cam2D.Priority = 0;
        is2D = false;
        playerMovement.SwitchBackTo3D();
        //Debug.Log("You are now back in 3D!");
    }
}
