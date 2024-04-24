using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [Serializable]
    public class QuestionsAndAnswers
    {
        public string question;
        public string[] answers;
        public int correctAnswer;
    }

    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public GameObject QuestionNumber;
    public GameObject QuestionText;
    
    public GameObject startPanel;
    public GameObject quizPanel;
    public GameObject GoPanel;
    public GameObject timePanel;

    public GameObject ScoreText;

    public int score;
    int totalQuestions = 0;
    int questionNb = 1;
    public Timer t1;
    private void Start()
    {
        totalQuestions = QnA.Count;
    }

    public void startQuiz()
    {
        startPanel.SetActive(false);
        timePanel.SetActive(true);
        quizPanel.SetActive(true);
        generateQuestions();
    }

    public void GameOver()
    {
        quizPanel.SetActive(false);
        timePanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreText.GetComponent<TextMeshProUGUI>().text = score + "/" + totalQuestions;
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void resetTimerFromManager()
    {
        t1.ResetTimer();
    }


    public void correct()
    {
        //correct answer
        score++;
        if(QnA.Count > 0)
        {
            QnA.RemoveAt(currentQuestion);
        }
        
        
        generateQuestions();
    }


    public void wrong()
    {
        // wrong answer
        if (QnA.Count > 0)
        {
            QnA.RemoveAt(currentQuestion);
        }

        generateQuestions();
    }

    void SetAnswers()
    {
      
        for(int i=0; i < options.Length; i++)
        {
            //options[i].GetComponent<Button>().interactable = true;
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].answers[i];
            if (QnA[currentQuestion].correctAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestions()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = UnityEngine.Random.Range(0, QnA.Count);
            QuestionText.GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].question;
            QuestionNumber.GetComponent<TextMeshProUGUI>().text = "Question " + (questionNb);
            SetAnswers();
            t1.StartTimer();
            questionNb++;
        }
        else
        {
            GameOver();
        }
        
    }
}
