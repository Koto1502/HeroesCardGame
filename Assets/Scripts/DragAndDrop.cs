using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (GameController.instance.player.isTurn)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if ((eventData.pointerCurrentRaycast.gameObject.name != "PlayZone" && eventData.pointerCurrentRaycast.gameObject.name != "DiscardDeck")|| gameObject.name == "PlayerDamage" )
        { 
            
            transform.position = originalPosition;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        
    }
}
