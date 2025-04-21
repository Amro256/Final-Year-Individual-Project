using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Making this script a singleton

    //Private Variables
    private PlayerMovement playerMovement; //Private reference to the player movement script
    private PlayerInput playerinput;
    private InputAction pausing;
    private Vector3 lastcheckpoint;
    
    //Variables
    [Header("Transitions")]
    [SerializeField] Animator transitionAnim; //Reference to the circle animator  
    [SerializeField] Animator BlackScreenTransitionAnim; // Refernece to the animator of the square
    [SerializeField] private TMP_Text countDownText;

    [Header("Loading Scenes")]
    [SerializeField] string sceneName; //Using a string to change scene so it's not hard coded and it'll be easier to switch while testing


    [Header("UI Canvases")]
    [SerializeField] GameObject modeTransitionCanvas;
    [SerializeField] GameObject pauseMenuPanel;
    
    bool ispaused = false; //Bool to check if the game is paused
   
    void Awake() //Checks if there is another Game Manager Instance in the scene
    {
        if(instance == null)
        {
            instance = this;
        }    
        else 
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }

   
    // Start is called before the first frame update
    void Start()
    {
       modeTransitionCanvas.SetActive(false);
       playerMovement = FindObjectOfType<PlayerMovement>(); //Game will search for an object/component that has the player movement script attached to it
       playerinput = FindObjectOfType<PlayerInput>();
       countDownText.enabled = false;
       pauseMenuPanel.SetActive(false);

        playerinput.actions.FindActionMap("Pause").Enable();

        playerinput.actions["Escape"].performed += ctx => PauseGame();

    }

    public void UpdatePostion(Vector3 position)
    {
        lastcheckpoint = position;
    }

    public Vector3 GetLastCheckpoint()
    {
        return lastcheckpoint;
    }

    public IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("isTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(sceneName);
        
    }

    //------------------------------------------------TRANSITIONS FUNCTIONS------------------------------------------------------------------------------//

    //Use an enumerator to fake a screen transition
    public IEnumerator BlackScreenTransition()
    {
        modeTransitionCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        BlackScreenTransitionAnim.SetTrigger("IsTransitioning");
        yield return new WaitForSeconds(3f);
        modeTransitionCanvas.SetActive(false);   
    }

    public void StartBlackScreenAnim() //public method so the transition is selectable in the inspector 
    {
        StartCoroutine(BlackScreenTransition());
    }

    public IEnumerator CountdownTransition()
    {
        playerMovement.enabled = false;
        playerinput.enabled = false;
        countDownText.enabled = true;
        Debug.Log("Player Input taken away");

        //for loop that will countdown to 0
        for(int i = 3; i >= 0; i--) // "--" is using as the coutndown will be decrementing
        {
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countDownText.text = "";
        playerMovement.enabled = true;
        playerinput.enabled = true;
        //Debug.Log("Player regained input!"); - Used for testing
    }

     public void StartCountdownAnim() //public method so the transition is selectable in the inspector 
    {
        StartCoroutine(CountdownTransition());
    }

    //------------------------------------------------PAUSE MENU FUNCTIONS------------------------------------------------------------------------------//

    public void StartGame()
    {
        StartCoroutine(LoadScene()); // This works but will need some adjuments

        //Disable a main menu panel here
    }

        public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void  PauseGame()
    {
        ispaused = !ispaused;

        if(ispaused)
        {
            Time.timeScale = 0f;
            pauseMenuPanel.SetActive(true);
            playerinput.SwitchCurrentActionMap("UI"); //Switch the UI action map - gives players the actions for UI navigation        
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuPanel.SetActive(false);
            playerinput.SwitchCurrentActionMap("Player"); //Switch back to the player action map - gives player regular controls
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}
