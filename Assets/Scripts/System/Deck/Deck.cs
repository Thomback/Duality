using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    List<Card> deck = new List<Card>();

    [SerializeField]
    private int cardLimit = 40;
    public int cardLimite {
        get
        {
            return cardLimit;
        }
    }

    // Return the current count of cards
    public int cardCount {
        get
            {
                return deck.Count;
            }
        }

    // This function set the deck that will add the currentDeck
    public void SetDeck(List<Card> cards)
    {
        deck = new List<Card>(cards);
        foreach(Card card in deck)
        {
            card.Init();
        }
    }

    // Warning, return a reference
    public List<Card> getDeck()
    {
        return deck;
    }

    public void ClearDeck()
    {
        deck.Clear();
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

    public Card PopCard()
    {
        if (cardCount == 0) return null;
        return deck[0];
    }

    public Card PopCard(int index)
    {
        if (cardCount == 0 || index < 0 || index >= deck.Count) return null;
        return deck[index];
    }

    public void RemoveCard(Card card)
    {
        if (cardCount == 0) return;
        deck.Remove(card);
    }

    public void AddCard(Card card)
    {
        if (cardCount == cardLimit) return;
        deck.Add(card);
    }
}
