using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Replay()
    {
        Debug.Log("Replay");
        SceneManager.LoadScene("GamePlay");
        SceneManager.LoadScene("Core", LoadSceneMode.Additive);
    }
    public void MainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
