using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle fullscreenToggle;
    bool isFullScreen;

    // Start is called before the first frame update
    void Start()
    {
        //Load in saved fullscreen setting
        isFullScreen = PlayerPrefs.GetInt("fullscreen",1) == 1;
        Screen.fullScreen = isFullScreen;
        fullscreenToggle.isOn = isFullScreen;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method to set the fullScreen toggle

    public void SetFullScreen()
    {   
        isFullScreen = fullscreenToggle.isOn;
        Screen.fullScreen = isFullScreen; 
    }

    public void SaveAndReturnToMain()
    {

        //Save the current settings

        //Save fullscreen
        PlayerPrefs.SetInt("fullscreen",Screen.fullScreen ? 1:0);

        //Save the preferences
        PlayerPrefs.Save();

        //Return back to Main Menu

        SceneManager.LoadScene("MainMenu");


    }
}
