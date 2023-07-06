using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
       
    }


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        transform.position = Input.mousePosition;
    }


    public void OnEndDrag (PointerEventData evenData) //Al soltar el agarre
    {
        Debug.Log("OnEndDrag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        
    }
  
}