using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{

    public static DeckManager instance = null;

    // The deck where you gonna pick your cards
    [SerializeField]
    public Deck deck;
    // The deck of the used cardss
    [SerializeField]
    public Deck deadDeck;
    // The card you have in hand
    [SerializeField]
    public Deck currentHand;

    [SerializeField]
    int currentCardLimit = 5;
    public int cardLimit {
        get
        {
            return currentCardLimit;
        }
        set
        {
            if (value <= 0) currentCardLimit = 1;
            else if (value >= deck.cardCount) currentCardLimit = deck.cardCount;
            else currentCardLimit = value;
        }
    }

    void Awake()
    {
        deadDeck.Shuffle();

        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Take the first card of the deck and put it in side your hand
    public void PickUpCard()
    {
        if (currentHand.cardCount > currentCardLimit) return;
        // If there is no card in the deck we shuffle and put the deadDeck inside it
        if (deck.cardCount == 0)
        {
            deadDeck.Shuffle();
            deck.SetDeck(deadDeck.getDeck());
            deadDeck.ClearDeck();
        }
        Card currentCard = deck.PopCard();
        currentHand.AddCard(currentCard);
        deck.RemoveCard(currentCard);
    }

    // Take the first card of the deck and put it in side your hand
    public void FullHand()
    {
        if (currentHand.cardCount > currentCardLimit) return;
        // If there is no card in the deck we shuffle and put the deadDeck inside it
        if (deck.cardCount == 0)
        {
            deadDeck.Shuffle();
            deck.SetDeck(deadDeck.getDeck());
            deadDeck.ClearDeck();
        }
        // We store the current deckCardCount before changing it by removing card inside it
        int currentDeckCardCount = deck.cardCount;
        for (int i = 0; i < currentCardLimit && i < currentDeckCardCount; ++i)
        {
            Card currentCard = deck.PopCard();
            currentHand.AddCard(currentCard);
            deck.RemoveCard(currentCard);
        }
    }

    // Use the first card in the hand
    public void UseCard()
    {
        UseCard(0);
    }

    // Use the first card in the hand
    public void UseCard(int position)
    {
        if (currentHand.cardCount == 0) return;
        Card currentCard = currentHand.PopCard(position);
        if (currentCard.cardType == Card.CardType.Magic)
        {
            currentCard.use();
        }
        else
        {
            currentCard.equip();
        }
        currentHand.RemoveCard(currentCard);
        deadDeck.AddCard(currentCard);
    }
}
