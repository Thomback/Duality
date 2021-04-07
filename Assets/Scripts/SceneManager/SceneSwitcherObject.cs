using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcherObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Player")
        {
            Debug.Log("Try to load New Scene");
            GameObject.FindWithTag("GameController").GetComponent<SwitchToNextLevel>().LoadNextScene();
        }
    }
}
