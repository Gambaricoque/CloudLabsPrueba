using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour , IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragHandler dragHandler = dropped.GetComponent<DragHandler>();
        dragHandler.parentAfterDrag = transform;
    }

}
