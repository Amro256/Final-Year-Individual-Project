using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class AnimationEventHandler : MonoBehaviour
{
    private PlayerMovement playercontrols;
    private PlayerInput playerinput;

    public static event Action animationPlay;
    public static event Action animationFinish;
    // Start is called before the first frame update
    void Start()
    {
        if (playercontrols == null)
        {   
            playercontrols = FindObjectOfType<PlayerMovement>();   
        } 
        if(playerinput == null)
        {
            playerinput = FindObjectOfType<PlayerInput>();
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimationStart()
    {
        playercontrols.enabled = false;
        playerinput.enabled = false;

        animationPlay?.Invoke();
    }

    private void OnAnimationFinish()
    {
        playercontrols.enabled = true;
        playerinput.enabled = true;

        animationFinish?.Invoke();
    }
}
