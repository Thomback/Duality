using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySwitcher : MonoBehaviour
{

    bool cardGamePlay = false, chaosControl = false;

    DeckUI deckUI;
    float timer = 0.0f;

    public int timeBeforeSwitch = 30;

    Coroutine currentCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        deckUI = GameObject.FindWithTag("UI").GetComponent<DeckUI>();
        LaunchFight();
    }

    // Update is called once per frame
    void Update()
    {
        // Chaos Control
        if (chaosControl)
        {
            timer += Time.deltaTime * 3;
            Time.timeScale = 1 - timer;
            if (timer >= 0.9f)
            {
                chaosControl = false;
                timer = 0.0f;
                Time.timeScale = 0;
                SwitchGamePlay();
            }
        }

        // On switch le GamePlay 
        if (cardGamePlay && DeckManager.instance.currentHand.cardCount == 0)
        {
            SwitchGamePlay();
        }
    }

    public void LaunchFight()
    {
        cardGamePlay = false;
        SwitchGamePlay();
    }

    IEnumerator WaitForSwitch()
    {
        chaosControl = false;
        yield return new WaitForSecondsRealtime(timeBeforeSwitch - 1);
        chaosControl = true;
    }

    void SwitchGamePlay()
    {
        cardGamePlay = !cardGamePlay;
        if (cardGamePlay)
        {
            Time.timeScale = 0;
            deckUI.SwapHandUI(true);
            DeckManager.instance.FullHand();
        }
        else
        {
            deckUI.SwapHandUI(false);
            chaosControl = false;
            Time.timeScale = 1.0f;
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
            }
            currentCoroutine = StartCoroutine(WaitForSwitch());
        }
        deckUI.UpdateUI();
    }

}
