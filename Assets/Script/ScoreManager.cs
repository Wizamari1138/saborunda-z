using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float highScore;

    private void Start()
    {
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetFloat("HighScore",0f); // �ۑ����ꂽ�n�C�X�R�A�����[�h�i�����0�j
    }

    public void CheckAndSaveHighScore(float currentScore)
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetFloat("HighScore", highScore); // �n�C�X�R�A��ۑ�
            PlayerPrefs.Save(); // �ۑ���K�p�i���Ƀ��o�C���ŏd�v�j
        }
    }



}
