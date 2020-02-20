using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hurtPlayer : MonoBehaviour
{
    private BattleStats battleStats;

    private void Start()
    {
        if (GetComponent<BattleStats>())
        {
            battleStats = GetComponent<BattleStats>();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && collision.gameObject.tag == "Enemy")
        {
            battleStats.takeDamage(5);
            battleStats.hitStun(collision.gameObject);
            battleStats.enableInvicibilityFrames(0.65f);
        }
    }
}
