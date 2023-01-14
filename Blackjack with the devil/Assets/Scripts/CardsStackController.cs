using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsStackController : MonoBehaviour
{   
    [SerializeField] private Transform cardsDeckTransform;
    [SerializeField] private float cardsYOffsetAfterCleaningStack;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private string stackName;


    private Dictionary<Card, GameObject> cardsInStackMap;
    public CardsStack cardsStack;


    private void Start() 
    {
        cardsInStackMap = new Dictionary<Card, GameObject>();
        cardsStack = new CardsStack(stackName);
    }

    private void Update() 
    {
       

    }

    public virtual void AddCardToStack(Card card)
    {
        SpawnCardInStack(card);
        cardsStack.AddCardToStack(card);
    }

    private void SpawnCardInStack(Card card)
    {
        GameObject spawnedCard = Instantiate(cardPrefab, transform);
        spawnedCard.GetComponent<CardController>().SetSprite(card);
        SpriteRenderer cardSpriteRenderer = spawnedCard.GetComponentInChildren<SpriteRenderer>();
        
        Vector2 finalPosition = transform.position;
        if(cardsInStackMap.Count > 0)
        {
            
            GameObject previousCardObject = cardsInStackMap[cardsStack.GetCard(cardsStack.GetCardsAmountInStack() - 1)];
            finalPosition.x = previousCardObject.transform.position.x + 0.7f;
            cardSpriteRenderer.sortingOrder = previousCardObject.GetComponentInChildren<SpriteRenderer>().sortingOrder + 1;
        }
        StartCoroutine(spawnedCard.GetComponent<TransformController>().MoveToDesiredLocation(cardsDeckTransform.position, finalPosition, 1.2f, ()=>{}));
        cardsInStackMap.Add(card, spawnedCard);

    }

    public IEnumerator CleanStack()
    {
        foreach(KeyValuePair<Card, GameObject> entry in cardsInStackMap)
        {
            GameObject cardObject = GetCardObject(entry.Key);
            Vector2 startPosition = cardObject.transform.position;
            Vector2 finalPosition = startPosition;
            finalPosition.y  += cardsYOffsetAfterCleaningStack;

            StartCoroutine(cardObject.GetComponent<TransformController>().MoveToDesiredLocation(startPosition, finalPosition, 1f, () => {Destroy(cardObject);}));

            yield return new WaitForSeconds(0.2f);
        }

        cardsInStackMap.Clear();
        cardsStack.CleanStack();
    }

    
    public GameObject GetCardObject(Card card)
    {
        return cardsInStackMap[card];
    }
    
    public void FlipCard(Card card, bool playAnimation)
    {
        cardsInStackMap[card].GetComponent<CardController>().Flip(playAnimation);
    }

}
