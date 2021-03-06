﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBrackeys : MonoBehaviour
{
    
    private bool isMoving;

    public CharacterControllerBrackeys controller;
    public BattleStats battleStats;
    public Animator anim;
    public PlayerAttack playerAttack;

    float horizontalMove = 0f;              // In which direction and at which coefficient is the player moving
    bool jump = false;                      // Is the player in the air
    bool crouch = false;                    // NOT IN USE - Is the player crouching

    private bool canMove = true;            // Can the player move and jump
    private bool hasControl = true;         // Has the player control
    private bool holdingM2 = false;         // Is the player holding "Fire2"

    private float timeBtwAttack;            // Temps restant avant déblocage des attaques
    private float startTimeBtwAttack;       // Temps auquel le décompte a démarré

    private int memoireTampon = 0;          // Capacité mise en mémoire tampon


    private int waitedSeconds = 0;          // Seconds waited for our idle animations trigger

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
        if (hasControl)
        {
            if (canMove)
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
            }

            // ----- Abilities -----

            if(timeBtwAttack <= 0 && !holdingM2)
            {
                // Attaque principale
                if (Input.GetButtonDown("Fire1"))
                {
                    launchAttack1();
                }

                // Attaque lourde
                else if (Input.GetButtonDown("Fire2"))
                {
                    launchAttack2();
                }

                // Equipement 1
                else if (Input.GetButtonDown("Ability1"))
                {
                    launchAbility1();
                }

                // Equipement 2
                else if (Input.GetButtonDown("Ability2"))
                {
                    launchAbility2();
                }

            }
            else
            {
                timeBtwAttack -= Time.deltaTime;

                // ---------- MEMOIRE TAMPON ----------
                if (!holdingM2 && timeBtwAttack <= 0.1f && timeBtwAttack > 0)   // S'il reste 0.1 second au cd d'attaque ou moins
                {
                    if (Input.GetButton("Fire1"))
                        memoireTampon = 1;
                    else if (Input.GetButton("Fire2"))
                        memoireTampon = 2;
                    else if (Input.GetButton("Ability1"))
                        memoireTampon = 3;
                    else if (Input.GetButton("Ability2"))
                        memoireTampon = 4;
                }

                if (timeBtwAttack <= 0 && !holdingM2) // Si fin du cooldown d'attaque (et M2 pas enfoncé)
                {
                    if(memoireTampon == 1)
                    {
                        launchAttack1();
                    }else if(memoireTampon == 2)
                    {
                        launchAttack2();
                    }
                    else if (memoireTampon == 3)
                    {
                        launchAbility1();
                    }
                    else if (memoireTampon == 4)
                    {
                        launchAbility2();
                    }
                    else
                    {
                        ChangeControl(true);
                    }
                    memoireTampon = 0;
                }
            }

            if(holdingM2)       // Release de l'attaque lourde
            {
                if (!Input.GetButton("Fire2"))
                {
                    startTimeBtwAttack = playerAttack.attack2Release();
                    timeBtwAttack = startTimeBtwAttack;
                    StopAllCoroutines();
                    waitedSeconds = 0;

                    ChangeControl(false);

                    holdingM2 = false;  
                }
            }
        }

        Debug.Log("Is player dead? :" + battleStats.dead);
        // Check if the player died, and if he still has control
        if (battleStats.dead)
        {
            hasControl = false;
            Debug.Log(gameObject.name + " is dead.");
        }
    }

    private void FixedUpdate()
    {
        if(Mathf.Abs(horizontalMove) != 0)
        {
            if (isMoving == false)// Si la dernière valeur de isMoving est fausse
            {
                isMoving = true;
                anim.SetBool("isRunning", true);
                anim.SetTrigger("Run");
                StopAllCoroutines();
                waitedSeconds = 0;
            }
        }
        else
        {
            if(isMoving == true)// Si la dernière valeur de isMoving est vraie
            {
                isMoving = false;
                anim.SetBool("idleEvent", false);
                anim.SetBool("isRunning", false);
                anim.SetTrigger("Stay");
                StartCoroutine(idleWaiter());
            }
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void launchAttack1() {
        Debug.Log("Attaque simple");

        startTimeBtwAttack = playerAttack.attack1();
        timeBtwAttack = startTimeBtwAttack;
        StopAllCoroutines();
        waitedSeconds = 0;

        ChangeControl(false);
    }

    private void launchAttack2() {
        Debug.Log("Attaque lourde");
        playerAttack.attack2();

        StopAllCoroutines();

        holdingM2 = true;
    }

    private void launchAbility1() {
        Debug.Log("Equipement 1");

        startTimeBtwAttack = playerAttack.ability1();
        timeBtwAttack = startTimeBtwAttack;
        StopAllCoroutines();
        waitedSeconds = 0;
    }

    private void launchAbility2() {
        Debug.Log("Equipement 2");

        startTimeBtwAttack = playerAttack.ability2();
        timeBtwAttack = startTimeBtwAttack;
        StopAllCoroutines();
        waitedSeconds = 0;
    }

    //Gérer le contrôle du joueur
    public void ChangeControl(bool status)
    {
        this.canMove = status;
        
        if (canMove == false)
        {
            horizontalMove = 0; // Stoppe le joueur à sa position
            controller.StopVelocity();
            StartCoroutine(freeze());
        }
        
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

    private IEnumerator freeze()
    {
        yield return new WaitForSeconds(0.06f);
        ChangeControl(true);
    }
}
