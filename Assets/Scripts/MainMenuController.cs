using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private string gameSceneName = "GameScene";

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(gameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();
    }
}