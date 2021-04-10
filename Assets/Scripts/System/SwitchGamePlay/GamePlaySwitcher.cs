using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class GamePlaySwitcher : MonoBehaviour
{

    bool cardGamePlay = false, chaosControl = false;

    DeckUI deckUI;
    float timer = 0.0f;

    public int timeBeforeSwitch = 30;
    public AudioSource music;

    private BattleStats playerStats;
    private bool wasInBattle = false;
    Coroutine currentCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        deckUI = GameObject.FindWithTag("UI").GetComponent<DeckUI>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<BattleStats>();

        deckUI.SwapHandUI(false);
        deckUI.UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

        if(playerStats.inBattle && !wasInBattle)
        {
            cardGamePlay = false;
            chaosControl = true;
        }
        // Chaos Control
        if (chaosControl)
        {
            timer += Time.deltaTime * 1.2f;
            Time.timeScale = 1 - timer;
            music.pitch = 1 - timer;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
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

        wasInBattle = playerStats.inBattle;
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
            music.pitch = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
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
