using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject gameOverUI;
    public bool gameOver = false;

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        gameOver = true;
    }

    public void Restart()
    {
        gameOver = false;
        gameOverUI.SetActive(false);
        SceneManager.LoadScene("__Scene_0");
    }
}
