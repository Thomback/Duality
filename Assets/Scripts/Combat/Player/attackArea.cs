using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackArea : MonoBehaviour
{
    public string shape = "Cube";
    public float attackRangeX;
    public float attackRangeY;
    public float attackRangeSphere;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if(shape == "Sphere")
        {
            Gizmos.DrawWireSphere(transform.position, attackRangeSphere);
        }
        else
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(attackRangeX, attackRangeY));
        }
        
    }
}
