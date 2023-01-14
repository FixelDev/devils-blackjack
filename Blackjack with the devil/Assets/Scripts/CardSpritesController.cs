using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpritesController : MonoBehaviour
{
    private Sprite[] cardSprites;

    private void Awake() 
    {
        cardSprites = Resources.LoadAll<Sprite>("cardDeck");    
    }

    public Sprite GetSprite(Card card)
    {
        Sprite foundSprite = null;

        foreach(Sprite cardSprite in cardSprites)
        {
            string[] cardProperties = cardSprite.name.Split('_');

            if(cardProperties.ToList().Find(s => s == card.id.ToString()) != null && cardProperties.ToList().Find(s => s == card.symbol.ToString()) != null)
            {
                if(card.id != CardId.Standard)
                {
                    foundSprite = cardSprite;
                    break;
                }
                else
                {
                    if(cardProperties.ToList().Find(s => s == card.value.ToString()) != null)
                    {
                        foundSprite = cardSprite;
                        break;
                    }
                }
               
            }
        }

        return foundSprite;
    }

    public Sprite GetSprite(string name)
    {
        return cardSprites.ToList().Find(s => s.name == name);
    }
}
