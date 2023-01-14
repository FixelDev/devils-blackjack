using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private bool isInBetBox;
    private HeartsDepthController heartsDepthController;

    private void Start() 
    {
        myRigidbody = GetComponent<Rigidbody2D>();    
        heartsDepthController = FindObjectOfType<HeartsDepthController>();

        heartsDepthController.SortHeart(gameObject);
    }

    public void PickUp()
    {
        ChangeGravityScale(0);
    }

    public void Release()
    {
        BetBoxController betBoxController = FindObjectOfType<BetBoxController>();

        if(isInBetBox)
        {
            if(betBoxController.AddHeart(gameObject))
            {
                transform.SetParent(betBoxController.transform); 
                ChangeMaskInteraction(SpriteMaskInteraction.VisibleOutsideMask);
                return;
            }         
        }
        else
        {
            transform.SetParent(null);
            betBoxController.RemoveHeart(gameObject);
            ChangeMaskInteraction(SpriteMaskInteraction.None);
        }

        ChangeGravityScale(1);
    }

    private void ChangeGravityScale(int gravityScale)
    {
        myRigidbody.gravityScale = gravityScale;   
    }

    private void ChangeMaskInteraction(SpriteMaskInteraction spriteMaskInteraction)
    {
        GetComponentInChildren<SpriteRenderer>().maskInteraction = spriteMaskInteraction;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "BetBox")
            isInBetBox = true;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "BetBox")
            isInBetBox = false;    
    }

    private void OnDestroy() 
    {
        heartsDepthController.RemoveHeart(gameObject);    
    }

}
