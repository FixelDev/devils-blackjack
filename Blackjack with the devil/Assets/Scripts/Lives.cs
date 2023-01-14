using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives  
{
    private int livesAmount;

    public Lives(int livesAmount)
    {
        this.livesAmount = livesAmount;
    }

    public void RemoveLives(int livesAmountToRemove)
    {
        if(livesAmount >= livesAmountToRemove)
        {
            livesAmount -= livesAmountToRemove;
        }
    }

    public void AddLives(int livesAmountToAdd)
    {
        livesAmount += livesAmountToAdd;
    }

    public int GetLivesAmount()
    {
        return livesAmount;
    }
}
