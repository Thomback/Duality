using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDamage : MonoBehaviour
{
    public GameObject spear;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Projectile Damage");
            GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<BattleStats>().takeDamage(15);
            GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<BattleStats>().hitStun(GameObject.FindGameObjectsWithTag("Player")[0], 0.1f, 0.5f);
            Destroy(spear);
        }
        Destroy(spear);
    }
}
