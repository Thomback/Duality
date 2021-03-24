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

    private int comboCounter;
    private float timer;

    private GameObject temp;

    // Start is called before the first frame update
    void Start()
    {
        if (itemSlots.Equals(null))
            itemSlots = GetComponent<ItemSlots>();
        if(anim.Equals(null))
            anim = transform.GetChild(0).GetComponent<Animator>();
        if(battleStats.Equals(null))
            battleStats = GetComponent<BattleStats>();
        if(itemList.Equals(null))
            itemList = GameObject.FindWithTag("GameController").GetComponent<ItemList>();
    }

    private void Update()
    {
        // combo
        if (comboCounter > 0)
        {
            timer += Time.deltaTime;
            if(timer >= 3)
            {
                comboCounter = 0;
            }
        }
        if (comboCounter == 0 && timer != 0)
        {
            timer = 0;
        }
    }

    public float attack1()
    {
        switch (itemSlots.weaponSlot)
        {
            case 1: //Epées
            case 2: //Hache TEST
                if (comboCounter == 0)
                {
                    anim.Play("SingleHandSlash");
                    //Debug.Log("FrontSlash");
                    comboCounter++;
                }else if (comboCounter >= 1)
                {
                    anim.Play("SingleHandBackSlash");
                    //Debug.Log("BackSlash");
                    comboCounter = 0;
                }
                Collider2D[] ennemiesToDamage;
                // Si l'attaque de l'arme est en forme de sphere
                if(itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().shape == "Sphere")
                {// récupère la liste des Collider 2D des ennemis à endommager en fonction de la taille de l'attaque de l'arme
                    ennemiesToDamage = Physics2D.OverlapCircleAll(itemList.items[itemSlots.weaponSlot].attackAreaSimple.transform.position,
                        itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeSphere, whatIsEnnemy);
                }
                else
                {// Sinon si l'attaque est en forme de cube
                    ennemiesToDamage = Physics2D.OverlapBoxAll(itemList.items[itemSlots.weaponSlot].attackAreaSimple.transform.position,
                        new Vector2(itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeX, itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeY),
                        whatIsEnnemy);
                }
                
                for(int i=0; i<ennemiesToDamage.Length; i++)
                {
                    if(ennemiesToDamage[i].tag == "Enemy")
                    {// Parcourt la liste des ennemis à endommager
                        ennemiesToDamage[i].GetComponent<BattleStats>().takeDamage(battleStats.finalAttackDamage());
                        ennemiesToDamage[i].GetComponent<BattleStats>().hitStun(gameObject, 0.1f, 0.2f);
                    }
                }
                return battleStats.finalAttackDelay();
            default:
                return 0;
        }
    }

    public float attack2()
    {
        switch (itemSlots.weaponSlot)
        {
            case 1:
                return battleStats.finalAttackDelay();
                break;
            case 2: // Hache TEST
                anim.SetBool("IsM2Released", true);
                anim.Play("DoubleHandSlash hit");

                Collider2D[] ennemiesToDamage;
                // Si l'attaque de l'arme est en forme de sphere
                if (itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().shape == "Sphere")
                {// récupère la liste des Collider 2D des ennemis à endommager en fonction de la taille de l'attaque de l'arme
                    ennemiesToDamage = Physics2D.OverlapCircleAll(itemList.items[itemSlots.weaponSlot].attackAreaSimple.transform.position,
                        itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeSphere, whatIsEnnemy);
                }
                else
                {// Sinon si l'attaque est en forme de cube
                    ennemiesToDamage = Physics2D.OverlapBoxAll(itemList.items[itemSlots.weaponSlot].attackAreaSimple.transform.position,
                        new Vector2(itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeX, itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeY),
                        whatIsEnnemy);
                }

                for (int i = 0; i < ennemiesToDamage.Length; i++)
                {
                    if (ennemiesToDamage[i].tag == "Enemy")
                    {// Parcourt la liste des ennemis à endommager
                        ennemiesToDamage[i].GetComponent<BattleStats>().takeDamage(battleStats.finalAttackDamage() * 5);
                        ennemiesToDamage[i].GetComponent<BattleStats>().hitStun(gameObject, 0.6f, 0.6f);
                    }
                }
                return battleStats.finalAttackDelay();
                break;
            default:
                return 2;
                break;
        }
    }

    public float attack2Release()
    {
        return 1;
    }

/*
    public float attack2()
    {
        switch (itemSlots.weaponSlot)
        {
            case 1:
                anim.SetBool("IsM2Released", false);
                anim.Play("DoubleHandSlash start");

                return battleStats.finalAttackDelay();
            default:
                return 0;
        }
    }

    public float attack2Release()
    {

        switch (itemSlots.weaponSlot)
        {
            case 1:
            case 2: // Hache TEST
                anim.SetBool("IsM2Released", true);
                anim.Play("DoubleHandSlash hit");

                Collider2D[] ennemiesToDamage;
                // Si l'attaque de l'arme est en forme de sphere
                if (itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().shape == "Sphere")
                {// récupère la liste des Collider 2D des ennemis à endommager en fonction de la taille de l'attaque de l'arme
                    ennemiesToDamage = Physics2D.OverlapCircleAll(itemList.items[itemSlots.weaponSlot].attackAreaSimple.transform.position,
                        itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeSphere, whatIsEnnemy);
                }
                else
                {// Sinon si l'attaque est en forme de cube
                    ennemiesToDamage = Physics2D.OverlapBoxAll(itemList.items[itemSlots.weaponSlot].attackAreaSimple.transform.position,
                        new Vector2(itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeX, itemList.items[itemSlots.weaponSlot].attackAreaSimple.GetComponent<attackArea>().attackRangeY),
                        whatIsEnnemy);
                }

                for (int i = 0; i < ennemiesToDamage.Length; i++)
                {
                    if (ennemiesToDamage[i].tag == "Enemy")
                    {// Parcourt la liste des ennemis à endommager
                        ennemiesToDamage[i].GetComponent<BattleStats>().takeDamage(battleStats.finalAttackDamage() * 5);
                        ennemiesToDamage[i].GetComponent<BattleStats>().hitStun(gameObject, 0.6f, 0.6f);
                    }
                }

                return battleStats.finalAttackDelay() * 2;
            default:
                return 1;
        }
    }*/

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
