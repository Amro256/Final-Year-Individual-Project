using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Namespace for the next input system

public class OptionsMenu : MonoBehaviour //THis has to be done via code
{
    private InputAction tabSwitchAction; //Reference to the Tab Switch Action
    
    private void OnEnable() 
    {
        //sub to the TabSwitch function
        tabSwitchAction.performed += TabSwitch;
        //Enable the Tab Switch Action 
        tabSwitchAction.Enable();
    }

    void OnDisable()
    {
        //unsub from the TabSwitch function
        tabSwitchAction.performed -= TabSwitch;
        //Disable the tab Switch Action
        tabSwitchAction.Disable();
    }


    private void TabSwitch(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>(); // Since the triggers are buttons, this has be read as a float otherwise Unity will throw an error

        if(input > 0) 
        {
            Debug.Log(input);
        }

    }
}
