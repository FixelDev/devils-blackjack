using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimationController : MonoBehaviour
{
    [SerializeField] private Animator titleAnimator;
    [SerializeField] private List<TransformController> buttonTransformControllers = new List<TransformController>();

    private void Start() 
    {
        StartCoroutine(PlayButtonsAnimations());    
    }

    private IEnumerator PlayButtonsAnimations()
    {
        yield return new WaitUntil(() => titleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);

        foreach(TransformController transformController in buttonTransformControllers)
        {
            Vector2 currentButtonPosition = transformController.gameObject.transform.position;
            Vector2 finalPosition = currentButtonPosition;
            finalPosition.y -= 4.3f;
            AudioManager.Instance.PlayRandomSoundByIndex("drawCard", 1, 4);
            StartCoroutine(transformController.MoveToDesiredLocation(currentButtonPosition, finalPosition, 0.8f, () =>{
                transformController.GetComponent<OPEE_Move>().SetIfIsEnabled(true);
                transformController.GetComponent<OPEE_Show>().SetIfIsEnabled(true);
                }));

            yield return new WaitForSeconds(0.5f);
        }
    }
}
