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

    Card parentCard;

    public virtual void use()
    {
        switch (parentCard.cardType)
        {
            case Card.CardType.Armor:
                
                break;
            case Card.CardType.Magic:

                break;
            case Card.CardType.Weapon:

                break;
        }
        Debug.Log("Je suis une carte classique");
    }

    public virtual void HasBeenUsedLastRound()
    {
        return;
    }

    public void init(Card parentCard, int value, ArmorType armorType, MagicType magicType)
    {
        this.parentCard = parentCard;
        this.value = value;
        this.armorType = armorType;
        this.magicType = magicType;
    }
}
