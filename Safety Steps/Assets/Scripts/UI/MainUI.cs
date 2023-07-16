using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance { get; private set; }

    public float score;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private void Awake()
    {
        Instance = this;
        PlayerData.LoadScore();
    }

    private void Update()
    {
        score += Time.deltaTime;

        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        highscoreText.text = "Highscore: " + Mathf.FloorToInt(PlayerData.highscore).ToString();
    }
}
