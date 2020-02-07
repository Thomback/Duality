using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hurtPlayer : MonoBehaviour
{
    public GameObject text;

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
        Debug.Log("Collision avec " + collision.gameObject.name);
        if (collision.gameObject.layer == 9 && collision.gameObject.tag == "Ennemy")
        {
            battleStats.takeDamage(5);
            battleStats.hitStun();
            battleStats.enableInvicibilityFrames(3);
            text.GetComponent<Text>().text = GetComponent<BattleStats>().currentHP.ToString() + " / " + GetComponent<BattleStats>().maxHP.ToString();
        }
    }
}
