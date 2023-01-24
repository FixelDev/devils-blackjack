using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UI_RoundsController : MonoBehaviour
{
    [SerializeField] private GameObject cardsDeck;
    [SerializeField] private GameObject clickToStayTextObject;
    [SerializeField] private GameObject clickToHitTextObject;
    [SerializeField] private TextMeshProUGUI text;
    private RoundsController roundsController;
    private bool isStayButtonInteractable;
    private bool isCardsDeckButtonInteractable = true;

    private void Awake() 
    {
        roundsController = GetComponent<RoundsController>();
        roundsController.OnBlackjackEvent += ShowText;
    }
    
    private void ShowText(string textToShow)
    {
        text.SetText(textToShow);
        text.gameObject.SetActive(true);
        ParticlesSpawner.Singleton.SpawnParticles(ParticlesId.BlackjackTextSpawn, text.transform.position);
    }

    public void SetInteractionOfStayButton(bool active)
    {
        isStayButtonInteractable = active;
        clickToStayTextObject.SetActive(active);
    }

    public void SetInteractionOfCardsDeckButton(bool active)
    {
        isCardsDeckButtonInteractable = active;
        cardsDeck.GetComponent<OPEE_Move>().SetIfIsEnabled(active);
        

        if(roundsController.IsRoundInSession())
        {
            clickToHitTextObject.GetComponent<TextMeshProUGUI>().SetText("Click to hit");
            clickToHitTextObject.SetActive(active);
        }
        else
        {
            clickToHitTextObject.GetComponent<TextMeshProUGUI>().SetText("Click to start game");
            clickToHitTextObject.SetActive(active);
        }
    }
    
    public void HitPlayer()
    {
        StartCoroutine(roundsController.HitPlayer());
    }

    public void StayPlayer()
    {
        if(isStayButtonInteractable == false)
            return;

        Debug.Log("Player stays");
        AudioManager.Instance.PlaySound("devilClick");
        StartCoroutine(roundsController.HitDevil());
    }

    public void CardsDeckButton()
    {
        if(isCardsDeckButtonInteractable == false)
            return;

        

        if(roundsController.IsRoundInSession())
        {
            HitPlayer();
        }
        else
        {
            AudioManager.Instance.PlaySound("cardDeckShuffle");
            FindObjectOfType<BetBoxTransformController>().SetActivity(true);  
            SetInteractionOfCardsDeckButton(false);         
        }

        
    }

}
