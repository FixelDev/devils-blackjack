using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private CardSpritesController cardSpritesController;

    private Sprite cardObverse;
    private Sprite cardReverse;

    private bool isCardShowingObverse;
    public void SetSprite(Card card)
    {
        
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();    
        cardSpritesController = FindObjectOfType<CardSpritesController>();

        cardReverse = cardSpritesController.GetSprite("cardReverse");
        cardObverse = cardSpritesController.GetSprite(card);
        
        mySpriteRenderer.sprite = cardObverse;

        isCardShowingObverse = true;
    }

    public void Flip(bool playAnimation)
    {
        if(playAnimation)
        {
            StartCoroutine(PlayCardFlippingAnimation());
        }
        else
        {
            FlipCardGraphics();
        }
    }

    private IEnumerator PlayCardFlippingAnimation()
    {
        Vector2 startPosition = transform.position;
        Vector2 finalPosition = startPosition;
        finalPosition.y -= 0.7f;

        float animationTime = 0.4f;
        StartCoroutine(GetComponent<TransformController>().MoveToDesiredLocationAndComeBack(startPosition, finalPosition, animationTime, ()=>{}));
        yield return new WaitForSeconds(animationTime / 2f);
        FlipCardGraphics();

    }

    private void FlipCardGraphics()
    {
        if(isCardShowingObverse)
        {
            mySpriteRenderer.sprite = cardReverse;
        }
        else
        {
            mySpriteRenderer.sprite = cardObverse;
        }

        isCardShowingObverse = !isCardShowingObverse;
    }

}
