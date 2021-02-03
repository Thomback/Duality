using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckUI : MonoBehaviour
{
    public GameObject handHolder;
    public GameObject deckHolder;
    public GameObject deadDeckHolder;
    public GameObject prefab;

    Color[] cardColors = { Color.green, Color.red, Color.cyan};

    // Start is called before the first frame update
    void Start()
    {
        DeckManager.instance.deck.Shuffle();
        int cardCount = DeckManager.instance.deck.cardCount;
        for (int i = 0; i < 5 && i < cardCount; ++i)
        {
            DeckManager.instance.PickUpCard();
        }
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (DeckManager.instance.currentHand.cardCount == 0)
            {
                DeckManager.instance.FullHand();
                UpdateUI();
                return;
            }
            DeckManager.instance.UseCard();
            UpdateUI();
        }

       /* if (Input.GetKeyDown(KeyCode.A))
        {
            UpdateUI();
        }*/
    }

    void UpdateUI()
    {
        UpdateDeck(deckHolder, DeckManager.instance.deck.getDeck());
        UpdateDeck(handHolder, DeckManager.instance.currentHand.getDeck());
        UpdateDeck(deadDeckHolder, DeckManager.instance.deadDeck.getDeck());
    }

    void UpdateDeck(GameObject holder, List<Card> cardsList)
    {
        int count = holder.transform.childCount - 1;
        for (int i = count; i >= 0; i--)
        {
            Destroy(holder.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < cardsList.Count; i++)
        {
            GameObject go = Instantiate(prefab, holder.transform);
            go.GetComponent<Image>().color = cardColors[(int)cardsList[i].cardType];
            go.transform.GetChild(0).GetComponent<Text>().text = cardsList[i].name;
            go.transform.GetChild(1).GetComponent<Image>().sprite = cardsList[i].image;
            go.transform.GetChild(2).GetComponent<Text>().text = "Description : " + cardsList[i].description;
            go.transform.GetChild(3).GetComponent<Text>().text = cardsList[i].value.ToString();
        }
    }
}
