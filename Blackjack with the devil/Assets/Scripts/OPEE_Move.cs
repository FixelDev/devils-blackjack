using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPEE_Move : OnPoinerEnterExit
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Vector2 moveOffset;
    [SerializeField] private AnimationCurve animationCurve;
    private Vector2 startPosition;
    private Vector2 finalPosition;
    private float currentLerpTime;
    private float lerpTime = 0.2f;

    private void Start() 
    {
        StartCoroutine(CalculateValues());
    }

    private IEnumerator CalculateValues()
    {
        yield return new WaitUntil(() => isEnabled);

        startPosition = objectToMove.transform.localPosition;
        finalPosition = startPosition + moveOffset;
    }

    public override void PointerEnter()
    {
        base.PointerEnter();
        StartCoroutine(MoveToFinalPosition());
    }

    public override void PointerExit()
    {
        base.PointerExit();
        StartCoroutine(MoveToStartPosition());

    }

    private IEnumerator MoveToFinalPosition()
    {
        Vector2 currentPosition = objectToMove.transform.localPosition;
        currentLerpTime = 0;
        

        while(isPointerIn && (Vector2)objectToMove.transform.localPosition != finalPosition)
        {
            MoveToDesiredLocation(currentPosition, finalPosition);

            yield return null;
        }

        
    }

    private IEnumerator MoveToStartPosition()
    {
        Vector2 currentPosition = objectToMove.transform.localPosition;
        currentLerpTime = 0;

        while(isPointerIn == false && (Vector2)objectToMove.transform.localPosition != startPosition)
        {
            MoveToDesiredLocation(currentPosition, startPosition);

            yield return null;
        }
    }

    private void MoveToDesiredLocation(Vector2 startPosition, Vector2 finalPosition)
    {
        currentLerpTime += Time.deltaTime;
        float percentage = currentLerpTime / lerpTime;

        if(percentage >= 1f)
            return;

        Vector2 newPosition = Vector2.Lerp(startPosition, finalPosition, animationCurve.Evaluate(percentage));
        objectToMove.transform.localPosition = newPosition;
    }


    


}
