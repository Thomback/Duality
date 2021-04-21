using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class GamePlaySwitcher : MonoBehaviour
{

    bool cardGamePlay = false, chaosControl = false, antiChaosControl = false;

    DeckUI deckUI;
    float timer = 0.0f;

    public int timeBeforeSwitch = 30;
    public AudioSource music;

    private BattleStats playerStats;
    private PlayerMovementBrackeys playerMovement;
    private CharacterControllerBrackeys CCB;
    private BattleStats BS;
    private bool wasInBattle = false;
    Coroutine currentCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        deckUI = GameObject.FindWithTag("UI").GetComponent<DeckUI>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<BattleStats>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovementBrackeys>();

        CCB = GameObject.FindWithTag("Player").GetComponent<CharacterControllerBrackeys>();
        BS = GameObject.FindWithTag("Player").GetComponent<BattleStats>();

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
            if (timer >= 0.8f)
            {
                chaosControl = false;
                timer = 0;
                Time.timeScale = 0;
                SwitchGamePlay();
            }
        }

        if (antiChaosControl)
        {
            timer += ((1 -Time.timeScale)*0.05f);
            Time.timeScale = 0 + timer;
            music.pitch = 0 + timer;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            if (timer >= 0.9f)
            {
                antiChaosControl = false;
                timer = 0;
                Time.timeScale = 1;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                music.pitch = 1;
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
        if(playerStats.inBattle)
            chaosControl = true;
    }

    void SwitchGamePlay()
    {
        cardGamePlay = !cardGamePlay;
        if (cardGamePlay)
        {
            Time.timeScale = 0;
            playerMovement.ChangeControl(false);
            deckUI.SwapHandUI(true);
            DeckManager.instance.FullHand();

        }
        else
        {
            deckUI.SwapHandUI(false);
            chaosControl = false;
            antiChaosControl = true;
            Time.timeScale = 0.2f;
            playerMovement.ChangeControl(true);
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
