using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPrefab : MonoBehaviour
{
    public Card card;
    public int position = 0;

    public void UseCard()
    {
        DeckManager.instance.UseCard(position);
        GameObject.FindWithTag("UI").GetComponent<DeckUI>().UpdateUI();
    }
}
