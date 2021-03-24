using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{

    public Card card;

    // Start is called before the first frame update
    void Start()
    {
        if (card == null)
        {
            throw new System.Exception("Veuillez assignée une carte à cet objet");
        }
        card.use();
    }
}
