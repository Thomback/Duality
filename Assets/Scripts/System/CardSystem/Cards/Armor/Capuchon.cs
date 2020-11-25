using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capuchon : CardParent
{
    public new void HasBeenUsedLastRound()
    {
        throw new System.NotImplementedException();
    }

    public override void use()
    {
        Debug.Log("Je suis un capuchon !!! et ma valeur est de " + value);
    }
}
