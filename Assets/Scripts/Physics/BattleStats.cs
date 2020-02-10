using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStats : MonoBehaviour
{
    [Header("Defensive stats")]
    [Tooltip("Entity's max HP")]
    public float maxHP = 100;               // Entity's max hp
    //[HideInInspector]
    public float currentHP;                 // Entity's current hp
    [Tooltip("Entity's incoming damage flat reduction")]
    public float flatDmgReduction = 0;      // Entity's incoming damage flat reduction
    [Tooltip("Entity's incoming damage reduction in percentage")]
    public float dmgReduction = 0;          // Entity's incoming damage reduction in percentage

    [Header("Offensive stats")]
    [Tooltip("Entity's base attack damage")]
    public float attackDamage = 3;          // Entity's base attack damage
    [Tooltip("Entity's flat attack damage increase")]
    public float flatAttackDamageIncrease = 0;  // Entity's flat attack damage increase
    [Tooltip("Entity's attack damage increase in percentage")]
    public float attackDamageIncrease = 0;  // Entity's attack damage increase in percentage
    [Tooltip("Entity's attack cooldown")]
    public float attackDelay = 0.5f;        // Entity's attack cooldown

    [Header("Mobility stats")]
    [Tooltip("Entity's run speed")]
    public float runSpeed = 60;               // Entity's run speed
    [Tooltip("Entity's flat run speed increase")]
    public float flatRunSpeedIncrease = 0;    // Entity's flat run speed increase
    [Tooltip("Entity's run speed increase in percentage")]
    public float runSpeedIncrease = 0;        // Entity's run speed increase in percentage
    [Tooltip("Entity's jump force")]
    public float jumpForce = 20;            // Entity's jump force

    [HideInInspector]
    public bool invicibilityFrames;


    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }


    public void takeDamage(float totalAttackDamage)
    {
        if (!this.invicibilityFrames)
        {
            this.currentHP = this.currentHP - Mathf.CeilToInt((totalAttackDamage - this.flatDmgReduction) - ((totalAttackDamage - this.flatDmgReduction) * this.dmgReduction));
        }
        Debug.Log("Ouch, il me reste " + this.currentHP + " pv.");
    }
    public void takeDamage(float totalAttackDamage, bool trueDamage)
    {
        if (!this.invicibilityFrames)
        {
            if (trueDamage)
            {
                this.currentHP -= totalAttackDamage;
            }
            else
            {
                this.currentHP -= Mathf.FloorToInt((totalAttackDamage - this.flatDmgReduction) / this.dmgReduction);
            }
        }
    }

    public float finalAttackDamage()
    {
        return Mathf.CeilToInt((this.attackDamage + this.flatAttackDamageIncrease) + ((this.attackDamage + this.flatAttackDamageIncrease) * (this.attackDamageIncrease / 100)));
    }

    public void enableInvicibilityFrames(float seconds)
    {
        if (!this.invicibilityFrames)
        {
            this.invicibilityFrames = true;
            StartCoroutine(waitThenStopInvicibility(seconds));
        }
    }

    public void hitStun()
    {
        if (!this.invicibilityFrames)
        {
            GetComponent<REALPlayerMovementBrackeys>().enabled = false;
            if(GetComponent<Transform>().localScale.x >= 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-7f, 10f);
            }else{
                GetComponent<Rigidbody2D>().velocity = new Vector2(7f, 10f);
            }
            
            StartCoroutine(waitThenEnableMovement());
        }
    }

    private IEnumerator waitThenStopInvicibility(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.invicibilityFrames = false;
    }

    private IEnumerator waitThenEnableMovement()
    {
        yield return new WaitForSeconds(0.35f);
        GetComponent<REALPlayerMovementBrackeys>().enabled = true;
    }
}
