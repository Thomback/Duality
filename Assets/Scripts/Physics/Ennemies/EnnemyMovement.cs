using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnnemyMovement : MonoBehaviour
{
    int time = 0;
    public enum ennemyBehavior
    {
        Zombie,
        Pogo,
        Pogozombie,
        DashOut
    }

    public ennemyBehavior behavior = ennemyBehavior.Zombie;



    public EnnemyController controller;

    float horizontalMove = 0f;
    bool jump = false;

    private bool canMove = true;
    private bool isMoving;

    private Transform playerPosition;
    private Transform myPosition;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myPosition = gameObject.GetComponent<Transform>();

        if (controller == null)
            controller = gameObject.GetComponent<EnnemyController>();

    }

    // Update is called once per frame
    void Update()
    {
        this.time = (int)Time.time;
        if (time == 3) behavior = ennemyBehavior.DashOut;
        if (canMove)
        {
            switch (behavior)
            {
                case ennemyBehavior.Zombie:
                    if (myPosition.position.x - 0.1 > playerPosition.position.x)
                    {
                        horizontalMove = -1;
                    }
                    else if (myPosition.position.x + 0.1 < playerPosition.position.x)
                    {
                        horizontalMove = 1;
                    }
                    else
                    {
                        horizontalMove = 0;
                    }
                    break;
                case ennemyBehavior.Pogo:
                    jump = true;
                    break;
                case ennemyBehavior.Pogozombie:
                    jump = true;
                    if (myPosition.position.x - 0.1 > playerPosition.position.x)
                    {
                        horizontalMove = -1;
                    }
                    else if (myPosition.position.x + 0.1 < playerPosition.position.x)
                    {
                        horizontalMove = 1;
                    }
                    else
                    {
                        horizontalMove = 0;
                    }
                    break;
                case ennemyBehavior.DashOut:
                    Debug.LogWarning("DASHOUT");
                    StartCoroutine(dashOut());
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(horizontalMove) != 0)
        {
            if (isMoving == false)// Si la dernière valeur de isMoving est fausse
            {
                isMoving = true;
            }
        }
        else
        {
            if (isMoving == true)// Si la dernière valeur de isMoving est vraie
            {
                isMoving = false;
            }
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    public IEnumerator dashOut()
    {
        horizontalMove = 0;
        Debug.LogWarning("He wait : " + time);
        yield return new WaitForSeconds(1);
        Debug.LogWarning("TIME : " + time);

        gameObject.GetComponent<BattleStats>().attackDamage = 40;

        if (myPosition.position.x - 0.1 > playerPosition.position.x)
        {
            horizontalMove = 8;
            yield return new WaitForSeconds(1);
            horizontalMove = -8;
        }
        else if (myPosition.position.x + 0.1 < playerPosition.position.x)
        {
            horizontalMove = -8;
            yield return new WaitForSeconds(1);
            horizontalMove = 8;
        }
        else
        {
            horizontalMove = 0;
        }
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<BattleStats>().attackDamage = 15;
        behavior = ennemyBehavior.Zombie;

    }

}
