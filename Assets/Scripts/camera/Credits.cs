using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 4;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1;
        }
    }
}
