using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UI_LivesController))]
public class LivesController : MonoBehaviour
{
    [SerializeField] protected int startLivesAmount;
    [SerializeField] private Animator animator; 
    public Lives lives;

    public delegate void OnLivesAmountChanged(int livesAmount, bool init);
    public event OnLivesAmountChanged OnLivesAmountChangedEvent;

    protected virtual void Awake() 
    {
        lives = new Lives(startLivesAmount);  
        
    }

    private void Start() 
    {
        OnLivesAmountChangedEvent?.Invoke(lives.GetLivesAmount(), true);  
    }

    public void RemoveLives(int livesAmountToRemove)
    {
        lives.RemoveLives(livesAmountToRemove);
        animator.SetTrigger("takeDamage");
        OnLivesAmountChangedEvent?.Invoke(lives.GetLivesAmount(), false);  
    }

    public virtual void AddLives(int livesAmountToAdd)
    {
        lives.AddLives(livesAmountToAdd);
        OnLivesAmountChangedEvent?.Invoke(lives.GetLivesAmount(), false);  
    }

    public bool IsThereAnyLifeLeft()
    {
        return lives.GetLivesAmount() > 0;
    }


}
