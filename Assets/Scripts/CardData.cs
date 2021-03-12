using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Card", menuName ="CardGame/Card")]
public class CardData : ScriptableObject
{
    public enum CardType
    {
        Champion,
        Action,
        Item
    }
    public string cardTitle;
    public string description;
    public int Shield;
    public int cost;
    public int damage;
    public int gold;
    public Sprite cardImage;
    public Sprite cardFrameImage;
    public int numberInDeck;
    public bool isAttackCard = false;
    public bool isHealCard = false;
    public bool isDefenseCard = false;
    public bool isCreated = false;
}
