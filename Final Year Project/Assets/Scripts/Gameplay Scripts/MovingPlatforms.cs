using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    //Variables
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;

    [SerializeField] float platformSpeed = 1.5f; //Platform move speed

    private Transform currentTarget; //To store the platform position
    void Start()
    {
        //Set the platform current position to the first point on start
        currentTarget = point1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
         transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, platformSpeed * Time.deltaTime); 
         

         if(Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
         {
            if(currentTarget == point1)
            {
                currentTarget = point2;
            }
            else if(currentTarget == point2)
            {
                currentTarget = point1;
            }
         }
    }
}
