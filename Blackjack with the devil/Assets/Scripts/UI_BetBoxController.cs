using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BetBoxController : MonoBehaviour
{
    [SerializeField] private GameObject betButton;
    [SerializeField] private GameObject cantBetMoreHeartsText;

    private void Awake() 
    {
        BetBoxController betBoxController = GetComponent<BetBoxController>();
        betBoxController.OnHeartsAmountChangedEvent += UpdateActivityOfBetButton;
    }

    private void UpdateActivityOfBetButton(int heartsAmount)
    {
        ChangeActivityOfBetButton(heartsAmount > 0);
    }

    private void ChangeActivityOfBetButton(bool active)
    {
        betButton.SetActive(active);
    }

    public void ShowCantBetMoreHeartsText()
    {
        cantBetMoreHeartsText.SetActive(true);
    }
}
