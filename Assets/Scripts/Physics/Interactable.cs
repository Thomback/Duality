using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*  ---> Allow to create an action via Unity Events system <--- */ 

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    void Update() {
        if(isInRange){
            if(Input.GetButton("Action")){
                interactAction.Invoke(); //Fire Event
            }
        }    
    }
    
    

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            isInRange = true;
            Debug.Log("Player is now in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        isInRange = false;
        Debug.Log("Player now is not in range");
    }
}
