using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uppercut : MonoBehaviour
{
    public GameObject attackCenter;
    public LayerMask whatIsPlayer;
    public EnnemyMovement myMovement;
    public void DealDamage(float damage)
    {
        Collider2D[] playersToDamage;

        playersToDamage = Physics2D.OverlapBoxAll(attackCenter.transform.position, new Vector2(1, 4), whatIsPlayer);

        for (int i = 0; i < playersToDamage.Length; i++)
        {
            if (playersToDamage[i].tag == "Player")
            {// Parcourt la liste des players à endommager
                playersToDamage[i].GetComponent<BattleStats>().takeDamage(damage);
                playersToDamage[i].GetComponent<BattleStats>().hitStun(gameObject, 0.4f, 1f);
            }
        }
    }

    public void StartWalking()
    {
        myMovement.canMove = true;
    }
}
