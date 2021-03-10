using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlots : MonoBehaviour
{
    public int weaponSlot = 0;         //ID de l'arme équipée
    public int equipmentSlot1 = 0;     //ID du premier equipement (haut du corps)
    public int equipmentSlot2 = 0;     //ID du deuxième equipement (bas du corps)
    public ItemList listeItems;

    private BattleStats battleStats;

    private void Start()
    {
        battleStats = GetComponent<BattleStats>();
    }


    public void changeItem(int newItem)
    {
        battleStats.resetModifiers();
        switch (listeItems.items[newItem].slotLocation)
        {
            case 0:
                weaponSlot = newItem;
                break;
            case 1:
                equipmentSlot1 = newItem;
                break;
            case 2:
                equipmentSlot2 = newItem;
                break;
            case 3:
                equipmentSlot1 = newItem;
                equipmentSlot2 = newItem;
                break;
            default:
                break;
        }
        itemModifiers();
    }

    public void itemModifiers()
    {
        switch (weaponSlot)
        {
            case 0:
                battleStats.flatJumpForceIncrease += 10;
                battleStats.attackDelay = 0.5f;
                break;
            default:
                break;
        }

        switch (equipmentSlot1)
        {
            default:
                break;
        }

        switch (equipmentSlot2)
        {
            default:
                break;
        }
    }


}
