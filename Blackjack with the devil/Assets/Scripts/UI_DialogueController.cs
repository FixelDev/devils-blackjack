using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Awake() 
    {
        DialogueController dialogueController = GetComponent<DialogueController>();
        dialogueController.OnTextChangedEvent += UpdateText;

    }

    private void UpdateText(string text)
    {
        dialogueText.SetText(text);
    }
}
