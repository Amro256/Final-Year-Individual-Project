using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance; //Singleton

    [SerializeField] private Toggle fullscreenToggle;
    bool isFullScreen;

    //Audio & Slider References

    [Header("Audio")]
    [SerializeField] private AudioMixer aMixer;
    [SerializeField] private Slider masterVolSlider;
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private Slider sfxVolSlider;

    private void Awake()
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
        //Load in saved fullscreen setting
        isFullScreen = PlayerPrefs.GetInt("fullscreen",1) == 1;
        Screen.fullScreen = isFullScreen;
        fullscreenToggle.isOn = isFullScreen;

        //Load Audio Data on start
        LoadAudioData();
    }

    void OnEnable()
    {
        //Call the load data method
        LoadAudioData();
    }

    //Method to set the fullScreen toggle
    public void SetFullScreen()
    {   
        isFullScreen = fullscreenToggle.isOn;
        Screen.fullScreen = isFullScreen; 
        //Save fullscreen
        PlayerPrefs.SetInt("fullscreen",Screen.fullScreen ? 1:0);
        PlayerPrefs.Save();
    }

    //Methods for the sliders
    public void SetMasterVolume()
    {
        float volume = masterVolSlider.value;
        aMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20); //Converts slider value/scale to a logarithmic scale
        //Set this value using the player prefs
        PlayerPrefs.SetFloat("MasterVolume",volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume()
    {   
        float volume = musicVolSlider.value;
        aMixer.SetFloat("MusicVolume",Mathf.Log10(volume) * 20);
        //Set this value using the player prefs
        PlayerPrefs.SetFloat("MusicVolume",volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume()
    {   
        float volume = sfxVolSlider.value;
        aMixer.SetFloat("SFXVolume",Mathf.Log10(volume) * 20);
        //Set this value using the player prefs
        PlayerPrefs.SetFloat("SFXVolume",volume);
        PlayerPrefs.Save();
    }


    public void LoadAudioData() //Will load the saved values
    {
        masterVolSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        musicVolSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxVolSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        aMixer.SetFloat("MasterVolume", masterVolSlider.value);
        aMixer.SetFloat("MusicVolume", musicVolSlider.value);
        aMixer.SetFloat("SFXVolume", sfxVolSlider.value);

        // aMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolSlider.value) * 20);
        // aMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolSlider.value) * 20);
        // aMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolSlider.value) * 20);
        
    }


    public void SaveAndReturnToMain()
    {

        //Save the current settings
        PlayerPrefs.SetInt("fullscreen",Screen.fullScreen ? 1:0);

        //Save all preferences, including Audio
        PlayerPrefs.Save();

        //Return back to Main Menu

        SceneManager.LoadScene("MainMenu");


    }
}
