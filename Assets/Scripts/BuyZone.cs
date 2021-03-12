using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyZone : MonoBehaviour, IDropHandler
{
    //public List<Card> cards = new List<Card>();
    //public Transform[] positions = new Transform[6];
    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;
        Card card = obj.GetComponent<Card>();
        if (card != null)
        {
            GameController.instance.shopHand.BuyCard(card, GameController.instance.playerDiscard);
        }
    }
}
