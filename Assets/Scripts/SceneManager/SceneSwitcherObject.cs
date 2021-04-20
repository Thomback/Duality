using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcherObject : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;
    [SerializeField]
    private bool isLevel = false;

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Player")
        {
            if(GameObject.FindWithTag("Enemy") == null)
            {
                GetComponent<Collider2D>().enabled = false;
                if(!isLevel)
                    SceneController.Instance.FadeToScene(sceneToLoad);
                else
                    SceneController.Instance.FadeToLevel(sceneToLoad);
            }
        }
    }
}
