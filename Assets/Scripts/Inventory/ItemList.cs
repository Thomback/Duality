using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Resources;

public class ItemList : MonoBehaviour
{
    public Item[] items;

    private void Start()
    {
        for(int i= 0; i<items.Length; i++)
        {
            items[i].itemId = i;
        }
    }
}
