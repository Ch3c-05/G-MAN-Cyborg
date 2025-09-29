using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI; // assign your GameOverScreen panel here
    public GameObject winScreen;

    // Show Game Over screen
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // pause the game
    }

    // Restart the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f; // unpause
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f; // pause the game
    }

    // Quit game (works in build, not editor)
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
