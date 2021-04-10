using System.Collections;
using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
    // door
    GameObject door;

    float time = 5;
    public enum ennemyBehavior
    {
        Zombie,
        Pogo,
        Pogozombie,
        DashOut
    }
    public bool isInRange = false;
    public bool rangeAttack = true;
    public ennemyBehavior behavior = ennemyBehavior.Zombie;

    public EnnemyController controller;

    float horizontalMove = 0f;
    bool jump = false;

    private bool canMove = true;
    private bool isMoving;

    private Transform playerPosition;
    private Transform myPosition;

    public float pointDeVie;
    public float percentCurrentHP;

    public int intervalleMax;

    private void Start()
    {
        // door
        door = GameObject.Find("Door");
        door.SetActive(false);

        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myPosition = gameObject.GetComponent<Transform>();

        if(controller == null)
            controller = gameObject.GetComponent<EnnemyController>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (isInRange)
        {
            pointDeVie = gameObject.GetComponent<BattleStats>().currentHP;
            percentCurrentHP = (pointDeVie * 100) / 300;
            time -= Time.deltaTime;   
            if (time <= 0) 
            {
                if(percentCurrentHP > 70) time = 2;
                if (percentCurrentHP < 70 & percentCurrentHP > 50) time = 1;
                if (percentCurrentHP < 50) time = 0.5f;
                if (rangeAttack == true) intervalleMax = 6;
                if (rangeAttack == false) intervalleMax = 2;
                float EventAttack = Random.Range(0, intervalleMax);
                Debug.Log(intervalleMax);
                if (EventAttack > 0 & rangeAttack == true && canMove)
                {
                    canMove = !canMove;
                    StartCoroutine(shootWait());
                } 
                if (EventAttack == 0 && canMove)
                {
                    canMove = !canMove;
                    StartCoroutine(dashOut());
                    //behavior = ennemyBehavior.DashOut;
                }
            }

            if (canMove)
            {
                switch (behavior)
                {
                    case ennemyBehavior.Zombie:
                        if(myPosition.position.x - 0.1 > playerPosition.position.x)
                        {
                            horizontalMove = -1;
                        }
                        else if (myPosition.position.x + 0.1< playerPosition.position.x)
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
                    case ennemyBehavior.Pogozombie :
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
                        Debug.Log("DASHOUT");
                        StartCoroutine(dashOut());
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player"))
        {
            door.SetActive(true);
            isInRange = true;
            rangeAttack = false;
            CircleCollider2D collider = GetComponent<CircleCollider2D>();
            collider.radius = 3;
            //Debug.Log("The ennemy can see the player");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rangeAttack = true;
            GameObject.FindWithTag("Player").GetComponent<BattleStats>().inBattle = true;
            //Debug.Log("The ennemy can see the player");
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
    IEnumerator shootWait()
    {
        horizontalMove = 0;
        yield return new WaitForSeconds(0.7f);
        GameObject.Find("RockShoot").GetComponent<Projectile>().projectileLaunch();
        canMove = !canMove;
    }

    IEnumerator dashOut()
    {
        horizontalMove = 0;
        screenShake.Instance.ShakeCamera(1, 1.5f);
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<BattleStats>().attackDamage = 40;

        if (myPosition.position.x - 0.1 > playerPosition.position.x)
        {
            horizontalMove = 1;
            yield return new WaitForSeconds(1.2f);
            screenShake.Instance.ShakeCamera(2, 1.5f);
            horizontalMove = -8;
        }
        else if (myPosition.position.x + 0.1 < playerPosition.position.x)
        {
            horizontalMove = -1;
            yield return new WaitForSeconds(1.2f);
            screenShake.Instance.ShakeCamera(2, 1.5f);
            horizontalMove = 8;
        }
        else
        {
            horizontalMove = 0;
        }
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<BattleStats>().attackDamage = 15;

        canMove = !canMove;

        //behavior = ennemyBehavior.Zombie;

    }

}
