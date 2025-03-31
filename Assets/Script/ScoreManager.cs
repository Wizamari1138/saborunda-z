using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スコアスクリプト
public class ScoreManager : MonoBehaviour
{
    private float highScore;

    private void Start()
    {
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetFloat("HighScore",0f); // 保存されたハイスコアをロード（初回は0）
    }

    public void CheckAndSaveHighScore(float currentScore)
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetFloat("HighScore", highScore); // ハイスコアを保存
            PlayerPrefs.Save(); // 保存を適用（特にモバイルで重要）
        }
    }



}
