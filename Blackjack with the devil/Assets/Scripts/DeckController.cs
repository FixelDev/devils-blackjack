using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{   
    public Deck deck;

    private void Start() 
    {
        deck = new Deck(6);
    }

    public Card GetCard()
    {
        return deck.GetRandomCard();
    }
}
