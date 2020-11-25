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

    public virtual void use()
    {
        Debug.Log("Je suis une carte classique");
    }

    public virtual void HasBeenUsedLastRound()
    {
        return;
    }

    public void init(int value, ArmorType armorType, MagicType magicType)
    {
        this.value = value;
        this.armorType = armorType;
        this.magicType = magicType;
    }
}
