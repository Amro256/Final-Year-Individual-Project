using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Namespace for the next input system

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject startScreenCanvas; //Reference to the start screen canvas
    [SerializeField] GameObject mainMenuCanvas; //Referennce to the Main Menu Canvas

    private PlayerInput playerInput;  // Reference to the player Input 
    private InputAction titleScreenAction; //Refernece to the title screen action map - which deals with the button input 
    private bool HasPresseButton = false; //Set this bool to false at the start

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        titleScreenAction = playerInput.actions["Title Screen"];
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //Start screen should be enabled on start with the Main Menu will be active once the user has pressed the touchpad
        startScreenCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    // Update is called once per frame
   

    void OnEnable() 
    {
        titleScreenAction.performed += TitleScreenInput;
        titleScreenAction.Enable();
    }

    void OnDisable()
    {
        titleScreenAction.performed -= TitleScreenInput;
        titleScreenAction.Disable();
    }

    private void TitleScreenInput (InputAction.CallbackContext context)
    {
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        //Enable the Main Menu and disable the Start screen
        startScreenCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        //StartCoroutine(delayMainMenu());
    }

    // private IEnumerator delayMainMenu()
    // {
    //     yield return new WaitForSeconds(1.5f);
    //     mainMenuCanvas.SetActive(true);
    // }
}
