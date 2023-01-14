using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainMenuController : MonoBehaviour
{
    private void Update() 
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneController sceneController = FindObjectOfType<SceneController>();
            sceneController.LoadScene("MainMenu");
        }    
    }
}
