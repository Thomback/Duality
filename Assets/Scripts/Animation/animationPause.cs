using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationPause : MonoBehaviour
{
    public void Pause(Animator anim)
    {
        anim.speed = 0;
    }
}
