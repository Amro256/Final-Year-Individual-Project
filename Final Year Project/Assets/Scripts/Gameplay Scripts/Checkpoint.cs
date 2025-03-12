using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Game Manager code here
            GameManager.instance.UpdatePostion(transform.position);
            Debug.Log("Checkpoint activated!");
        }
    }
}
