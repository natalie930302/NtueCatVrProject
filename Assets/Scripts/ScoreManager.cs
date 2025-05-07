using UnityEngine;
using TMPro; // Required for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Singleton instance

    public TextMeshProUGUI scoreText; // Drag your ScoreText UI element here
    private int score = 0;

    void Awake()
    {
        // Singleton pattern setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // Optional: DontDestroyOnLoad(gameObject); if you have multiple scenes
        }
    }

    void Start()
    {
        UpdateScoreDisplay(); // Initialize display
    }

    // Call this function to add or subtract points
    public void ModifyScore(int points)
    {
        score += points;
        Debug.Log($"Score changed by {points}. New score: {score}"); // Log for debugging
        UpdateScoreDisplay();
    }

    // Updates the UI Text element
    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("ScoreText reference is not set in ScoreManager!");
        }
    }

    // Optional: Function to get the current score
    public int GetScore()
    {
        return score;
    }
}