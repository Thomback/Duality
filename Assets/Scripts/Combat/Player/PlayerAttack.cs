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
    public GameObject weaponTracing;

    private Animator tracingAnim;

    private bool isSpinning = false;
    private float timer;
    private float tickTimer;

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
        if (weaponTracing.Equals(null))
            weaponTracing = GameObject.Find("WeaponTracing");

        tracingAnim = weaponTracing.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isSpinning)  // Heavy Axe attack timer
        {
            timer -= Time.deltaTime;
            if(timer <= 0)// Si fin de l'attaque
            {

                SoundScript.PlaySound("sword");
                tracingAnim.SetBool("AxeHeavy", false);
                isSpinning = false;
            }
            else
            {
                tickTimer -= Time.deltaTime;

                if(tickTimer <= 0)
                {
                    SoundScript.PlaySound("sword");

                    DamageInflicter(0.5f, 0.1f, 0.2f, EnnemiesToDamage(true));

                    tickTimer = 0.5f;
                }
            }
        }
    }


    public float attack1()
    {
        switch (itemSlots.weaponSlot)
        {
            case 1: //Axe
                anim.Play("DoubleHandSlash");
                tracingAnim.SetTrigger("AxeLight");
                SoundScript.PlaySound("sword");

                DamageInflicter(1, 0.1f, 0.6f, EnnemiesToDamage(false));

                return battleStats.finalAttackDelay();
            case 2: //Sword
                
                anim.Play("SingleHandSlash");
                tracingAnim.SetTrigger("SwordLight");
                SoundScript.PlaySound("sword");
                //Debug.Log("FrontSlash");

                //Collider2D[] ennemiesToDamage = EnnemiesToDamage(false);

                /*
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
                }*/
                /*
                for(int i=0; i<ennemiesToDamage.Length; i++)
                {
                    if(ennemiesToDamage[i].tag == "Enemy")
                    {// Parcourt la liste des ennemis à endommager
                        ennemiesToDamage[i].GetComponent<BattleStats>().takeDamage(battleStats.finalAttackDamage());
                        ennemiesToDamage[i].GetComponent<BattleStats>().hitStun(gameObject, 0.1f, 0.2f);
                        Debug.Log("Ennemy endommagé :" + ennemiesToDamage[i].name);
                    }
                }*/

                DamageInflicter(1, 0.1f, 0.2f, EnnemiesToDamage(false));

                return battleStats.finalAttackDelay();
            case 3: //Lance
                anim.Play("DoubleHandSlash");
                tracingAnim.SetTrigger("LanceLight");
                SoundScript.PlaySound("sword");

                DamageInflicter(1, 0.1f, 0.3f, EnnemiesToDamage(false));

                return battleStats.finalAttackDelay();
            default:
                return 0;
        }
    }

    public float attack2()
    {
        switch (itemSlots.weaponSlot)
        {
            case 1://Axe
                SoundScript.PlaySound("sword");
                tracingAnim.SetBool("AxeHeavy", true);
                isSpinning = true;

                DamageInflicter(0.5f, 0.1f, 0.2f, EnnemiesToDamage(true));

                tickTimer = 0.5f;
                timer = 3;
                return (3);//3 sec de tournoiement
            case 2: // Sword
                anim.SetBool("IsM2Released", true);
                anim.Play("DoubleHandSlash");
                tracingAnim.SetTrigger("SwordHeavy");
                SoundScript.PlaySound("sword");

                
                //Collider2D[] ennemiesToDamage = EnnemiesToDamage(true);

                /*
                // Si l'attaque de l'arme est en forme de sphere
                if (itemList.items[itemSlots.weaponSlot].attackAreaLourde.GetComponent<attackArea>().shape == "Sphere")
                {// récupère la liste des Collider 2D des ennemis à endommager en fonction de la taille de l'attaque de l'arme
                    ennemiesToDamage = Physics2D.OverlapCircleAll(itemList.items[itemSlots.weaponSlot].attackAreaLourde.transform.position,
                        itemList.items[itemSlots.weaponSlot].attackAreaLourde.GetComponent<attackArea>().attackRangeSphere, whatIsEnnemy);
                }
                else
                {// Sinon si l'attaque est en forme de cube
                    ennemiesToDamage = Physics2D.OverlapBoxAll(itemList.items[itemSlots.weaponSlot].attackAreaLourde.transform.position,
                        new Vector2(itemList.items[itemSlots.weaponSlot].attackAreaLourde.GetComponent<attackArea>().attackRangeX, itemList.items[itemSlots.weaponSlot].attackAreaLourde.GetComponent<attackArea>().attackRangeY),
                        whatIsEnnemy);
                }*/
                /*
                for (int i = 0; i < ennemiesToDamage.Length; i++)
                {
                    if (ennemiesToDamage[i].tag == "Enemy")
                    {// Parcourt la liste des ennemis à endommager
                        ennemiesToDamage[i].GetComponent<BattleStats>().takeDamage(battleStats.finalAttackDamage() * 5);
                        ennemiesToDamage[i].GetComponent<BattleStats>().hitStun(gameObject, 0.6f, 0.6f);
                    }
                }*/
                DamageInflicter(5, 0.6f, 0.6f, EnnemiesToDamage(true));
                return battleStats.finalAttackDelay() * 2;
            default:
                return 0.5f;
        }
    }

    public float attack2Release()
    {
        return 1;
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




    private Collider2D[] EnnemiesToDamage(bool isLourde)
    {
        Collider2D[] ennemiesToDamage;

        if (!isLourde)
        {
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
        }
        else
        {
            // Si l'attaque de l'arme est en forme de sphere
            if (itemList.items[itemSlots.weaponSlot].attackAreaLourde.GetComponent<attackArea>().shape == "Sphere")
            {// récupère la liste des Collider 2D des ennemis à endommager en fonction de la taille de l'attaque de l'arme
                ennemiesToDamage = Physics2D.OverlapCircleAll(itemList.items[itemSlots.weaponSlot].attackAreaLourde.transform.position,
                    itemList.items[itemSlots.weaponSlot].attackAreaLourde.GetComponent<attackArea>().attackRangeSphere, whatIsEnnemy);
            }
            else
            {// Sinon si l'attaque est en forme de cube
                ennemiesToDamage = Physics2D.OverlapBoxAll(itemList.items[itemSlots.weaponSlot].attackAreaLourde.transform.position,
                    new Vector2(itemList.items[itemSlots.weaponSlot].attackAreaLourde.GetComponent<attackArea>().attackRangeX, itemList.items[itemSlots.weaponSlot].attackAreaLourde.GetComponent<attackArea>().attackRangeY),
                    whatIsEnnemy);
            }
        }

        return ennemiesToDamage;
    }

    private void DamageInflicter(float damageMultiplicator, float knockBack, float hitStunSeconds, Collider2D[] ennemiesToDamage)
    {
        for (int i = 0; i < ennemiesToDamage.Length; i++)
        {
            if (ennemiesToDamage[i].tag == "Enemy")
            {// Parcourt la liste des ennemis à endommager
                ennemiesToDamage[i].GetComponent<BattleStats>().takeDamage(battleStats.finalAttackDamage() * damageMultiplicator);
                ennemiesToDamage[i].GetComponent<BattleStats>().hitStun(gameObject, knockBack, hitStunSeconds);
            }
        }
    }

}
