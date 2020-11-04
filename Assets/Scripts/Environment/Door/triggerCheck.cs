using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCheck : MonoBehaviour
{
    enum TriggerType { Entree, SortieLeftSide, SortieRightSide}
    [SerializeField] private DoorManager doorManager;
    [SerializeField] private TriggerType triggerType;
    private bool enteredTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enteredTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enteredTrigger)
        {
            enteredTrigger = false;
            
            if (collision.CompareTag("Player"))
            {
                switch (triggerType)
                {
                    case TriggerType.Entree:
                        doorManager.justLeftEntrance();
                        break;
                    case TriggerType.SortieLeftSide:
                        doorManager.justLeftLeft();
                        break;
                    case TriggerType.SortieRightSide:
                        doorManager.justLeftRight();
                        break;
                }
            }
        }
    }
}
