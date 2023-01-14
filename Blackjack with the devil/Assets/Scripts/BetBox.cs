using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetBox
{
    private int heartsAmount;

    public void AddHeart()
    {
        heartsAmount++;
    }

    public void RemoveHeart()
    {
        heartsAmount--;
    }

    public int GetHeartsAmount()
    {
        return heartsAmount;
    }

    public void Clean()
    {
        heartsAmount = 0;
    }
}
