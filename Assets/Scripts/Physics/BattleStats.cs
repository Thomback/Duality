using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStats : MonoBehaviour
{
    [Header("Defensive stats")]
    [Tooltip("Entity's max HP")]
    public float maxHP = 100;               // Entity's max hp
    [HideInInspector]
    public float flatMaxHPIncrease = 0;     // Entity's flat maxHPIncrease
    [HideInInspector]
    public float maxHPIncrease = 0;         // Entity's max HP increase in percentage
    [HideInInspector]
    public float currentHP;                 // Entity's current hp
    [Tooltip("Entity's incoming damage flat reduction")]
    [HideInInspector]
    public float flatDmgReduction = 0;      // Entity's incoming damage flat reduction
    [Tooltip("Entity's incoming damage reduction in percentage")]
    [HideInInspector]
    public float dmgReduction = 0;          // Entity's incoming damage reduction in percentage

    [Header("Offensive stats")]
    [Tooltip("Entity's base attack damage")]
    public float attackDamage = 3;          // Entity's base attack damage
    [Tooltip("Entity's flat attack damage increase")]
    [HideInInspector]
    public float flatAttackDamageIncrease = 0;  // Entity's flat attack damage increase
    [Tooltip("Entity's attack damage increase in percentage")]
    [HideInInspector]
    public float attackDamageIncrease = 0;    // Entity's attack damage increase in percentage
    [Tooltip("Entity's attack cooldown")]
    public float attackDelay = 1f;            // Entity's attack cooldown
    [Tooltip("Entity's attack cooldown")]
    [HideInInspector]
    public float attackDelayDecrease = 0f;    // Entity's attack delay decrease in percentage

    [Header("Mobility stats")]
    [Tooltip("Entity's run speed")]
    public float runSpeed = 60;               // Entity's run speed
    [Tooltip("Entity's flat run speed increase")]
    [HideInInspector]
    public float flatRunSpeedIncrease = 0;    // Entity's flat run speed increase
    [Tooltip("Entity's run speed increase in percentage")]
    [HideInInspector]
    public float runSpeedIncrease = 0;        // Entity's run speed increase in percentage
    [Tooltip("Entity's jump force")]
    public float jumpForce = 20;              // Entity's jump force
    [Tooltip("Entity's flat jump force increase")]
    [HideInInspector]
    public float flatJumpForceIncrease = 0;   // Entity's jump force flat increase
    [Tooltip("Entity's jump force increase in percentage")]
    [HideInInspector]
    public float JumpForceIncrease = 0;       // Entity's jump force increase in percentage

    [HideInInspector]
    public bool invicibilityFrames;


    // Start is called before the first frame update
    void Awake()
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

    public void healHP(float HPHealed)
    {
        this.currentHP += HPHealed;
        if (this.currentHP > this.maxHP)
        {
            this.currentHP = this.maxHP;
        }
    }

    public float finalMaxHP()
    {
        return Mathf.FloorToInt((this.maxHP + this.flatMaxHPIncrease) + ((this.attackDamage + this.flatMaxHPIncrease) * (this.maxHPIncrease / 100)));
    }

    public float finalAttackDamage()
    {
        return Mathf.CeilToInt((this.attackDamage + this.flatAttackDamageIncrease) + ((this.attackDamage + this.flatAttackDamageIncrease) * (this.attackDamageIncrease / 100)));
    }

    public float finalAttackDelay()
    {
        return Mathf.CeilToInt(this.attackDelay - (this.attackDelay * (this.attackDelayDecrease / 100)));
    }

    public float finalRunSpeed()
    {
        return Mathf.CeilToInt((this.runSpeed + this.flatRunSpeedIncrease) + ((this.runSpeed + this.flatRunSpeedIncrease) * (this.runSpeedIncrease / 100)));
    }

    public float finalJumpForce()
    {
        return Mathf.CeilToInt((this.jumpForce + flatJumpForceIncrease) + ((this.jumpForce + this.flatJumpForceIncrease) * (this.JumpForceIncrease / 100)));
    }

    public void resetModifiers()
    {
        this.flatAttackDamageIncrease
            = this.attackDamageIncrease
            = this.flatDmgReduction
            = this.dmgReduction
            = this.flatRunSpeedIncrease
            = this.runSpeedIncrease
            = this.flatJumpForceIncrease
            = this.JumpForceIncrease
            = this.attackDelayDecrease
            = this.flatMaxHPIncrease
            = this.maxHPIncrease
            = 0f;

        if (this.currentHP > this.maxHP)
        {
            this.currentHP = this.maxHP;
        }
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
            GetComponent<CharacterControllerBrackeys>().Jump();
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
