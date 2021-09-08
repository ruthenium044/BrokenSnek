using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverImage;
    private string key = "Highscore";
    private int highScore = 0;
    private int score = 0;
    
    public int Score
    {
        get => score;
        set => score = value;
    }

    private void Awake()
    {
        gameOverImage.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey(key))
        {
            highScore = PlayerPrefs.GetInt(key);
        }
    }

    void Update()
    {
        if (score > highScore)
        {
            highScore = score;
        }
        highScoreText.text = "High score: " + highScore;
        scoreText.text = "Score: " + Score;
    }
    
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt(key, highScore);
        gameOverImage.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(key, highScore);
    }
}
