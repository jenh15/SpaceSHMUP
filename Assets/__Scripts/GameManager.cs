using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Main main;
    [SerializeField]
    public GameObject gameOverUI;
    public bool gameOver = false;

    public void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            gameOver = true;
        }
    }

    public void Restart()
    {
        ResetGame();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("__Scene_0");
    }

    public void ResetGame()
    {
        // Hide the Game Over UI
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        gameOver = false;

        // Reset the player's shield or health
        Hero.S?.ResetHero();

        // Destroy all existing enemies
        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            Destroy(e.gameObject);
        }

        // Destroy all projectiles
        foreach (ProjectileHero p in FindObjectsOfType<ProjectileHero>())
        {
            Destroy(p.gameObject);
        }

        // Reset score
        HighScoreManager.S.ResetScore();

        main.lives = 3;
        for (int i = 0; i < 3; i++)
        {
            main.heartIcons[i].SetActive(true);
        }
    }
}
