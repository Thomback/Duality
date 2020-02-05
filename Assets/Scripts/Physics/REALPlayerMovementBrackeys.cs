using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REALPlayerMovementBrackeys : MonoBehaviour
{
    Animator anim;

    public CharacterControllerBrackeys controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        if(Mathf.Abs(horizontalMove) != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
