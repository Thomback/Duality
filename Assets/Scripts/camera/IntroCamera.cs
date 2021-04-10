using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    private Camera cameraIntro;
    private float accelerator = 0;
    // Start is called before the first frame update
    void Start()
    {
        cameraIntro = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraIntro.orthographicSize > 6)
        {
            cameraIntro.orthographicSize -= accelerator;
            gameObject.transform.Rotate(0, 0, accelerator);
            accelerator += 0.0001f;
        }
    }
}
