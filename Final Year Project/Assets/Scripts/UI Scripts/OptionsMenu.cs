using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; //Namespace for the next input system

public class OptionsMenu : MonoBehaviour //THis has to be done via code
{
    //Array to store the different tabs
    [SerializeField] GameObject[] tabs;
    
    [SerializeField] int currentTab = 0;

    [SerializeField ]InputActionAsset InputActions; //Reference to the InputAction asset
    private InputAction switchTabAction; //Will be used to find the exact action map with the triggers binded to them
 
    private void Awake()
    {
        if(InputActions != null)
        {
            //Find the switch tab action within the asset
            var tabSwitchMap = InputActions.FindActionMap("Tab Switch");
            
            //Check if the tabSwitch is empty, if not, find the action for switching 
            if(tabSwitchMap != null)
            {
                switchTabAction = tabSwitchMap.FindAction("Tab Navigate");
            }

            //Check switch tab is found then make it sub to the tab switch event 
            if(switchTabAction != null)
            {
                //sub to event 
                switchTabAction.performed += TabSwitch;
            }
            else
            {
                Debug.Log("Error");
            }
        }
    }
    private void OnEnable() 
    {
       
    }

    void OnDisable()
    {
       
    }


    private void TabSwitch(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>(); // Since the triggers are buttons, this has be read as a float otherwise Unity will throw an error

        if(input < 0) 
        {
            Debug.Log(input);
            Debug.Log("Left Shoulder Button Pressed");
            currentTab--;
        }
        else if (input > 0)//Right Shoulder Button
        {
            Debug.Log(input);
            Debug.Log("Right Should Button Pressed");
            currentTab++;
        }

        //Manually Wrapping around 
         if (currentTab < 0) currentTab = tabs.Length - 1; // if the current tab is less than 0, loop back to the last tab
         if (currentTab >= tabs.Length) currentTab = 0; //If the current tab is greater than the length, loop back to the first tab

        UpdateTabs();

    }

    private void UpdateTabs()
    {
        for(int i = 0; i < tabs.Length; i++)
        {
            //tabs[i].SetActive(i  == currentTab); //This disables all the tabs which is not what I want

            //Grab a reference to the UI button component

            var buttonTab = tabs[i].GetComponent<UnityEngine.UI.Button>();

            if(buttonTab != null)
            {
                if(i == currentTab)
                {
                    //call the selected tab below
                    SelectedTab(buttonTab);
                }
                else
                {
                    unSelectedTab(buttonTab);
                }
            }
        }
    }

    //Method to handle selecting colours
    private void SelectedTab(UnityEngine.UI.Button tabButton) //This method will handle the colour when a tab is selected      
    {
        ColorBlock colours =  tabButton.colors;
        colours.normalColor = Color.green;

        //Change the colour of the tab when selected

        colours.selectedColor = Color.green;

        //Apply the colour to the tab

        tabButton.colors = colours;

        Debug.Log("Tab highlighted with selected color.");
    }


    //Method to handle unselected tabs
    private void unSelectedTab(UnityEngine.UI.Button tabButton)
    {
         ColorBlock colours =  tabButton.colors;
           colours.normalColor = Color.white;

        //Change the colour of the tab when selected

        colours.selectedColor = Color.white;

        //Apply the colour to the tab

        tabButton.colors = colours;
        


    Debug.Log("Tab reset to default color.");
    }

}
