using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsStack
{
    private List<Card> cardsInStack;
    public string Name;

    public CardsStack(string name)
    {
        Name = name;

        cardsInStack = new List<Card>();
    }

    public void AddCardToStack(Card card)
    {
        cardsInStack.Add(card);
    }

    public int GetCardsSumValue()
    {
        int sum = 0;

        foreach(Card card in cardsInStack)
        {
            sum += card.value;
        }

        return sum;
    }

    public void CleanStack()
    {
        cardsInStack.Clear();
    }

    public int GetCardsAmountInStack()
    {
        return cardsInStack.Count;
    }

    public Card GetCard(int index)
    {
        return cardsInStack[index];
    }


}
