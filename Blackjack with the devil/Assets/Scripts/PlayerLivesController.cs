using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesController : LivesController
{
    private HeartsSpawner heartsSpawner;

    protected override void Awake()
    {
        base.Awake();
        
        heartsSpawner = GetComponent<HeartsSpawner>();
        heartsSpawner.SpawnHearts(startLivesAmount);
    }

    public override void AddLives(int livesAmountToAdd)
    {
        base.AddLives(livesAmountToAdd);

        heartsSpawner.SpawnHearts(livesAmountToAdd);
    }
}
