using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private TextMeshProUGUI betsWonText;
    [SerializeField] private TextMeshProUGUI betsLostText;
    [SerializeField] private TextMeshProUGUI cardsDealtText;
    [SerializeField] private GameObject trophyCard;
    [SerializeField] private GameObject tombstone;

    private void Awake() 
    {
        GameOverController gameOverController = GetComponent<GameOverController>();
        gameOverController.OnGameOverEvent += ShowGameOverPanel;
    }

    private void ShowGameOverPanel(Winner winner, int betsWon, int betsLost, int cardsDealt)
    {
        string winnerToShow;

        if(winner == Winner.Player)
        {
            winnerToShow = "You won!";
            trophyCard.SetActive(true);
        }       
        else
        {
            winnerToShow = "You lost...";
            tombstone.SetActive(true);
        }
            
        FindObjectOfType<UI_RoundsController>().SetInteractionOfCardsDeckButton(false);
        winnerText.SetText(winnerToShow);
        betsWonText.SetText("bets won: " + betsWon.ToString());
        betsLostText.SetText("bets lost: " + betsLost.ToString());
        cardsDealtText.SetText("cards dealt: " + cardsDealt.ToString());
        

        gameOverPanel.SetActive(true);

        
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
