using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private List<Card> cardsInDeck;
    private int decksAmount;
    private int cardsDealt;

    public Deck(int decksAmount)
    {
        CreateNewDeck(decksAmount);
    }

    private void CreateNewDeck(int decksAmount)
    {
        cardsInDeck = new List<Card>();
        this.decksAmount = decksAmount;
      
   
        for(int i = 0; i < decksAmount; i++)
        {
            foreach(CardSymbol symbol in Enum.GetValues(typeof(CardSymbol)))
            {
                int cardValue = 2;

                for(int j = 0; j < 13; j++)
                { 
                    Card card;
                
                    if(cardValue < 11)
                    {
                        card = new Card(CardId.Standard, cardValue, symbol);
                    }
                    else
                    {
                        card = new Card((CardId)cardValue, 10,  symbol);
                    }
                    cardValue++;
                    cardsInDeck.Add(card);
                }
            }
        }
       
        // Card card;

        // card = new Card(CardId.Ace, 10, CardSymbol.Club);
        // cardsInDeck.Add(card);
        // card = new Card(CardId.Standard, 10, CardSymbol.Club);
        // cardsInDeck.Add(card);
        // card = new Card(CardId.Jack, 10, CardSymbol.Club);
        // cardsInDeck.Add(card);
        // card = new Card(CardId.Standard, 2, CardSymbol.Club);
        // cardsInDeck.Add(card);


    }

    public Card GetRandomCard()
    {
        
        if(cardsInDeck.Count == 0)
            CreateNewDeck(decksAmount);

        UnityEngine.Random.InitState ((int)System.DateTime.Now.Ticks); 
        Card randomCard = cardsInDeck[UnityEngine.Random.Range(0, cardsInDeck.Count)];
        cardsInDeck.Remove(randomCard);
        cardsDealt++;
        
        return randomCard;
        

        // Card card = cardsInDeck[0];
        // cardsInDeck.Remove(card);
        // return card;
    }

    public int GetCardsDealt()
    {
        return cardsDealt;
    }
}   
