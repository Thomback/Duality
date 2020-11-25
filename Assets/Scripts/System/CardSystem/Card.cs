using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class Card : ScriptableObject
{
    // Nom de la carte et de la classe associé
    public new string name;
    public string description;
    public Sprite image;
    public enum CardType { Armor, Weapon, Magic}
    [SerializeField]
    private CardType type;
    [SerializeField]
    private CardParent cardScript = null;
    [SerializeField]
    private bool useOverrideScript = false;
    private bool overriedYet = false;

    public delegate void LastMethod();
    LastMethod lastMethod;

    void Init()
    {
        overriedYet = true;
        Debug.Log("Initialisation de la carte...");
        if (name == null)
        {
            throw new Exception("La carte n'est pas valide, elle ne possède pas de nom");
        }
        // Récupère l'instance de la classe portant le même nom de la carte, et appel son constructeur
        Type type = Type.GetType(name);
        if (type == null)
        {
            throw new Exception("La classe associée '" + name + "' n'existe pas, vérifier que le nom de votre carte est correcte");
        }
        cardScript = (CardParent)System.Activator.CreateInstance(type);

        // Si la method init a été appellée depuis une autre méthode, on relance cette méthode
        lastMethod?.Invoke();
    }

    public void use()
    {
        // Si le script associé a cette carte n'est pas défini, on l'initialise
        if (useOverrideScript && !overriedYet)
        {
            lastMethod = use;
            Init();
            return;
        }
        Debug.Log(cardScript);
        Debug.Log("Utilisation de la carte...");
        // Lance l'abilité de la carte
        cardScript.use();
        lastMethod = null;
        overriedYet = false;
    }
}
