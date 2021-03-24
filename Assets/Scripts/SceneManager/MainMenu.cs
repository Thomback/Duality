﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
        SceneManager.LoadScene(1, LoadSceneMode.Additive) ;


    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
