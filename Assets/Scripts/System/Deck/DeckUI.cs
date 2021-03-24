using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckUI : MonoBehaviour
{
    public GameObject handHolder;
    public GameObject prefab;

    Color[] cardColors = { Color.red, Color.green, Color.cyan};

    // Start is called before the first frame update
    void Start()
    {
        DeckManager.instance.deck.Shuffle();
        int cardCount = DeckManager.instance.deck.cardCount;
        for (int i = 0; i < 5 && i < cardCount; ++i)
        {
            DeckManager.instance.deck.PopCard().Init();
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
    }

    public void UpdateUI()
    {
        UpdateDeck(handHolder, DeckManager.instance.currentHand.getDeck());
    }

    public void SwapHandUI(bool active)
    {
        handHolder.SetActive(active);
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
            CardPrefab cardPref = go.GetComponent<CardPrefab>();
            cardPref.card = cardsList[i];
            cardPref.position = i;
            go.GetComponent<Image>().color = cardColors[(int)cardsList[i].cardType];
            go.transform.GetChild(0).GetComponent<Text>().text = cardsList[i].name;
            go.transform.GetChild(1).GetComponent<Image>().sprite = cardsList[i].image;
            go.transform.GetChild(2).GetComponent<Text>().text = "Description : " + cardsList[i].description;
            go.transform.GetChild(3).GetComponent<Text>().text = cardsList[i].value.ToString();
        }
    }
}
