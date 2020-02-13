using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlots : MonoBehaviour
{
    private int weaponSlot = 0;         //ID de l'arme équipée
    private int equipmentSlot1 = 0;     //ID du premier equipement (haut du corps)
    private int equipmentSlot2 = 0;     //ID du deuxième equipement (bas du corps)
    public ItemList listeItems;

    private void Update()
    {
        switch (weaponSlot)
        {
            case 0:
                GetComponent<BattleStats>().jumpForce += 10;
                break;
            default:
                break;
        }
    }

    public void changeWeapon(int newItem)
    {
        
    }

}
