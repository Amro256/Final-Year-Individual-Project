using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PerspectiveChange : MonoBehaviour
{
    //Variables
    [SerializeField] CinemachineVirtualCamera mainCamTest; //Reference to the main 
    [SerializeField] CinemachineVirtualCamera sideCam; //Reference to the 2D virtual camera
    private bool is2D; //Bool to check if the player is in 2D or not

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            sideCam.Priority = 10;
            mainCamTest.Priority = 0;

            Debug.Log("Now in 2D!");
        }
    }
}
