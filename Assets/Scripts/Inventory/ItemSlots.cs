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
    private PlayerMovementBrackeys playerMovementBrackeys;
    private CharacterControllerBrackeys characterControllerBrackeys;

    private static ItemSlots Instance = null;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        battleStats = GetComponent<BattleStats>();
        playerMovementBrackeys = GetComponent<PlayerMovementBrackeys>();
        characterControllerBrackeys = GetComponent<CharacterControllerBrackeys>();
        Instance = this;
    }

    public void changeItem(int newItem)
    {
        battleStats.resetModifiers();
        Item item = null;
        for (int i = 0; i < listeItems.items.Length; ++i)
        {
            if (listeItems.items[i].itemId == newItem)
            {
                item = listeItems.items[i];
            }
        }
        // Quitte le script si l'item n'est pas trouvé
        if (item == null) return;
        switch (item.slotLocation)
        {
            case 0:
                weaponSlot = newItem;
                listeItems.items[1].objectInScene.GetComponent<MeshRenderer>().enabled = false;
                listeItems.items[2].objectInScene.GetComponent<MeshRenderer>().enabled = false;
                listeItems.items[3].objectInScene.GetComponent<MeshRenderer>().enabled = false;
                if (!listeItems.items[item.itemId].objectInScene.Equals(null))
                    listeItems.items[item.itemId].objectInScene.GetComponent<MeshRenderer>().enabled = true;
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
        GameObject.FindWithTag("UI").GetComponent<ItemSlotsUI>().UpdateSlots();
        itemModifiers();
    }

    public void itemModifiers()
    {
        switch (weaponSlot)
        {
            case 1://Axe
                battleStats.attackDamage = 20;
                battleStats.attackDelay = 1.3f;
                break;
            case 2://Sword
                battleStats.attackDamage = 10;
                battleStats.attackDelay = 0.5f;
                break;
            case 3://Lance
                battleStats.attackDamage = 10;
                battleStats.attackDelay = 0.9f;
                break;
            default:
                break;
        }

        switch (equipmentSlot1)
        {
            case 10:
                // capuche
                battleStats.resetModifiers();

                Debug.Log("vitesse+");
                battleStats.runSpeedIncrease = 25;
                break;

            case 11:
                // casques
                battleStats.resetModifiers();

                Debug.Log("attaque+");
                battleStats.attackDamageIncrease = 10;
                break;

            case 16:
                // seringue
                battleStats.resetModifiers();

                Debug.Log("saut+");
                battleStats.jumpForceIncrease = 25;
                break;

            case 17:
                // armure
                battleStats.resetModifiers();

                Debug.Log("defense+");
                battleStats.dmgReduction = +10;
                break;

            default:
                break;
        }

        switch (equipmentSlot2)
        {
            case 12:
                // bottes
                Debug.Log("double saut");
                playerMovementBrackeys.capacityOn = false;
                characterControllerBrackeys.canWallJump = false;

                break;
            case 13:
                // pantalon
                Debug.Log("dash");
                playerMovementBrackeys.capacityOn = true;
                characterControllerBrackeys.canWallJump = false;

                break;
            case 14:
                // bouclier 
                Debug.Log("resistence projectile");
                playerMovementBrackeys.capacityOn = false;
                characterControllerBrackeys.canWallJump = false;

                break;
            case 15:
                // walljump
                Debug.Log("walljump");
                playerMovementBrackeys.capacityOn = false;
                characterControllerBrackeys.canWallJump = true;

                break;
            default:
                break;
        }
    }


}
