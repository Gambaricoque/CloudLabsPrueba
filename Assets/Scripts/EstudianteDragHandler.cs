using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EstudianteDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool esAprobado;

    private Transform ubicacionInicial;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        ubicacionInicial = transform.parent;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (eventData.pointerEnter != null)
        {
            Transform contenedor = eventData.pointerEnter.GetComponent<DropZone>()?.contenedor;

            if (contenedor != null)
            {
                transform.SetParent(contenedor);
                transform.position = contenedor.position;

                EstudiantesManager estudiantesManager = FindObjectOfType<EstudiantesManager>();
                estudiantesManager.IncrementarContador(esAprobado);
            }
            else
            {
                transform.SetParent(ubicacionInicial);
                transform.position = ubicacionInicial.position;
            }
        }
        else
        {
            transform.SetParent(ubicacionInicial);
            transform.position = ubicacionInicial.position;
        }
    }

    public bool EnUbicacionCorrecta()
    {
        return transform.parent.GetComponent<DropZone>()?.esAprobado == esAprobado;
    }
}
