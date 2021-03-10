using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Projectil;
    public int strenght = 10;
    
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject Rock = Instantiate(Projectil, transform.position, Quaternion.identity) as GameObject;
            Rock.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.right * strenght);
            Destroy(Rock, 3f);
        }*/
    }
    public void projectileLaunch()
    {
        GameObject Rock = Instantiate(Projectil, transform.position, Quaternion.identity) as GameObject;
        Rock.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.right* strenght);
        Destroy(Rock, 3f);
    }
}
