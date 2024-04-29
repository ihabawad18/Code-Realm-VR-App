using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;

    public QuizManager quizManager;
    public GameObject[] options;


    public void Answer()
    {
        quizManager.resetTimerFromManager();
        if (isCorrect)
        {
            Debug.Log("correct");
            this.GetComponent<Button>().image.color = Color.green;

            StartCoroutine(ExecuteAfterDelayCorrect());
        }
        else
        {
            Debug.Log("wrong");
            this.GetComponent<Button>().image.color = Color.red;

            StartCoroutine(ExecuteAfterDelayWrong());
        }


    }

    private void enableButtons()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Button>().interactable = true;
        }
    }

    private void disableButtons()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Button>().interactable = false;
        }
    }

    IEnumerator ExecuteAfterDelayCorrect()
    {
        disableButtons();
        // Wait for the specified delay
        //yield return new WaitForSeconds(1.65f);
        yield return new WaitForSeconds(1.5f);
        enableButtons();

        quizManager.correct();

        this.GetComponent<Button>().image.color = Color.white;
    }

    IEnumerator ExecuteAfterDelayWrong()
    {
        disableButtons();
        // Wait for the specified delay
        //yield return new WaitForSeconds(1.65f);
        yield return new WaitForSeconds(1.5f);
        enableButtons();
        quizManager.wrong();

        this.GetComponent<Button>().image.color = Color.white;
    }
}
