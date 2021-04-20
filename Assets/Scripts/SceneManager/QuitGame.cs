using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void QuitTheGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
