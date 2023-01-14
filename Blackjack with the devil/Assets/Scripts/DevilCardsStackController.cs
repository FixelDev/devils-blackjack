using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilCardsStackController : CardsStackController
{
    public override void AddCardToStack(Card card)
    {
        base.AddCardToStack(card);
    
        if(cardsStack.GetCardsAmountInStack() == 1)
        {
            FlipCard(cardsStack.GetCard(0), false);
        }
    }
}
