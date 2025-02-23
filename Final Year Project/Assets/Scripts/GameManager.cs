using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Making this script a singleton
    
    //variables
    [SerializeField] Animator transitionAnim; //Reference to the animator of the circle 
    [SerializeField] Animator modeTransitionAnim; // Refernece to the animator of the square
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
