using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;

    public QuizManager quizManager;

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

    IEnumerator ExecuteAfterDelayCorrect()
    {

        // Wait for the specified delay
        yield return new WaitForSeconds(1.5f);

        quizManager.correct();

        this.GetComponent<Button>().image.color = Color.white;
    }

    IEnumerator ExecuteAfterDelayWrong()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(1.5f);

        quizManager.wrong();

        this.GetComponent<Button>().image.color = Color.white;
    }
}
