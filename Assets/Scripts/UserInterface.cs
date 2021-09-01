using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text scoreText;
    private int highScore = 0;
    private int score = 0;
    
    public int Score
    {
        get => score;
        set => score = value;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highScore = PlayerPrefs.GetInt("Highscore");
        }
    }

    void Update()
    {
        highScoreText.text = "High score: " + highScore;
        scoreText.text = "Score: " + Score;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Highscore", highScore);
    }
}
