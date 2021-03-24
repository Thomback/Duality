﻿using System;
using UnityEngine;

[System.Serializable]
public class CardParent
{
    [SerializeField]
    public int value;

    public enum ArmorType { upper, lower, both }
    [SerializeField]
    public ArmorType armorType;

    public enum MagicType { Enchant, debuff, skill}
    [SerializeField]
    public MagicType magicType;

    [SerializeField]
    bool usableOnTime;

    ItemSlots itemSlots;

    Card parentCard;

    public virtual void use()
    {
        if (parentCard.cardType == Card.CardType.Magic)
        {
            Debug.Log("Je suis une carte magique");
            return;
        }
        itemSlots.changeItem(parentCard.itemID);
        Debug.Log("Je suis une carte classique");
    }

    public virtual void HasBeenUsedLastRound()
    {
        return;
    }

    public void init(Card parentCard, int value, ArmorType armorType, MagicType magicType)
    {
        if(GameObject.FindWithTag("GameController") == null) throw new NullReferenceException("Il n'y a aucun object GameController sur cette scène");
        itemSlots = GameObject.FindWithTag("Player").GetComponent<ItemSlots>();
        this.parentCard = parentCard;
        this.value = value;
        this.armorType = armorType;
        this.magicType = magicType;
    }
}