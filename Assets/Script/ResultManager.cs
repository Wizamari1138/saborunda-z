using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI elapsedTimeText;
    public TextMeshProUGUI highscoreText;

    private void Start()
    {
        // �ۑ����ꂽ�o�ߎ��Ԃƃn�C�X�R�A���擾
        float elapsedTime = PlayerPrefs.GetFloat("ElapsedTime", 0f);
        float highscore = PlayerPrefs.GetFloat("HighScore", 0f);

        // UI�ɕ\��
        elapsedTimeText.text = $"����̋L�^: {FormatTime(elapsedTime)}";
        highscoreText.text = $"�ō��L�^: {FormatTime(highscore)}";
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}
