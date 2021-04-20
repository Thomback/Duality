using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interlude : MonoBehaviour
{
    [SerializeField]
    private GameObject DialogBox;
    [SerializeField]
    private TMP_Text TextContainer;
    [SerializeField]
    private string texte;
    [SerializeField]
    private string nextScene;
    [SerializeField]
    private bool isLevel = true;

    private bool active = false;
    void Start()
    {
        StartCoroutine(Wait());
    }

    void Update()
    {
        if ((Input.GetKeyUp(KeyCode.Space) || (Input.GetMouseButtonUp(0))) && active == true)
        {
            if (isLevel)
                SceneController.Instance.FadeToLevel(nextScene);
            else
                SceneController.Instance.FadeToScene(nextScene);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(EcrirePhrase(texte));
        active = true;
    }

    IEnumerator EcrirePhrase(string phrase)
    {
        TextContainer.text = "";
        foreach (char lettre in phrase.ToCharArray())
        {
            if (lettre != 'a' && lettre != 'e' && lettre != 'i' && lettre != 'o' && lettre != 'u' && lettre != 'y')
                DialogBox.GetComponent<AudioSource>().Play();
            TextContainer.text += lettre;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
