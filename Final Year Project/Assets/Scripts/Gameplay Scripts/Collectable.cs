using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable: MonoBehaviour  //Parent class for collectable items
{
    public int value; //Value that all collectables will share 
    public float speed = 3f; //How fast the collectable will rotate

    //Method that other scripts can overwrite and implement their own behaviour
    public virtual void Collect()
    {
        Debug.Log("Collectable Items collected");
    }

    public virtual void CollectableRotation()
    {
        transform.Rotate(0,speed,0);
    }

}
