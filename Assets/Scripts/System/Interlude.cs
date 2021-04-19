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

    private bool active = false;
    void Start()
    {
        StartCoroutine(Wait());
    }

    void Update()
    {
        if ((Input.GetKeyUp(KeyCode.Space) || (Input.GetMouseButtonUp(0))) && active == true)
        {
            SceneController.Instance.FadeToLevel("Etage1");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(EcrirePhrase("Notre héros vient d'entrer dans la tour sans plus d'accroc... Qui sait ce qui l'attend désormais?"));
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
