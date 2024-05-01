/*using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneSwitcher : MonoBehaviour
{
    public InputActionReference primaryButton;

    private void OnEnable()
    {
        primaryButton.action.performed += SwitchScene;
        primaryButton.action.performed += SwitchScene;
    }

    private void OnDisable()
    {
        primaryButton.action.performed -= SwitchScene;
        primaryButton.action.performed -= SwitchScene;
    }

    private void SwitchScene(InputAction.CallbackContext context)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
}*/


using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public InputActionReference primaryButton;
    public GameObject confirmationPanel;
    public AudioSource audioSource;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    private bool isPaused = false;
    private bool awaitingConfirmation = false;

    private void OnEnable()
    {
        primaryButton.action.performed += HandlePrimaryButton;
    }

    private void OnDisable()
    {
        primaryButton.action.performed -= HandlePrimaryButton;
    }

    private void HandlePrimaryButton(InputAction.CallbackContext context)
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

    private void Update()
    {
        if (isPaused && awaitingConfirmation && (leftActivate.action.ReadValue<float>()>=0.0001 || rightActivate.action.ReadValue<float>()>=0.0001))
        {
            ResumeGame();
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

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        audioSource.UnPause();
        confirmationPanel.SetActive(false);
        awaitingConfirmation = false;
    }
}

