using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotsUI : MonoBehaviour
{
    ItemSlots itemSlots;
    ItemList itemList;
    public GameObject holder;

    public void Start()
    {
        itemSlots = GameObject.FindWithTag("Player").GetComponent<ItemSlots>();
        itemList = GameObject.FindWithTag("GameController").GetComponent<ItemList>();
    }

    public void UpdateSlots()
    {
        Item[] listItem = { null, null, null };

        for (int i = 0; i < itemList.items.Length; ++i)
        {
            Item item = itemList.items[i];
            if (item.itemId == itemSlots.weaponSlot)
            {
                listItem[0] = item;
            }
            else if (item.itemId == itemSlots.equipmentSlot1)
            {
                listItem[1] = item;
            }
            else if (item.itemId == itemSlots.equipmentSlot2)
            {
                listItem[2] = item;
            }
        }

        for (int i = 0; i < 3; ++i)
        {
            Transform transform = holder.transform.GetChild(i);
            if (listItem[i] != null)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = listItem[i].itemSprite;
            }
        }
    }
}
