using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    List<Card> deck = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCard(Card card)
    {
        deck.Add(card);
    }

    public void SetDeck(List<Card> cards)
    {
        deck = cards;
    }

    public List<Card> getDeck()
    {
        return deck;
    }

    public void Shuffle()
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            Card value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }

    public Card UseCard()
    {
        Card card = deck[0];
        deck.Remove(card);
        return card;
    }

}
