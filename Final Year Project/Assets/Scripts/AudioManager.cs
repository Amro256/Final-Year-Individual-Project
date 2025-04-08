using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource soundSFX;

    //Make this a singleton so it can be accessed by other scripts

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void playSFX(AudioClip aclip, Transform spawnPoint, float volume)
    {
        AudioSource adsource = Instantiate(soundSFX, spawnPoint.position, Quaternion.identity);

        adsource.clip = aclip;

        adsource.volume = volume;

        adsource.Play();

        float sfxLength = adsource.clip.length;

        Destroy(adsource.gameObject, sfxLength);
    }
}
