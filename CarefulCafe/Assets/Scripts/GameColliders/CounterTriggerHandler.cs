using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterTriggerHandler : MonoBehaviour
{
    private bool isPlayerInside = false;

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            Debug.Log("enter");
            isPlayerInside = true;
        }
    } 

    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exit");
            isPlayerInside = false;
        }
    }

    public bool IsPlayerInside(){
        return isPlayerInside;
    }
}
