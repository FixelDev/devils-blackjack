using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetBoxController : MonoBehaviour
{

    [SerializeField] private LivesController playerLivesController;
    [SerializeField] private LivesController devilLivesController;

    public BetBox betBox;
    private List<GameObject> heartsInBetBox;

    public delegate void OnHeartsAmountChanged(int heartsAmount);
    public event OnHeartsAmountChanged OnHeartsAmountChangedEvent;

    private int betsWon;
    private int betsLost;

    private void Start() 
    {
        betBox = new BetBox();
        heartsInBetBox = new List<GameObject>();
        
    }

    public void GiveBetToWinner(Winner winner)
    {

        if(winner == Winner.Player)
        {
            playerLivesController.AddLives(GetBet() * 2);
            devilLivesController.RemoveLives(GetBet());
            betsWon++;
            
        }
        else if(winner == Winner.Devil)
        {
            devilLivesController.AddLives(GetBet());
            betsLost++;
        }
        else
        {
            playerLivesController.AddLives(GetBet());
        }
        
        Clean();
    }

    public bool AddHeart(GameObject heart)
    {
        if(heartsInBetBox.Contains(heart) == false && betBox.GetHeartsAmount() == devilLivesController.lives.GetLivesAmount())
        {
            return false;
        } 
        if(heartsInBetBox.Contains(heart) == false)
        {
            betBox.AddHeart();      
            heartsInBetBox.Add(heart);
            OnHeartsAmountChangedEvent?.Invoke(betBox.GetHeartsAmount());
        }
        
        return true;
    }

    public void RemoveHeart(GameObject heart)
    {
        if(heartsInBetBox.Contains(heart))
        {
            betBox.RemoveHeart();
            heartsInBetBox.Remove(heart);
            OnHeartsAmountChangedEvent?.Invoke(betBox.GetHeartsAmount());
        }
        
        
    }

    public void Clean()
    {
        foreach(GameObject heart in heartsInBetBox)
        {
            Destroy(heart);
        }

        heartsInBetBox.Clear();
        betBox.Clean();

    }

    public void MakeBet()
    {
        playerLivesController.RemoveLives(GetBet());
    }

    public int GetBet()
    {
        return betBox.GetHeartsAmount();
    }

    public int GetBetsWon()
    {
        return betsWon;
    }

    public int GetBetsLost()
    {
        return betsLost;
    }
}
