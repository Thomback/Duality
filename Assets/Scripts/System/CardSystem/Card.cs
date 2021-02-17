using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class Card : ScriptableObject
{
    public int itemID = 0; // Set it to -1 for magic
    private Item itemInstance = null;
    // Nom de la carte et de la classe associé
    public new string name;
    public string description;
    public Sprite image;
    public enum CardType { Weapon, Armor, Magic }
    public CardType cardType { get { return _type; } }
    public int value { get { return cardScriptData.value; } }
    [SerializeField]
    private CardType _type;
    [SerializeField]
    private CardParent cardScriptData = null;
    private CardParent cardScript = null;
    [SerializeField]
    private bool useOverrideScript = false;
    private bool overriedYet = false;

    public delegate void LastMethod();
    LastMethod lastMethod;

    // Card is init
    public void Init()
    {
        if (GameObject.FindWithTag("GameController") == null) throw new NullReferenceException("Il n'y a aucun object GameController sur cette scène");
        Debug.Log("I'm initalising the card");
        overriedYet = true;
        // if itemID is -1 then the card is a magic one
        if (itemID != -1)
        {
            ItemList itemList = GameObject.FindWithTag("GameController").GetComponent<ItemList>();
            for (int i = 0; i < itemList.items.Length; i++)
            {
                if (itemList.items[i].itemId == itemID)
                {
                    itemInstance = itemList.items[i];
                }
            }
            if (itemInstance == null)
            {
                throw new Exception("Aucun item associé à cette carte");
            }
            Debug.Log("Item Instance : " + itemInstance.itemName);
            // Get the value of the item and put it in the card
            name = itemInstance.itemName;
            description = itemInstance.itemDescription;
            image = itemInstance.itemSprite;
            switch (itemInstance.slotLocation)
            {
                case 0:
                    _type = CardType.Weapon;
                    break;
                case 1:
                    _type = CardType.Armor;
                    cardScriptData.armorType = CardParent.ArmorType.lower;
                    break;
                case 2:
                    _type = CardType.Armor;
                    cardScriptData.armorType = CardParent.ArmorType.upper;
                    break;
                case 3:
                    _type = CardType.Armor;
                    cardScriptData.armorType = CardParent.ArmorType.both;
                    break;
                default:
                    throw new Exception("Le slot '" + itemInstance.slotLocation + "' n'existe pas valide !");
            }
        }
        else
        {
            _type = CardType.Magic;
        }
        if (name == null)
        {
            throw new Exception("L'item n'est pas valide, elle ne possède pas de nom");
        }
        if (!overriedYet)
        {
            // Récupère l'instance de la classe portant le même nom de la carte, et appel son constructeur
            Type type = Type.GetType(name);
            if (type == null)
            {
                throw new Exception("La classe associée '" + name + "' n'existe pas, vérifier que le nom de votre item est correcte");
            }
            cardScript = (CardParent)Activator.CreateInstance(type);
        }
        else
        {
            cardScript = new CardParent();
        }
        cardScript.init(this, cardScriptData.value, cardScriptData.armorType, cardScriptData.magicType);

        // Si la method init a été appellée depuis une autre méthode, on relance cette méthode
        lastMethod?.Invoke();
    }

    public void use()
    {
        // Si le script associé a cette carte n'est pas défini, on l'initialise
        if (!overriedYet)
        {
            lastMethod = use;
            Init();
            return;
        }
        // Lance l'abilité de la carte
        cardScript.use();
        lastMethod = null;
        overriedYet = false;
    }
}
