using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToSpaceship()
    {
        SceneManager.LoadScene("Spaceship");
    }
    
    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
