using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Heart : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private bool isInBetBox;
    private HeartsDepthController heartsDepthController;
    private Light2D light;

    private void Start() 
    {
        myRigidbody = GetComponent<Rigidbody2D>();    
        heartsDepthController = FindObjectOfType<HeartsDepthController>();
        light = GetComponentInChildren<Light2D>();
        heartsDepthController.SortHeart(gameObject);
    }

    public void PickUp()
    {
        ToggleShadow(true);
        AudioManager.Instance.PlaySoundWithRandomPitch("heartPick", 0.7f, 1.5f);
    }

    public void Release()
    {
        BetBoxController betBoxController = FindObjectOfType<BetBoxController>();

        if(isInBetBox)
        {
            if(betBoxController.AddHeart(gameObject))
            {
                PutHeartInBetBox(betBoxController);
            }         
        }
        else
        {
            RemoveHeartFromBetBox(betBoxController);
        }
        AudioManager.Instance.PlaySoundWithRandomPitch("heartDrop", 0.7f, 1.5f);
        ToggleShadow(false);
    }

    private void PutHeartInBetBox(BetBoxController betBoxController)
    {
        transform.SetParent(betBoxController.transform); 
        ChangeMaskInteraction(SpriteMaskInteraction.VisibleOutsideMask);
        light.intensity = 0;
        transform.localScale = new Vector2(0.7f, 0.7f);
    }

    private void RemoveHeartFromBetBox(BetBoxController betBoxController)
    {
        transform.SetParent(null);
        betBoxController.RemoveHeart(gameObject);
        ChangeMaskInteraction(SpriteMaskInteraction.None);
        light.intensity = 0.46f;
        transform.localScale = new Vector2(1, 1);
    }

    private void ToggleShadow(bool toggle)
    {
        SpriteRenderer shadowSpriteRenderer = transform.Find("ShadowGFX").GetComponent<SpriteRenderer>();
        shadowSpriteRenderer.enabled = toggle;
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
