using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public bool esAprobado;
    public Transform contenedor;

    public void OnDrop(PointerEventData eventData)
    {
        EstudianteDragHandler dragHandler = eventData.pointerDrag.GetComponent<EstudianteDragHandler>();

        if (dragHandler != null)
        {
            dragHandler.transform.SetParent(contenedor);
            dragHandler.transform.position = contenedor.position;

            EstudiantesManager estudiantesManager = FindObjectOfType<EstudiantesManager>();
            estudiantesManager.IncrementarContador(dragHandler.esAprobado);
        }
    }
}
