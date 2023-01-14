using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetBoxTransformController : MonoBehaviour
{
    private Vector2 startPosition;
    [SerializeField] private Vector2 finalPosition;
    [SerializeField] private AnimationCurve animationCurve;

    private float lerpTime = 1.2f;
    private float currentLerpTime;

    private bool isActive = false;

    private void Start() 
    {
        startPosition = transform.position;    
    }

    public void SetActivity(bool active)
    {
        if(active == isActive)
            return;

        if(active)
        {
            StartCoroutine(StartLerp(startPosition, finalPosition));
        }
        else
        {
            StartCoroutine(StartLerp(finalPosition, startPosition));
        }

        isActive = active;

    }

    private IEnumerator StartLerp(Vector2 startPosition, Vector2 finalPosition)
    {
        while(currentLerpTime < lerpTime)
        {
            currentLerpTime += Time.deltaTime;

            float percentage = currentLerpTime / lerpTime;
            Vector2 newPosition = Vector2.Lerp(startPosition, finalPosition, animationCurve.Evaluate(percentage));
            transform.position = newPosition;

            yield return null;
        }
        currentLerpTime = 0;

        yield return null;
    }
}
