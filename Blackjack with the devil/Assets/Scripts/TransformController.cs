using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    private bool wasMoved;

    public IEnumerator MoveToDesiredLocation(Vector2 startPosition, Vector2 finalPosition, float time, Action callback)
    {
        wasMoved = false;
        yield return StartCoroutine(Move(startPosition, finalPosition, time, callback));
        wasMoved = true;
    }

    private IEnumerator Move(Vector2 startPosition, Vector2 finalPosition, float time, Action callback)
    {
        float currentLerpTime = 0f;

        while(currentLerpTime < time)
        {
            currentLerpTime += Time.deltaTime;

            float percentage = currentLerpTime / time;

            
            if(percentage >= 1f)
            {        
                callback();
            }

            Vector2 newPosition = Vector2.Lerp(startPosition, finalPosition, animationCurve.Evaluate(percentage));
            transform.position = newPosition;

            yield return null;
        }
        currentLerpTime = 0f;
        yield return null;
    }

    public IEnumerator MoveToDesiredLocationAndComeBack(Vector2 startPosition, Vector2 finalPosition, float time, Action callback)
    {
        float timeForOneMove = time / 2f;

        wasMoved = false;

        yield return StartCoroutine(MoveToDesiredLocation(startPosition, finalPosition, timeForOneMove, ()=>{}));
        yield return StartCoroutine(MoveToDesiredLocation(finalPosition, startPosition, timeForOneMove, callback));

        wasMoved = true;
    }

    public bool CheckIfWasMoved()
    {
        return wasMoved;
    }
}
