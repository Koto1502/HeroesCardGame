using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hand
{
    public List<Card> cards = new List<Card>();
    public List<Transform> positions = new List<Transform>();
    //public string[] animNames = new string[5];
    public bool isPlayer = false;
    public bool isBoard = false;
    public bool isShop = false;
    public int totalDam = 0;
    public int totalCoin = 0;

    public void PlayCard(Card card)
    {
        for(int i=0; i<5; i++)
        {

            if(cards[i] == card)
            {   
                //UPDATE CARD POSITION AND LIST
                GameController.instance.board.cards.Add(cards[i]);
                int boardCount = GameController.instance.board.cards.Count;
                cards[i].transform.position = GameController.instance.board.positions[boardCount-1].position;

                cards[i] = null;

                //USE CARD'S DAM and COIN
                GameController.instance.totalDamage += card.cardData.damage;
                GameController.instance.totalCoin += card.cardData.gold;

                //UPDATE DAMAGE
                GameController.instance.updateDamage();
                GameController.instance.updateCoin();

            }
        }
    }
    public void BuyCard(Card card,Deck discardDeck)
    {
        if (isShop)
        {
            for (int i = 0; i < 5; i++)
            {
                if (cards[i] == card)
                {
                    //Check Cost
                    if (cards[i].cardData.cost > GameController.instance.totalCoin)
                    {
                        Debug.Log("no money");
                        UnityEngine.Object.Destroy(cards[i].gameObject);
                        return;
                    }
                    //Minus gold
                    GameController.instance.totalCoin -= cards[i].cardData.cost;
                    //UPDATE gold
                    GameController.instance.updateCoin();

                    //Reposition
                    UnityEngine.Object.Destroy(cards[i].gameObject);

                    //Buy Card
                    discardDeck.cardDatas.Add(cards[i].cardData);
                    cards[i] = null;

                    //DEAL AGAIN
                    GameController.instance.shopDeck.DealCard(this);
                }
            }

        }
    }

    //clear board: discard deck, reposition, 
    public void Discard(Deck disDeck, Transform disPosition) 
    {
        for (int i = 0; i < this.cards.Count; i++)
        {
            //Debug.Log(cards[i].cardData.cardTitle + i.ToString());
            if(cards[i] != null)
            {
                UnityEngine.Object.Destroy(cards[i].gameObject);
                disDeck.cardDatas.Add(this.cards[i].cardData);
                cards[i] = null;
            }
        }
    }
}
