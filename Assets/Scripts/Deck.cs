using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck
{
    public List<CardData> cardDatas = new List<CardData>();

    public void Create(List<CardData> cards)
    {
        //Create a list of cards in order
        List<CardData> cardDataInOrder = new List<CardData>();
        foreach (CardData cardData in cards)
        {
            for(int i = 0; i < cardData.numberInDeck; i++)
            {
                cardDataInOrder.Add(cardData);
            }
        }


        //Reorder the cards

        while (cardDataInOrder.Count > 0)
        {
            int randomIndex = Random.Range(0, cardDataInOrder.Count);
            cardDatas.Add(cardDataInOrder[randomIndex]);
            cardDataInOrder.RemoveAt(randomIndex);
        }
    }

    private CardData RandomCard()
    {
        CardData result = null;
        if (cardDatas.Count == 0)
        {
            if (!GameController.instance.player.isTurn)
            {
                GameController.instance.Reshuffle(GameController.instance.playerDiscard,GameController.instance.playerDeck);
            }
            else
            {
                GameController.instance.Reshuffle(GameController.instance.enemyDiscard, GameController.instance.enemyDeck);
            }
        }
        result = cardDatas[0];
        cardDatas.RemoveAt(0);
        return result;

    }

    private Card CreateNewCard(Vector3 position)
    {
        CardData randomCard = RandomCard();
        GameObject newCard = GameObject.Instantiate(GameController.instance.cardPrefab,
                                                        GameController.instance.canvas.gameObject.transform);
            newCard.transform.position = position;
            Card card = newCard.GetComponent<Card>();
            if (card)
            {
                card.cardData = randomCard;
                card.Initialise();
                return card;
            }
            else
            {
                Debug.LogError("no card found");
                return null;
            }
    }

    internal void DealCard(Hand hand)
    {
        for(int h=0; h<5; h++)
        {
            if (hand.cards[h] == null)
            {
                hand.cards[h] = CreateNewCard(hand.positions[h].position);
                return;
            }
        }
    }
}
