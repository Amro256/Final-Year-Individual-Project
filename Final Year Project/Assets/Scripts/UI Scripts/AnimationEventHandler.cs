using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationEventHandler : MonoBehaviour
{
    private PlayerMovement playercontrols;
    private PlayerInput playerinput;
    // Start is called before the first frame update
    void Start()
    {
        playercontrols = FindObjectOfType<PlayerMovement>();
        playerinput = FindObjectOfType<PlayerInput>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onAnimationPlay()
    {
        playercontrols.enabled = false;
        playerinput.enabled = false;
    }

    private void OnAnimationFinish()
    {
        playercontrols.enabled = true;
        playerinput.enabled = true;
    }
}
