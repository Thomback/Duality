using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Projectil;
    //public GameObject Particule;
    public int strenght = 10;
    
    void Start()
    {
        //Particule.transform.parent = Projectil.transform;
    }
    public void projectileLaunch()
    {
        GameObject Rock = Instantiate(Projectil, transform.position, Quaternion.identity) as GameObject;
        Rock.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.right* strenght);
    }
}
