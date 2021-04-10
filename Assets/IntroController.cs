using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public Animator animFamily;
    public Animator animMissing;
    public Animator animHeritage;
    public Animator animFiesta;

    private int avancementHistoire = 0;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            avancementHistoire++;
            switch (avancementHistoire)
            {
                case 1:
                    animFamily.Play("Family");
                    break;
                case 2:
                    animMissing.Play("Missing");
                    break;
                case 3:
                    animHeritage.Play("Heritage");
                    break;
                case 4:
                    animFiesta.Play("Fiesta");
                    break;
                case 5:
                    SceneManager.LoadScene(2);
                    SceneManager.LoadScene(1, LoadSceneMode.Additive);
                    break;
            }
        }
    }
}
