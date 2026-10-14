
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePauseController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject confirmExitPanel;

    [Header("Navigation")]
    [SerializeField] private SceneNavigation sceneNavigation;

    private bool isPaused = false;

    private enum ExitDestination
    {
        Spaceship,
        MainMenu
    }

    private ExitDestination selectedDestination;

    private void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;

        pausePanel.SetActive(false);
        confirmExitPanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (confirmExitPanel.activeSelf)
                return;

            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;

        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        pausePanel.SetActive(false);
        confirmExitPanel.SetActive(false);
    }

    public void RequestReturnToSpaceship()
    {
        selectedDestination = ExitDestination.Spaceship;
        ShowConfirmation();
    }

    public void RequestReturnToMenu()
    {
        selectedDestination = ExitDestination.MainMenu;
        ShowConfirmation();
    }

    private void ShowConfirmation()
    {
        pausePanel.SetActive(false);
        confirmExitPanel.SetActive(true);
    }

    public void CancelExit()
    {
        confirmExitPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ConfirmExit()
    {
        // TODO: Discard current run progress before leaving.

        ResumeGame();

        switch (selectedDestination)
        {
            case ExitDestination.Spaceship:
                sceneNavigation.GoToSpaceship();
                break;

            case ExitDestination.MainMenu:
                sceneNavigation.GoToMainMenu();
                break;
        }
    }

    private void OnDisable()
    {
        if (isPaused)
            Time.timeScale = 1f;
    }
}
