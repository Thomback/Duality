using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public GameObject projectile;
    //public GameObject Particule;
    public int strength = 20;

    public void SpearLaunch()
    {
        if (gameObject.transform.position.x > gameObject.transform.parent.gameObject.transform.position.x) //If facing right 
        {
            Quaternion spawnRotation = Quaternion.Euler(0, 0, -90);
            GameObject spear = Instantiate(projectile, transform.position, spawnRotation) as GameObject;
            spear.GetComponent<Rigidbody2D>().velocity = new Vector2(strength, 3f);
        }
        else
        {
            Quaternion spawnRotation = Quaternion.Euler(0, 0, 90);
            GameObject spear = Instantiate(projectile, transform.position, spawnRotation) as GameObject;
            spear.GetComponent<Rigidbody2D>().velocity = new Vector2(strength * -1, 3f);
        }
    }
}
