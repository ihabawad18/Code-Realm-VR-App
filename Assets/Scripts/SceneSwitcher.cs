using UnityEngine;
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
}
