using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyCardController : MonoBehaviour
{
    [SerializeField] private Sprite cardReverse;
    private Sprite cardObverse;
    private bool showingObverse = true;

    private void Start() 
    {
        cardObverse = GetComponent<Image>().sprite;
    }

    public void FlipCard()
    {
        Sprite newSprite = null;

        if(showingObverse)
            newSprite = cardReverse;
        else
            newSprite = cardObverse;

        GetComponent<Image>().sprite = newSprite;

        showingObverse = !showingObverse;
        transform.localScale *= new Vector2(-1, 1);
    }

    public void SetIfCanFlip()
    {
        GetComponent<Animator>().SetBool("canFlip", true);
    }
}
