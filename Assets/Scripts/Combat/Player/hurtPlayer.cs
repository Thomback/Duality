using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hurtPlayer : MonoBehaviour
{
    private BattleStats battleStats;
    public Animator anim;

    public float invincibilitySeconds = 2f;

    void Start()
    {
        if (GetComponent<BattleStats>())
        {
            battleStats = GetComponent<BattleStats>();
        }
        if (anim.Equals(null))
            anim = transform.GetChild(0).GetComponent<Animator>();
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<BattleStats>() != null)
                battleStats.takeDamage(collision.gameObject.GetComponent<BattleStats>().finalAttackDamage());
            else
            {
                battleStats.takeDamage(5);
                Debug.Log("Erreur, ennemi sans BattleStats");
            }


            battleStats.hitStun(collision.gameObject, 1, 0.35f);
            battleStats.enableInvicibilityFrames(invincibilitySeconds);
            StopAllCoroutines();
            anim.SetBool("isBlinking", true);
            StartCoroutine(waitThenDeblink());
        }
    }

    private IEnumerator waitThenDeblink()
    {
        yield return new WaitForSeconds(invincibilitySeconds);
        anim.SetBool("isBlinking", false);
    }
}
