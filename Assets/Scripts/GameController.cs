using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    static public GameController instance = null;

    //DECKS
    public Deck playerDeck    = new Deck();
    public Deck enemyDeck     = new Deck();
    public Deck shopDeck      = new Deck();
    public Deck playerDiscard = new Deck();
    public Deck enemyDiscard  = new Deck();

    //HANDS
    public Hand board      = new Hand();
    public Hand enemyHand  = new Hand();
    public Hand playerHand = new Hand();
    public Hand shopHand   = new Hand();

    public List<CardData> cards = new List<CardData>();
    public List<CardData> startingCards = new List<CardData>();

    public Text turn = null;

    public GameObject cardPrefab = null;

    public Text playerDamage = null;
    public Text playerCoin = null;

    public Canvas canvas = null;

    public Player player = null;
    public Player enemy = null;

    public Transform discardPosition      = null;
    public Transform enemyDiscardPosition = null;

    public bool isPlayable = false;

    public int totalCoin = 0;
    public int totalDamage = 0;
    private void Awake()
    {
        instance = this;
        playerDeck.Create(startingCards);
        enemyDeck.Create(startingCards);
        shopDeck.Create(cards);

        StartCoroutine(DealHands());
    }

    private void Update()
    {
        if(player.health<= 0)
        {
            SceneManager.LoadScene(2);
        }
        else if (enemy.health <= 0)
        {
            SceneManager.LoadScene(2);
        }
        if (player.isTurn)
        {
            turn.text = "Your Turn";
        }
        else
        {
            turn.text = "Enemy's Turn";
        }
    }

    public Sprite[] shieldNumbers = new Sprite[10];
    public Sprite[] costNumbers = new Sprite[10];
    //public Sprite[] HealthNumbers = new Sprite[10];
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void SkipTurn()
    {
        if (player.isTurn)
        {
            //health
            enemy.health -= totalDamage;
            updateHealth();

            //dam
            totalDamage = 0;
            updateDamage();

            //coin
            totalCoin = 0;
            updateCoin();
            //Debug.Log(player.isTurn);

            //pass turn token
            player.isTurn = false;
            enemy.isTurn = true;
            //Debug.Log(player.isTurn);

            //clear board: discard deck, reposition, 
            board.Discard(playerDiscard,discardPosition);
            playerHand.Discard(playerDiscard, discardPosition);

            isPlayable = false;
        }
        else
        {
            //bot plays
            BotPlays();
            //health
            player.health -= totalDamage;
            updateHealth();

            //dam
            totalDamage = 0;
            updateDamage();

            //coin
            totalCoin = 0;
            updateCoin();
            //Debug.Log(player.isTurn);

            //pass turn token
            player.isTurn = true;
            enemy.isTurn = false;
            //Debug.Log(player.isTurn);

            //clear board: discard deck, reposition, 
            board.Discard(enemyDiscard, enemyDiscardPosition);
            enemyHand.Discard(enemyDiscard, enemyDiscardPosition);

            isPlayable = true;
        }
        board.cards.RemoveAll(card => card == null);
        StartCoroutine(DealHands());
    }

    private void BotPlays()
    {
        //yield return new WaitForSeconds(3);
        for(int i = 0; i< enemyHand.cards.Count; i++)
        {
            enemyHand.PlayCard(enemyHand.cards[i]);
        }
        
    }

    internal IEnumerator DealHands()
    {
        for(int i=0; i<5; i++)
        {
            playerDeck.DealCard(playerHand);
            enemyDeck.DealCard(enemyHand);
            shopDeck.DealCard(shopHand);
            yield return new WaitForSeconds(0.5F);
        }
        isPlayable = true;
    }

    public void Reshuffle(Deck discard , Deck deck)
    {
        while( discard.cardDatas.Count>0)
        {
            //this might remove the wrong entry cuz the list changes
            int randomIndex = UnityEngine.Random.Range(1, discard.cardDatas.Count)-1;
            deck.cardDatas.Add(discard.cardDatas[randomIndex]);
            discard.cardDatas.RemoveAt(randomIndex);
            //return;
        }
    }


    public void updateHealth()
    {
        Text enemyHealthText = enemy.GetComponentInChildren<Text>();
        enemyHealthText.text = enemy.health.ToString();

        Text playerHealthText = player.GetComponentInChildren<Text>();
        playerHealthText.text = player.health.ToString();
    }

    public void updateDamage()
    {
        playerDamage.text = totalDamage.ToString();
    }

    public void updateCoin()
    {
        playerCoin.text = totalCoin.ToString();
    }
}
