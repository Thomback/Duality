using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Resources;

public class ItemList : MonoBehaviour
{
    [HideInInspector]
    public Item[] items;

    private void Awake()
    {
        items = new Item[] {
            // ----- Starting Items ------ //
            new Item(   // Epee courte
            0,
            "Epée Courte",
            "Attention, elle est rouillée.",
            Resources.Load<Sprite>("Sprites/Items/EpeeCourte"),
            true
            ),

            new Item(   // Dague
            1,
            "Dague",
            "Une fine lame aiguisée. Un peu courte cependant.",
            Resources.Load<Sprite>("Sprites/Items/Dague"),
            true
            ),

            new Item(   // Capuche
            2,
            "Capuche",
            "Petit capuchon vert. Joli.",
            Resources.Load<Sprite>("Sprites/Items/Capuche"),
            false
            ),

            new Item(   // Casque en cuir
            3,
            "Casque en cuir",
            "Ce casque n'est pas très resistant, mais c'est mieux que rien.",
            Resources.Load<Sprite>("Sprites/Items/CasqueEnCuir"),
            false
            ),

            new Item(   // Pantalon
            4,
            "Pantalon",
            "C'est étrange, ce pantalon possède un design bien particulier," +
            " il semblerait presque sorti d'une autre époque.",
            Resources.Load<Sprite>("Sprites/Items/Pantalon"),
            false
            ),

            new Item(   // Bottes
            5,
            "Bottes",
            "Ces botines vous vont à ravir!",
            Resources.Load<Sprite>("Sprites/Items/Bottes"),
            false
            )
        };

    }
}
