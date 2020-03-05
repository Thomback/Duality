using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnnemy;

    public ItemSlots itemSlots;
    public BattleStats battleStats;
    public ItemList itemList;
    public Animator anim;

    private GameObject temp;



    // Start is called before the first frame update
    void Start()
    {
        if(itemSlots.Equals(null))
            itemSlots = GetComponent<ItemSlots>();
        if(anim.Equals(null))
            anim = transform.GetChild(0).GetComponent<Animator>();
        if(battleStats.Equals(null))
            battleStats = GetComponent<BattleStats>();
        if(itemList.Equals(null))
            itemList = GameObject.FindWithTag("GameController").GetComponent<ItemList>();
    }

    public float attack1()
    {
        switch (itemSlots.weaponSlot)
        {
            case 1:
                anim.Play("SingleHandSlash");
                Collider2D[] ennemiesToDamage;
                if(itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().shape == "Sphere")
                {
                    ennemiesToDamage = Physics2D.OverlapCircleAll(itemList.items[itemSlots.weaponSlot].attackAreaSimple.transform.position,
                        itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeSphere, whatIsEnnemy);
                }
                else
                {
                    ennemiesToDamage = Physics2D.OverlapBoxAll(itemList.items[itemSlots.weaponSlot].attackAreaSimple.transform.position,
                        new Vector2(itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeX, itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeY),
                        whatIsEnnemy);
                }
                
                for(int i=0; i<ennemiesToDamage.Length; i++)
                {
                    if(ennemiesToDamage[i].tag == "Enemy")
                    {
                        ennemiesToDamage[i].GetComponent<BattleStats>().takeDamage(battleStats.finalAttackDamage());
                    }
                }
                return battleStats.finalAttackDelay();
            default:
                return battleStats.finalAttackDelay();
        }
    }

    public float attack2()
    {
        switch (itemSlots.weaponSlot)
        {
            case 1:
                anim.Play("DoubleHandSlash");
                return 1f;
            default:
                return 1;
        }
    }

    public float ability1()
    {
        switch (itemSlots.weaponSlot)
        {
            case 0:
            default:
                return 1;
        }
    }

    public float ability2()
    {
        switch (itemSlots.weaponSlot)
        {
            case 0:
            default:
                return 1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
