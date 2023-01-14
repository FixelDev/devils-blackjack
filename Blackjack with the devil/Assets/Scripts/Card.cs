using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardId
{
    Standard,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14
}

public enum CardSymbol
{
    Club,
    Diamond,
    Heart,
    Spade
}

public class Card
{
    public CardId id;
    public int value;
    public CardSymbol symbol;

    public Card(CardId id, int value, CardSymbol symbol)
    {
        this.id = id;
        this.value = value;
        this.symbol = symbol;
    }
}
