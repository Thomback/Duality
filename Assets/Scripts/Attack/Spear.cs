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
        GameObject spear = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        //Rock.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector2(1,0.3f)* strength);
        //Rock.GetComponent<Rigidbody2D>().AddForce(transform.forward * strength);
        if (gameObject.transform.position.x > gameObject.transform.parent.gameObject.transform.position.x) //If facing right
            spear.GetComponent<Rigidbody2D>().velocity = new Vector2(strength, 3f);
        else
            spear.GetComponent<Rigidbody2D>().velocity = new Vector2(strength * -1, 3f);
    }
}
