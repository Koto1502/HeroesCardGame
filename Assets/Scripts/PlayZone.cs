using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayZone : MonoBehaviour, IDropHandler
{
    //public List<Card> cards = new List<Card>();
    //public Transform[] positions = new Transform[6];
    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;
        Card card = obj.GetComponent<Card>();
        if (card != null)
        {
            //to do : enemy dropcard
            GameController.instance.playerHand.PlayCard(card);
        }
    }
}
