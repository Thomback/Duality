using UnityEngine;

[System.Serializable]
public class CardParent
{
    [SerializeField]
    protected int value;

    protected enum ArmorType { upper, lower, both }
    [SerializeField]
    protected ArmorType armorType;

    protected enum MagicType { Enchant, debuff, skill}
    [SerializeField]
    protected MagicType magicType;



    public virtual void use()
    {
        Debug.Log("Je suis une carte classique");
    }

    public virtual void HasBeenUsedLastRound()
    {
        return;
    }
}
