using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public GameObject Projectil;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BattleStats playerStats = collision.gameObject.GetComponent<BattleStats>();
            CharacterControllerBrackeys playerController = collision.gameObject.GetComponent<CharacterControllerBrackeys>();
            //Debug.Log("Projectile Damage");

            if (playerController.immuneToProjectile)
            {
                //TODO play sound
            }
            else
            {
                playerStats.takeDamage(15);
                playerStats.hitStun(GameObject.FindGameObjectsWithTag("Enemy")[0], 0.1f, 0.5f);
            }
            Destroy(Projectil);
        }
        Destroy(Projectil);
    }
}
