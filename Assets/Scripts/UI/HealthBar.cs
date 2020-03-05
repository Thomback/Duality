using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public BattleStats statsJoueur;
    public TMPro.TextMeshProUGUI TexMex;

    static float t = 0.0f;

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = statsJoueur.maxHP;
        //slider.value = statsJoueur.currentHP;
        TexMex.text = statsJoueur.currentHP.ToString() + "/"+ statsJoueur.maxHP.ToString();

        if(slider.value > statsJoueur.currentHP)
        {
            slider.value -= 0.2f;
        }
    }
}
