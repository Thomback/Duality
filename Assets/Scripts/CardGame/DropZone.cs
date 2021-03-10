using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Draggable.Slot typeOfItem = Draggable.Slot.WEAPON;


    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        if(eventData.pointerDrag == null)
        {
            return;
        }
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            if (typeOfItem == d.typeOfItem || typeOfItem == Draggable.Slot.INVENTORY)
            {
                d.placeholderParent = this.transform;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        if (eventData.pointerDrag == null)
        {
            return;
        }
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeholderParent== this.transform)
        {
            if (typeOfItem == d.typeOfItem || typeOfItem == Draggable.Slot.INVENTORY)
            {
                d.placeholderParent = d.parentToReturnTo;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log (eventData.pointerDrag.name + "was dropped on " + gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            Destroy(eventData.pointerDrag);
            Destroy(d.placeholder);
            if (typeOfItem == d.typeOfItem || typeOfItem == Draggable.Slot.INVENTORY)
            {
                d.parentToReturnTo = this.transform;
            }
        }
    }
}
