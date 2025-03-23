using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI elapsedTimeText;
    public TextMeshProUGUI highscoreText;

    private void Start()
    {
        // 保存された経過時間とハイスコアを取得
        float elapsedTime = PlayerPrefs.GetFloat("ElapsedTime", 0f);
        float highscore = PlayerPrefs.GetFloat("HighScore", 0f);

        // UIに表示
        elapsedTimeText.text = $"今回の記録: {FormatTime(elapsedTime)}";
        highscoreText.text = $"最高記録: {FormatTime(highscore)}";
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}
