using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    public float score;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "Highscore: " + PlayerData.highscore.ToString();
    }
}
