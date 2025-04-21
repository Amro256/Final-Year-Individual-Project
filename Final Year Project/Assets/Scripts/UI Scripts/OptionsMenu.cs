using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; //Namespace for the next input system
using UnityEngine.EventSystems;
public class OptionsMenu : MonoBehaviour //THis has to be done via code
{
    //Array to store the different tabs
    [SerializeField] GameObject[] tabs;
    [SerializeField] GameObject[] tabPanels;
    
    
    [SerializeField] int currentTab = 0;

    [SerializeField]InputActionAsset InputActions; //Reference to the InputAction asset
    private InputAction switchTabAction; //Will be used to find the exact action map with the triggers binded to them

    //private PlayerInput playerinput; //Reference to the playerinput
    
 
    private void Awake()
    {
        switchTabAction = InputActions.FindAction("TabNavigate");
    }
    private void OnEnable() 
    { 
        switchTabAction.Enable();
        switchTabAction.performed += TabSwitch;
    }

    void OnDisable()
    {  
       switchTabAction.performed -= TabSwitch;
       switchTabAction.Disable();
    }

    void TabSwitch(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>(); // Since the triggers are buttons, this has be read as a float otherwise Unity will throw an error
        Debug.Log("Tab Switch Input: " + input);

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
            tabs[i].SetActive(true); //Keeps all tabs visable. Selected colours can be changed in the inspector

            //Grab a reference to the UI button component

            var buttonTab = tabs[i].GetComponent<UnityEngine.UI.Button>();

            if(buttonTab != null)
            {
                if(i == currentTab)
                {
                    //call the selected tab below
                    //SelectedTab(buttonTab);

                    //Show the panel for the selected tab
                    tabPanels[i].SetActive(true);

                    //Set the selected tab by telling the event system which one it should be set to
                    EventSystem.current.SetSelectedGameObject(buttonTab.gameObject);

                    
                }
                else
                {
                    //unSelectedTab(buttonTab);

                    //Deactivate the others
                    tabPanels[i].SetActive(false);
                    
                }
            }
        }
    }


}
