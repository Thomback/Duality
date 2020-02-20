using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBrackeys : MonoBehaviour
{
    
    private bool isMooving;

    public CharacterControllerBrackeys controller;
    public BattleStats battleStats;
    public Animator anim;
    public PlayerAttack playerAttack;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    

    private float timeBtwAttack;
    private float startTimeBtwAttack;


    private int waitedSeconds = 0; //Seconds waited for our idle animations trigger

    private void Start()
    {
        if (anim.Equals(null))
            anim = transform.GetChild(0).GetComponent<Animator>();
        if (playerAttack.Equals(null))
            playerAttack = GetComponent<PlayerAttack>();
        if(battleStats.Equals(null))
            battleStats = GetComponent<BattleStats>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        // ----- Abilities -----

        if(timeBtwAttack <= 0)
        {
            // Attaque principale
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Attaque simple");
                

                startTimeBtwAttack = playerAttack.attack1();
                timeBtwAttack = startTimeBtwAttack;
                StopAllCoroutines();
                waitedSeconds = 0;
            }

            // Attaque lourde
            else if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Attaque lourde");
                playerAttack.attack2();

                startTimeBtwAttack = playerAttack.attack2();
                timeBtwAttack = startTimeBtwAttack;
                StopAllCoroutines();
                waitedSeconds = 0;
            }

            // Equipement 1
            else if (Input.GetButtonDown("Ability1"))
            {
                Debug.Log("Equipement 1");
                ;

                startTimeBtwAttack = playerAttack.ability1();
                timeBtwAttack = startTimeBtwAttack;
                StopAllCoroutines();
                waitedSeconds = 0;
            }

            // Equipement 2
            else if (Input.GetButtonDown("Ability2"))
            {
                Debug.Log("Equipement 2");
                playerAttack.ability2();

                startTimeBtwAttack = playerAttack.ability2();
                timeBtwAttack = startTimeBtwAttack;
                StopAllCoroutines();
                waitedSeconds = 0;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        


    }

    private void FixedUpdate()
    {
        if(Mathf.Abs(horizontalMove) != 0)
        {
            if (isMooving == false)// Si la dernière valeur de isMooving est fausse
            {
                isMooving = true;
                anim.SetBool("isRunning", true);
                anim.SetTrigger("Run");
                StopAllCoroutines();
                waitedSeconds = 0;
            }
        }
        else
        {
            if(isMooving == true)// Si la dernière valeur de isMooving est vraie
            {
                isMooving = false;
                anim.SetBool("idleEvent", false);
                anim.SetBool("isRunning", false);
                anim.SetTrigger("Stay");
                StartCoroutine(idleWaiter());
            }
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }



    // Partie jolis Idle

    private IEnumerator idleWaiter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            anim.SetBool("idleEvent", false);
            anim.SetBool("idleEvent2", false);
            anim.SetBool("idleEvent3", false);
            anim.SetBool("idleEvent4", false);
            yield return new WaitForSeconds(1);
            waitedSeconds = waitedSeconds + 2;
            if(waitedSeconds >= 12)
            {
                
                switch (Mathf.FloorToInt(Random.Range(0, 4)))
                {
                    case 0:
                        anim.SetBool("idleEvent", true);
                        break;
                    case 1:
                        anim.SetBool("idleEvent2", true);
                        break;
                    case 2:
                        anim.SetBool("idleEvent3", true);
                        break;
                    case 3:
                        anim.SetBool("idleEvent4", true);
                        StartCoroutine(waitForPushUps());
                        break;
                    default:
                        break;
                }
                waitedSeconds = 0;
            }
        }
    }
    

    private IEnumerator waitForPushUps()
    {
        yield return new WaitForSeconds(9);
        anim.SetTrigger("pushUpEnd");
    }
}
