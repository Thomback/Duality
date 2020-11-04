using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public TMPro.TextMeshProUGUI TexMex;

    private BattleStats enemyStats;

    private void Start()
    {
        enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BattleStats>();
        Debug.Log(GameObject.FindGameObjectWithTag("Enemy").name + " found.");

        slider.value = enemyStats.currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = enemyStats.maxHP;

        TexMex.text = enemyStats.gameObject.name + " : " + enemyStats.currentHP.ToString() + "/" + enemyStats.maxHP.ToString();

        if (slider.value > enemyStats.currentHP)
        {
            slider.value -= 1f;
        }
    }
}
