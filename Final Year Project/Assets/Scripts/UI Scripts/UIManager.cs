using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{   
    //Singleton
    public static UIManager instance;

    //Variables 

    [SerializeField] private GameObject IntroPanel3D;

    //[SerializeField] private GameObject IntroPanel2d;

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

    void OnEnable()
    {
        AnimationEventHandler.animationFinish += ShowPopUp;
    }

   void ShowPopUp()
   {
        if(IntroPanel3D != null)
        {
            IntroPanel3D.SetActive(true);
            Debug.Log("3D panel shown!");
        }
   }
}
