using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTarget : MonoBehaviour
{
    //Variables
    [SerializeField] Transform camTargetPosition; 
    [SerializeField] Vector3 TargetRotation;
    

    public Vector3 GetCamPosition() //Returns a Vector 3
    {
        return camTargetPosition.position;
    }


    public Vector3 GetCamRotation() //Returns a Vector 3
    {
        return TargetRotation;
    }
}
