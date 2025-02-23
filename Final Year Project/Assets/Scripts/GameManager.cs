using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Making this script a singleton
    private PlayerMovement playerMovement; //Private reference to the player movement script
    private PlayerInput playerinput;
    
    //variables
    [SerializeField] Animator transitionAnim; //Reference to the animator of the circle 
    [SerializeField] Animator modeTransitionAnim; // Refernece to the animator of the square
    [SerializeField] TMP_Text countDownText;
    [SerializeField] string sceneName; //Using a string to change scene so it's not hard coded and it'll be easier to switch while test
    [SerializeField] GameObject modeTransitionCanvas;
   
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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) //for testing purposes 
        {
            //Play animation & load scene in the background 
           StartCoroutine(LoadScene());
            
        }
    }

    public IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("isTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(sceneName);
        
    }

    //Use an enumerator to fake the transition
    public IEnumerator modeTransition()
    {
        modeTransitionCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        modeTransitionAnim.SetTrigger("IsTransitioning");
        
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
        Debug.Log("Player regained input!");
    
    }

    public void StartGame()
    {
        //Code here to load the level
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}
