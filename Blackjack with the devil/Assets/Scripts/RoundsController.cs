using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Winner
{
    Player,
    Devil,
    Nobody
}

public class RoundsController : MonoBehaviour
{
    private UI_RoundsController uI_RoundsController;
    
    [SerializeField] private CardsStackController playerCardsStack;
    [SerializeField] private CardsStackController devilCardsStack;

    private DeckController deckController;
    private bool isRoundInSession;

    private BetBoxController betBoxController;


    public delegate void OnRoundEnded(Winner winner);
    public event OnRoundEnded OnRoundEndedEvent;

    public delegate void OnBlackjack(string textToShow);
    public event OnBlackjack OnBlackjackEvent;

    private void Start() 
    {
        deckController = FindObjectOfType<DeckController>();
        uI_RoundsController = FindObjectOfType<UI_RoundsController>();
        betBoxController = FindObjectOfType<BetBoxController>();
    }

    private IEnumerator RoundPreparation()
    {
        uI_RoundsController.SetInteractionOfCardsDeckButton(false);

        yield return StartCoroutine(AddCardToStack(playerCardsStack));
        yield return StartCoroutine(AddCardToStack(devilCardsStack));
        yield return StartCoroutine(AddCardToStack(playerCardsStack));

        if(playerCardsStack.cardsStack.GetCardsSumValue() > 21)
        {
            StartCoroutine(ChooseWinner(Winner.Devil));
            yield break;
        }

        yield return StartCoroutine(AddCardToStack(devilCardsStack));

        if(playerCardsStack.cardsStack.GetCardsSumValue() == 21)
        {
            OnBlackjackEvent?.Invoke("Blackjack!");
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(FlipDevilFirstCard());
            CompareCardsValues();
            yield break;
        }

        uI_RoundsController.SetInteractionOfCardsDeckButton(true);
        uI_RoundsController.SetInteractionOfStayButton(true);

    }

    private IEnumerator AddCardToStack(CardsStackController stack)
    {
        

        Card card = deckController.GetCard();

        if(card.id == CardId.Ace)
        {
            card.value = 11;

            if(stack.cardsStack.GetCardsSumValue() + card.value > 21)
                card.value = 1;
        }

        stack.AddCardToStack(card);

        Debug.Log("Added " + card.symbol.ToString() + " " + card.id.ToString() + " " + card.value.ToString() + " to the " + stack.cardsStack.Name + " stack");
  
        yield return new WaitUntil(() => stack.GetCardObject(card).GetComponent<TransformController>().CheckIfWasMoved());
    }

    private IEnumerator FlipDevilFirstCard()
    {
        yield return new WaitForSeconds(0.6f);

        devilCardsStack.FlipCard(devilCardsStack.cardsStack.GetCard(0), true);

        yield return new WaitUntil(() => devilCardsStack.GetCardObject(devilCardsStack.cardsStack.GetCard(0)).GetComponent<TransformController>().CheckIfWasMoved());
    }

    private IEnumerator ChooseWinner(Winner winner)
    {
        uI_RoundsController.SetInteractionOfCardsDeckButton(false);
        uI_RoundsController.SetInteractionOfStayButton(false);
        yield return new WaitForSeconds(1.3f);
        Debug.Log(winner.ToString() + " wins");
        
        StartCoroutine(playerCardsStack.CleanStack());
        StartCoroutine(devilCardsStack.CleanStack());
        betBoxController.GiveBetToWinner(winner);
        
        yield return new WaitUntil(() => devilCardsStack.GetCardObject(devilCardsStack.cardsStack.GetCard(devilCardsStack.cardsStack.GetCardsAmountInStack() - 1)).GetComponent<TransformController>().CheckIfWasMoved());
        
        isRoundInSession = false;
        OnRoundEndedEvent?.Invoke(winner);
        OnBlackjackEvent?.Invoke(winner.ToString() + " wins");

        uI_RoundsController.SetInteractionOfCardsDeckButton(true);
    }

    

    public IEnumerator HitPlayer()
    {
        Debug.Log("Player hits");

        uI_RoundsController.SetInteractionOfStayButton(false);
        uI_RoundsController.SetInteractionOfCardsDeckButton(false);

        yield return StartCoroutine(AddCardToStack(playerCardsStack));

        if(playerCardsStack.cardsStack.GetCardsSumValue() > 21)
        {
            StartCoroutine(ChooseWinner(Winner.Devil));
            yield break;
        }
        else if(playerCardsStack.cardsStack.GetCardsSumValue() == 21)
        {
            StartCoroutine(HitDevil());
            yield break;
        }
        uI_RoundsController.SetInteractionOfStayButton(true);
        uI_RoundsController.SetInteractionOfCardsDeckButton(true);
    }



    public IEnumerator HitDevil()
    {
        uI_RoundsController.SetInteractionOfStayButton(false);
        uI_RoundsController.SetInteractionOfCardsDeckButton(false);

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FlipDevilFirstCard());

        while(devilCardsStack.cardsStack.GetCardsSumValue() < 17)
        {
            yield return StartCoroutine(AddCardToStack(devilCardsStack));  
            yield return new WaitForSeconds(0.4f);        
        }

        if(devilCardsStack.cardsStack.GetCardsSumValue() > 21)
        {
            StartCoroutine(ChooseWinner(Winner.Player));
            yield break;
        }

        CompareCardsValues();
    }

    private void CompareCardsValues()
    {

        if(devilCardsStack.cardsStack.GetCardsSumValue() > playerCardsStack.cardsStack.GetCardsSumValue())
        {
            StartCoroutine(ChooseWinner(Winner.Devil));
        }
        else if(devilCardsStack.cardsStack.GetCardsSumValue() == playerCardsStack.cardsStack.GetCardsSumValue())
        {
            StartCoroutine(ChooseWinner(Winner.Nobody));
        }
        else
        {
            StartCoroutine(ChooseWinner(Winner.Player));
        }
    }

    public void StartNewRound()
    {
        if(betBoxController.GetBet() == 0)
            return;

        betBoxController.MakeBet();
        StartCoroutine(RoundPreparation());
        isRoundInSession = true;
    }

    public bool IsRoundInSession()
    {
        return isRoundInSession;
    }



}

