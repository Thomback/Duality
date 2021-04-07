using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image cooldown;
    public PlayerMovementBrackeys statsJoueur;

    void Start()
    {

    }

    void FixedUpdate()
    {
        cooldown.fillAmount = ( statsJoueur.heavyAttackCooldown / statsJoueur.startHeavyAttackCooldown );
    }
}
