using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnswerScriptTemp : MonoBehaviour
{
    public Button buttonToClick;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the button component is not null
        if (buttonToClick != null)
        {
            // Trigger the click event on the button
            buttonToClick.onClick.Invoke();
        }
        else
        {
            Debug.LogWarning("Button reference is null. Make sure to assign a Button component to 'buttonToClick'.");
        }
    }
   
}
