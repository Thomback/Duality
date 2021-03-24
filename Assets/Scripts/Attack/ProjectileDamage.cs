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
            Debug.Log("Projectile Damage");
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<BattleStats>().takeDamage(10);
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<BattleStats>().hitStun(GameObject.FindGameObjectsWithTag("Enemy")[0], 0.2f, 0.5f);
            Destroy(Projectil);
        }
    }
}
