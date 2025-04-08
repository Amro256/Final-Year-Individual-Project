using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable // The coin script is inheriting behaviour from the Collectable parent class
{

    [SerializeField] private AudioClip CoinSFX;

    void Update()
    {
        base.CollectableRotation();
    }
    //Checks if the collision is with the player and then call the collect function
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Collect(); 
            Destroy(gameObject); //And destory the game object afterwards
        }    
    }
    public override void Collect()
    {
        base.Collect(); //Implements the code in the colelctable scripts - can be used for debugging for now
        AudioManager.instance.playSFX(CoinSFX, transform, 0.5f);
        Debug.Log("Coin Collected! Counter: " + value);
    }


    
}
