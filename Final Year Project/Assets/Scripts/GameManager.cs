using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Making this script a singleton

    //variables
    [SerializeField] Animator transitionAnim; //Reference to the animation
    [SerializeField] string sceneName; //Using a string to change scene so it's not hard coded and it'll be easier to switch while test
   
    void Awake() 
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

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("isTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(sceneName);
        
    }
}
