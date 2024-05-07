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

    // Timer variables to manage confirmation panel display
    private float confirmationDisplayTime = 5f;
    private float currentConfirmationTime = 0f;

    // Static variables to store the player's position between scene loads and flag for checking transition
    private static Vector3 savedPosition = Vector3.zero;
    private static bool isTransitioning = false;

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

        if (isPaused && awaitingConfirmation)
        {
            // Countdown for hiding the confirmation panel after a set period
            currentConfirmationTime -= Time.unscaledDeltaTime;
            if (currentConfirmationTime <= 0f)
            {
                ResumeGame();
            }

            if (leftActivate.action.ReadValue<float>() >= 0.0001 || rightActivate.action.ReadValue<float>() >= 0.0001)
            {
                ResumeGame();
            }
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

            // Save the player's current position before switching scenes and set transitioning flag
            if (player != null)
            {
                savedPosition = player.transform.position;
                isTransitioning = true;
            }

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
        currentConfirmationTime = confirmationDisplayTime; // Start countdown timer
    }

    private void ConfirmSceneSwitch()
    {
        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (player != null)
        {
            if (isTransitioning && savedPosition != Vector3.zero)
            {
                // Restore the player's saved position if transitioning between scenes
                player.transform.position = savedPosition;
            }
            else
            {
                // Default positions if it's the player's first time entering the scene
                Vector3 startPosition = scene.buildIndex == 1 ?
                    new Vector3(-9.65f, 0.163f, -7.73f) :
                    new Vector3(-7f, 0.236f, -11f);
                player.transform.position = startPosition;
            }
        }

        // Reset the transitioning flag
        isTransitioning = false;
        ResumeGame();
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        audioSource.UnPause();
        confirmationPanel.SetActive(false);
        awaitingConfirmation = false;
        currentConfirmationTime = 0f; // Reset the countdown timer
    }
}
