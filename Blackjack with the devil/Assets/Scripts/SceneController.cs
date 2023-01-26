using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;

    public void LoadScene(string name)
    {
        transitionAnimator.SetTrigger("show");
        StartCoroutine(LoadSceneAfterTransition(name));
    }

    private IEnumerator LoadSceneAfterTransition(string name)
    {  
        yield return new WaitUntil(() => HasTransitionFinished());
        SceneManager.LoadScene(name);
    }

    private bool HasTransitionFinished()
    {
        
        return transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("transition_show") && transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
    }
}
