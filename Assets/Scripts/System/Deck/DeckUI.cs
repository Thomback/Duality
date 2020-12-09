using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckUI : MonoBehaviour
{

    public Deck deck;
    public List<Card> cards = new List<Card>();

    public GameObject holder;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        deck.SetDeck(cards);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Card currentCard = deck.UseCard();
            currentCard.use();
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            deck.Shuffle();
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        while(holder.transform.childCount > 0)
        {
            Destroy(holder.transform.GetChild(0));
        }
        List<Card> cardsList = deck.getDeck();
        for (int i = 0; i < cardsList.Count; i++)
        {
            GameObject go = Instantiate(prefab, holder.transform);
            go.transform.GetChild(0).GetComponent<Text>().text = cardsList[i].name;
            go.transform.GetChild(1).GetComponent<Image>().sprite = cardsList[i].image;
            go.transform.GetChild(2).GetComponent<Text>().text = "Description : " + cardsList[i].description;
            go.transform.GetChild(3).GetComponent<Text>().text = "None";
        }
    }
}
