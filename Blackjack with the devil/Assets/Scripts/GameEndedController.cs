using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    [SerializeField]private LivesController playerLivesController;
    [SerializeField]private LivesController devilLivesController;  

    public delegate void OnGameOver(Winner winner, int betsWon, int betsLost, int cardsDealt);
    public event OnGameOver OnGameOverEvent;

    private void Awake() 
    {
        RoundsController roundsController = FindObjectOfType<RoundsController>();
        roundsController.OnRoundEndedEvent += CheckIfSomeoneLost;
    }


    private void CheckIfSomeoneLost(Winner winner)
    {
        if(playerLivesController.IsThereAnyLifeLeft() == false || devilLivesController.IsThereAnyLifeLeft() == false)
        {
            BetBoxController betBoxController = FindObjectOfType<BetBoxController>();
            DeckController deckController = FindObjectOfType<DeckController>();

            int betsWon = betBoxController.GetBetsWon();
            int betsLost = betBoxController.GetBetsLost();
            int cardsDealt = deckController.deck.GetCardsDealt();

            OnGameOverEvent?.Invoke(winner, betsWon, betsLost, cardsDealt);

        }
    }


}
