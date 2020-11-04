using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeIsDoor : MonoBehaviour
{
    Animator anim;

    [SerializeField] private DoorManager doorManager;
    [SerializeField] private bool isOpened = false;
    public bool isOpenable = true;
    public bool autoOpen = false;
    public bool autoClose = false;

    private bool isColliding = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isColliding && !isOpened && isOpenable)
        {
            if (Input.GetButtonDown("Action") && !autoOpen || autoOpen)
            {
                anim.SetBool("isOpened", true);
                isOpened = true;
            }
            
        }

        if(autoClose && !isColliding && isOpened)
        {
            anim.SetBool("isOpened", false);
            isOpened = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            isColliding = false;
    }

    private void unloadOldScene()
    {
        if (doorManager != null)
            doorManager.unloadOldRoom(this);
    }
}
