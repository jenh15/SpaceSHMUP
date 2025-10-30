using UnityEngine;
using UnityEngine.UI; // for UI Text
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager S; // Singleton reference

    [Header("Dynamic")]
    public int score = 0;
    public int highScore = 0;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Awake()
    {
        // Singleton pattern
        if (S == null)
        {
            S = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Keep it between scenes (optional)
        DontDestroyOnLoad(gameObject);

        // Load saved high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateUI();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null) scoreText.text = $"Score: {score}";
        if (highScoreText != null) highScoreText.text = $"High Score: {highScore}";
    }
}
