using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemId;
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public bool itemIsWeapon;               // if False, is equipment
    public bool ItemIsEquipped = false;


    public Item(int id, string name, string description, Sprite sprite, bool isWeapon)
    {
        this.itemId = id;
        this.itemName = name;
        this.itemDescription = description;
        this.itemSprite = sprite;
        this.itemIsWeapon = isWeapon;
    }

}
