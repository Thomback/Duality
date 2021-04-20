using System.Collections;
using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
    // door
    GameObject door;
    GameObject[] doorToClose;

    private float time = 2;
    public enum ennemyBehavior
    {
        Zombie,
        Pogo,
        Pogozombie,
        Dwarf,
        OldDemon
    }
    public bool isInRange = false;
    public bool rangeAttack = true;
    public ennemyBehavior behavior = ennemyBehavior.Zombie;

    public EnnemyController controller;
    public BattleStats battleStats;
    public GameObject projectileStart;

    float horizontalMove = 0f;
    bool jump = false;

    [HideInInspector]
    public bool canMove = true;
    private bool isMoving;

    [SerializeField]
    private GameObject ennemyModel;
    private Animator ennemyAnimator;
    private float lastMovement;

    private Transform playerPosition;
    private Transform myPosition;

    private float percentCurrentHP;

    private int intervalleMax;

    private void Start()
    {
        // door
        door = GameObject.Find("Door");
        if(door)
            door.SetActive(false);
        doorToClose = GameObject.FindGameObjectsWithTag("Door");

        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myPosition = gameObject.GetComponent<Transform>();
        ennemyAnimator = ennemyModel.GetComponent<Animator>();

        if(controller == null)
            controller = gameObject.GetComponent<EnnemyController>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (isInRange)
        {
            time -= Time.deltaTime;   
            if (time <= 0 && behavior == ennemyBehavior.Zombie) 
            {
                percentCurrentHP = (battleStats.currentHP * 100) / battleStats.maxHP;

                if(percentCurrentHP > 70) time = 2.5f;
                if (percentCurrentHP < 70 & percentCurrentHP > 50) time = 2;
                if (percentCurrentHP < 50) time = 1f;
                if (rangeAttack == true) intervalleMax = 6;
                if (rangeAttack == false) intervalleMax = 3;
                float EventAttack = Random.Range(0, intervalleMax);
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
                        if(lastMovement == 0 && horizontalMove != 0)
                        {
                            ennemyAnimator.SetBool("Moving", true);
                        }
                        else if(lastMovement != 0 && horizontalMove == 0)
                        {
                            ennemyAnimator.SetBool("Moving", false);
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
                    case ennemyBehavior.Dwarf:
                        if (myPosition.position.x - 1 > playerPosition.position.x)
                        {
                            horizontalMove = -1;
                        }
                        else if (myPosition.position.x + 1 < playerPosition.position.x)
                        {
                            horizontalMove = 1;
                        }
                        else
                        {
                            jump = true;
                        }
                        if (lastMovement == 0 && horizontalMove != 0)
                        {
                            ennemyAnimator.SetBool("isRunning", true);
                        }
                        else if (lastMovement != 0 && horizontalMove == 0)
                        {
                            ennemyAnimator.SetBool("isRunning", false);
                        }
                        if (myPosition.position.y + 1 < playerPosition.position.y)
                            jump = true;
                        break;
                    case ennemyBehavior.OldDemon:
                        if (myPosition.position.x - 3 > playerPosition.position.x)
                        {
                            horizontalMove = -1;
                        }
                        else if (myPosition.position.x + 3 < playerPosition.position.x)
                        {
                            horizontalMove = 1;
                        }
                        else
                        {
                            horizontalMove = 0;
                        }
                        if (lastMovement == 0 && horizontalMove != 0)
                        {
                            ennemyAnimator.SetBool("Walk", true);
                        }
                        else if (lastMovement != 0 && horizontalMove == 0)
                        {
                            ennemyAnimator.SetBool("Walk", false);
                            ennemyAnimator.SetTrigger("Uppercut");
                            canMove = false;
                        }
                        break;
                }
            }
        }
        lastMovement = horizontalMove;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player"))
        {
            if(door)
                door.SetActive(true);
            Debug.Log("Door to close : " + doorToClose.Length);
            if (doorToClose.Length > 0)
            {
                foreach(GameObject door in doorToClose)// Close all doors
                {
                    door.GetComponent<Animator>().SetBool("closeDoor", true);
                }
            }
            isInRange = true;
            rangeAttack = false;
            GameObject.FindWithTag("Player").GetComponent<BattleStats>().inBattle = true;
            //Debug.Log("The ennemy can see the player");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rangeAttack = true;
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
        switch (Random.Range(0, 10))
        {
            case 0:
                SoundScript.PlaySound("laugh1");
                break;
            case 1:
                SoundScript.PlaySound("laugh2");
                break;
            case 2:
                SoundScript.PlaySound("vocalisation1");
                break;
            case 3:
                SoundScript.PlaySound("vocalisation2");
                break;
            default:
                break;
        }

        ennemyAnimator.SetTrigger("Throw");
        yield return new WaitForSeconds(1f);
        //GameObject.Find("RockShoot").GetComponent<Projectile>().projectileLaunch();
        projectileStart.GetComponent<Projectile>().projectileLaunch();
        yield return new WaitForSeconds(0.5f);
        canMove = !canMove;
    }

    IEnumerator dashOut()
    {
        horizontalMove = 0;
        SoundScript.PlaySound("vocalisation3");
        screenShake.Instance.ShakeCamera(1, 1.5f);
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<BattleStats>().attackDamage = 40;

        var rotationVector = ennemyModel.transform.rotation.eulerAngles;
        rotationVector.y += 180;
        ennemyModel.transform.rotation = Quaternion.Euler(rotationVector);

        if (myPosition.position.x - 0.1 > playerPosition.position.x)
        {
            horizontalMove = 1;
            ennemyAnimator.SetBool("BackwardMoving", true);
            yield return new WaitForSeconds(1.2f);
            screenShake.Instance.ShakeCamera(2, 1.5f);
            horizontalMove = -5;
        }
        else if (myPosition.position.x + 0.1 < playerPosition.position.x)
        {
            horizontalMove = -1;
            ennemyAnimator.SetBool("BackwardMoving", true);
            yield return new WaitForSeconds(1.2f);
            screenShake.Instance.ShakeCamera(2, 1.5f);
            horizontalMove = 5;
        }
        else
        {
            horizontalMove = 0;
        }
        ennemyAnimator.SetBool("BackwardMoving", false);
        rotationVector = ennemyModel.transform.rotation.eulerAngles;
        rotationVector.y -= 180;
        ennemyModel.transform.rotation = Quaternion.Euler(rotationVector);

        ennemyAnimator.SetBool("Running", true);
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<BattleStats>().attackDamage = 15;

        ennemyAnimator.SetBool("Moving", false);
        ennemyAnimator.SetBool("Running", false);
        horizontalMove = 0;
        yield return new WaitForSeconds(0.2f);
        canMove = !canMove;

        //behavior = ennemyBehavior.Zombie;

    }


}
