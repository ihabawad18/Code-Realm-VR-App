using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public InputActionReference primaryButton;
    public GameObject confirmationPanel;
    public AudioSource audioSource;
    public GameObject player;
    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;
    private bool isPaused = false;
    private bool awaitingConfirmation = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (primaryButton.action.triggered)
        {
            HandlePrimaryButton();
        }
        if (isPaused && awaitingConfirmation && (leftActivate.action.ReadValue<float>() >= 0.0001 || rightActivate.action.ReadValue<float>() >= 0.0001))
        {
            ResumeGame();
        }
    }

    private void HandlePrimaryButton()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else if (awaitingConfirmation)
        {
            isPaused = false;
            awaitingConfirmation = false;
            ConfirmSceneSwitch();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        audioSource.Pause();
        confirmationPanel.SetActive(true);
        awaitingConfirmation = true;
    }

    private void ConfirmSceneSwitch()
    {
        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;

        Vector3 newPosition = sceneIndex == 1 ?
            new Vector3(-9.65f, 0.163f, -7.73f) :
            new Vector3(-7f, 0.236f, -11f);

        if (player != null)
        {
            player.transform.position = newPosition;
        }

        ResumeGame();
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        audioSource.UnPause();
        confirmationPanel.SetActive(false);
        awaitingConfirmation = false;
    }
}
