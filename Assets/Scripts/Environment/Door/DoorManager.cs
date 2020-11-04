using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    Animator anim;
    

    public string nameSceneLeft;
    public string nameSceneRight;


    private bool wasLeft = false;
    private bool readyToLeave = false;

    public void unloadOldRoom(MeIsDoor doorObject)
    {
        if (readyToLeave)
        {
            Debug.Log("Unloading old Scene : " + nameSceneLeft);
            if (SceneManager.GetSceneByName(nameSceneLeft).isLoaded)
                SceneManager.UnloadSceneAsync(nameSceneLeft);
            doorObject.isOpenable = false;
        }
    }

    public void justLeftLeft()
    {
        if (!wasLeft)
        {
            readyToLeave = false;
        }
        wasLeft = true;
    }

    public void justLeftRight()
    {
        if (wasLeft)
        {
            readyToLeave = true;
        }
        wasLeft = false;
    }

    public void justLeftEntrance()
    {
        Debug.Log("Loading next Scene : " + nameSceneRight);
        if (!SceneManager.GetSceneByName(nameSceneRight).isLoaded)
            SceneManager.LoadScene(nameSceneRight, LoadSceneMode.Additive);
    }
}
