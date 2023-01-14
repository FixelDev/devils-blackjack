using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(LivesController))]
public class UI_LivesController : MonoBehaviour
{
    [SerializeField] private Slider livesSlider;
    private TextMeshProUGUI additionalHealthText;

    private float currentLerpTime;
    private float lerpTime = 1f;


    private void Awake() 
    {
        additionalHealthText = livesSlider.transform.Find("AdditionalHealth").GetComponent<TextMeshProUGUI>();

        LivesController livesController = GetComponent<LivesController>();
        livesController.OnLivesAmountChangedEvent += UpdateLivesSlider;
    }

    private void UpdateLivesSlider(int livesAmount, bool init)
    {
        if(init)
            livesSlider.maxValue = livesAmount;

        float finalValue = livesAmount;

        if(livesAmount > livesSlider.maxValue)
        {
            finalValue = livesSlider.maxValue;

            additionalHealthText.SetText("+" + (livesAmount - livesSlider.maxValue).ToString());
        }
        else
        {
            additionalHealthText.SetText("");
        }
            

        StartCoroutine(MoveSlider(livesSlider.value, livesAmount));

    }

    private IEnumerator MoveSlider(float startValue, float finalValue)
    {
        while(currentLerpTime < lerpTime)
        {
            currentLerpTime += Time.deltaTime;

            float percentage = currentLerpTime / lerpTime;
            float newValue = Mathf.Lerp(startValue, finalValue, percentage);

            livesSlider.value = newValue;

            yield return null;
        }
        currentLerpTime = 0;
        yield return null;
    }
}
