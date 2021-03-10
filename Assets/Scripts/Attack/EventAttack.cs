using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAttack : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float EventAttack = Random.Range(0, 2);
            Debug.Log(EventAttack);
            if (EventAttack == 1)
            {
                GameObject.Find("RockShoot").GetComponent<Projectile>().projectileLaunch();
            }
        }
    }
}
