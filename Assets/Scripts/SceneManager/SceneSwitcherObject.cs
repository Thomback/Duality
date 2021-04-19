﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcherObject : MonoBehaviour
{
    bool hasSwitch;

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;
            SceneController.Instance.FadeToScene("Interlude");
        }
    }
}
