using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    private string textToShow = "Witam serdecznie. Kekw popoga pepege aaaa asdasd.!";
    private bool finishedShowingText = true;
    public delegate void OnTextChanged(string text);
    public event OnTextChanged OnTextChangedEvent;

    private void Start() 
    {
        StartCoroutine(ShowText(textToShow));
    }

    private IEnumerator ShowText(string textToShow)
    {
        finishedShowingText = false;

        string allText = string.Empty;
        char[] textCharacters = textToShow.ToCharArray();

        for(int i = 0; i < textCharacters.Length; i++)
        {
            allText += textCharacters[i];
            OnTextChangedEvent?.Invoke(allText);
            yield return new WaitForSeconds(0.08f);
        }

        finishedShowingText = true;
    }

    public bool DidFinishShowingText()
    {
        return finishedShowingText;
    }

}
