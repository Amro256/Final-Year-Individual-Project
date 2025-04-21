using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //This must use the eventsystem namespace to work

public class PreventDeselection : MonoBehaviour //The purpose of this script to to ensure the controler does not lose focus on a UI element when, for example, the user clicks outside a selectable element using the mouse
{
    //Information on this was found on a GameDeveloper article. If the user clicks off, the event system will remember what the last selected element was and forces the system to rest it
    EventSystem eventsys; //Reference to the events system
    private GameObject currentSelection; //Private gameobject to track the current selected UI element
    private void Start()
    {
        eventsys = EventSystem.current; //Sets the event system to the current selected object / element
    }

    private void Update()
    {
        if(eventsys.currentSelectedGameObject != null && eventsys.currentSelectedGameObject != currentSelection) 
        {
            currentSelection = eventsys.currentSelectedGameObject;
        }
        else if(currentSelection != null && eventsys.currentSelectedGameObject == null)
        {
            eventsys.SetSelectedGameObject(currentSelection); 
        } 
    }
}
