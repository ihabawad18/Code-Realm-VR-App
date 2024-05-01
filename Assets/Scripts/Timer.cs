using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject timerText;
    public float countdownDuration = 20f;
    public AudioClip timerSound;

    public AudioSource audioSource;
    private Coroutine countdownCoroutine;
    private bool isCountingDown = false;
    
    public QuizManager quizManager;

    // Call this method to start the countdown timer
    public void StartTimer()
    {
        //isCountingDown = true;
        countdownCoroutine = StartCoroutine(Countdown()); 
    }
    public void ResetTimer()
    {
        Debug.Log("resetted");
        //isCountingDown = false;
        if (countdownCoroutine != null)
        {
            audioSource.Stop();
            StopCoroutine(countdownCoroutine);
        }
        timerText.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(countdownDuration).ToString();

    }
    IEnumerator Countdown()
    {
        float timeRemaining = countdownDuration;
       
        // Play the timer sound effect
        audioSource.PlayOneShot(timerSound);
        while (timeRemaining > 0f)
        {
            // Update the timer text to show the remaining time
            timerText.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(timeRemaining).ToString();
            

            // Wait for one second
            yield return new WaitForSeconds(1f);

            // Decrease the remaining time
            timeRemaining -= 1f;
        }

        StartCoroutine(ExecuteAfterDelayWrong());

    }
    IEnumerator ExecuteAfterDelayWrong()
    {
        // Wait for the specified delay
        audioSource.Stop();

        timerText.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(countdownDuration).ToString();

        yield return new WaitForSeconds(0.2f);
        
        quizManager.wrong();

    }
}
