using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    private Animator anim;
    private string sceneToLoad;
    private bool additive;
    void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        AudioListener.volume = 1;
    }

    public void FadeToScene(string sceneName)
    {
        sceneToLoad = sceneName;
        additive = false;
        StartCoroutine(fadeSound());
        anim.SetBool("fadeOut", true);
    }

    public void FadeToLevel(string levelName)
    {
        sceneToLoad = levelName;
        additive = true;
        StartCoroutine(fadeSound());
        anim.SetBool("fadeOut", true);
    }

    private void FadeComplete()
    {
        if (additive)
        {
            SceneManager.LoadScene(1);
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private IEnumerator fadeSound()
    {
        float currentTime = 0;
        float start = AudioListener.volume;

        while (currentTime < 0.5f)
        {
            currentTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(start, 0, currentTime / 0.5f);
            yield return null;
        }
        yield break;
    }
}
