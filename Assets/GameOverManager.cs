using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI; // assign your GameOverScreen panel here

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

    // Quit game (works in build, not editor)
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
