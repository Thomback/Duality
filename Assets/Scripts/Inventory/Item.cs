using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [HideInInspector]
    public int itemId;
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int slotLocation;               // if 0, is equipment, 1 is equipment1, 2 is equipment2, 3 is both
    public GameObject attackAreaSimple;
    public GameObject attackAreaLourde;
    public GameObject objectInScene;

    public Item(string name, string description, Sprite sprite, int slotLocation)
    {
        this.itemName = name;
        this.itemDescription = description;
        this.itemSprite = sprite;
        this.slotLocation = slotLocation;
    }

    public Item(string name, string description, Sprite sprite, int slotLocation, GameObject attackAreaSimple, GameObject attackAreaLourde)
    {
        this.itemName = name;
        this.itemDescription = description;
        this.itemSprite = sprite;
        this.slotLocation = slotLocation;
        this.attackAreaSimple = attackAreaSimple;
        this.attackAreaLourde = attackAreaLourde;
    }

    public Item(string name, string description, Sprite sprite, int slotLocation, GameObject attackAreaSimple, GameObject attackAreaLourde, GameObject object3d)
    {
        this.itemName = name;
        this.itemDescription = description;
        this.itemSprite = sprite;
        this.slotLocation = slotLocation;
        this.attackAreaSimple = attackAreaSimple;
        this.attackAreaLourde = attackAreaLourde;
        this.objectInScene = object3d;
    }

}
