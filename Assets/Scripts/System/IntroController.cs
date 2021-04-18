using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public Animator animFamily;
    public Animator animMissing;
    public Animator animHeritage;
    public Animator animFiesta;

    private Camera cameraIntro;
    private float accelerator = 0;

    [SerializeField]
    private GameObject DialogBox;
    [SerializeField]
    private TMP_Text TextContainer;
    private int avancementHistoire = 0;
    private Queue<string> phrases;

    private float compteur = 0;
    private float fadeToBlack = 1.5f;
    private bool active = false;


    private void Start()
    {
        DialogBox.SetActive(false);
        cameraIntro = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        phrases = new Queue<string>();
        phrases.Enqueue("Notre petite histoire commence dans un large domaine au nord de l'Ecosse.");
        phrases.Enqueue("Dans celle-ci, James Hamilton, le jeune comte, vivait heureux avec son père, avec lequel il partageait régulièrement " +
            "des soirées de cornemuse endiablées.");
        phrases.Enqueue("Malheureusement pour notre héros, le vieil Hamilton aimait la chasse. Un jour, cette activité finit par en venir " +
            "a bout, et le père de James devint introuvable, laissant son fils à l'abandon.");
        phrases.Enqueue("Les seuls souvenirs qui resteraient à notre protagoniste furent le riche domaine, chateau familial, ses sujet, " +
            "ses vivres, sa cuisine équipée, sa cave à bière...");
        phrases.Enqueue("...ainsi que la cornemuse de son père. Elle siégeait fière, au dessus de la cheminée du salon, dégageant une aura " +
            "qui semblait protéger le jeune orphelin.");
        phrases.Enqueue("Les années passèrent, passèrent, jusqu'à nous retrouver aujourd'hui. James est maintenant un homme respecté par " +
            "ses valets, avec lesquels il passe le plus clair de ses journées à festoyer.");
        phrases.Enqueue("Cependant cette fois-ci, James était triste. Il repensait à son père qu'il avait perdu dans sa jeunesse. Il repensait " +
            "aux soirées musicales qu'il chérissait tant.");
        phrases.Enqueue("Il décida donc, à la mémoire de son père, et après quelques verres, d'empoigner la cornemuse familiale et de jouer " +
            "pour ses valets.");
        phrases.Enqueue("Aux premières notes, l'instrument s'éveilla. Une aura démoniaque surgit de l'outil maléfique, ses compagnons de beuverie " +
            "semblaient disparaître dans les ténèbres un à un et un tremblement de terre se faisait sentir.");
        phrases.Enqueue("Un regard par la fenêtre permettait d'apercevoir la source du rafut, une tour géante qui sortait du sol de son domaine.");
        phrases.Enqueue("Bientôt les habitants démoniaques de l'édifice allaient envahir son chateau, il le savait. Cependant, James sentait de " +
            "nouveau la présence de son père en lui. Il devait tirer tout ça au clair.");
        phrases.Enqueue("Après une dernière pinte de bière, paré de son kilt de combat et sa cornemuse de guerre, il allait renverser les envahisseurs.");


    }
    void Update()
    {
        if (cameraIntro.orthographicSize > 6)
        {
            cameraIntro.orthographicSize -= accelerator;
            cameraIntro.gameObject.transform.Rotate(0, 0, accelerator);
            accelerator += 0.0001f;
        }

        if (!active && cameraIntro.orthographicSize <= 6)
        {
            active = !active;
            DialogBox.SetActive(true);
            AfficherProchainePhrase();
        }

        if(compteur > 0)
            compteur -= Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Space) && active && compteur <= 0)
        {
            avancementHistoire++;
            AfficherProchainePhrase();
            compteur = 0.3f;
            switch (avancementHistoire)
            {
                case 2:
                    animFamily.Play("Family");
                    break;
                case 4:
                    animMissing.Play("Missing");
                    break;
                case 5:
                    animHeritage.Play("Heritage");
                    break;
                case 8:
                    animFiesta.Play("Fiesta");
                    break;
                case 12:
                    SceneManager.LoadScene(2);
                    SceneManager.LoadScene(1, LoadSceneMode.Additive);
                    break;
            }
        }
    }

    private void AfficherProchainePhrase()
    {
        if(phrases.Count != 0)
        {
            string phrase = phrases.Dequeue();
            StopAllCoroutines();
            StartCoroutine(EcrirePhrase(phrase));
        }
    }

    IEnumerator EcrirePhrase(string phrase)
    {
        TextContainer.text = "";
        foreach(char lettre in phrase.ToCharArray())
        {
            TextContainer.text += lettre;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
