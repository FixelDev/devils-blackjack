using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEndedController : MonoBehaviour
{
    [SerializeField]private LivesController playerLivesController;
    [SerializeField]private LivesController devilLivesController;  


    private void Awake() 
    {
        RoundsController roundsController = FindObjectOfType<RoundsController>();
        roundsController.OnRoundEndedEvent += CheckIfSomeoneLost;
    }


    private void CheckIfSomeoneLost(Winner winner)
    {
        if(playerLivesController.IsThereAnyLifeLeft() == false)
        {
            
        }
        else if(devilLivesController.IsThereAnyLifeLeft() == false)
        {
            
        }
    }


}
