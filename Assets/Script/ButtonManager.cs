using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�T�{����s�X�N���v�g
public class ButtonManager : MonoBehaviour
{
    public ActionManager actionManager;
    public Teacher teacher;
    public StudentAppearanceManager studentAppearanceManager; // StudentAppearanceManager �ւ̎Q��

    private string currentAction = ""; // ���݂̃A�N�V������
    private bool isActionRunning = false; // �A�N�V���������s�����ǂ���

    private AudioSource audioSource;  // AudioSource
    public AudioClip actionSE; //�A�N�V����SE

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Sleep()
    {
        audioSource.PlayOneShot(actionSE); // ���ʉ���炷
        studentAppearanceManager.SetActionAppearance("Sleep"); // �����ڂ�ύX
        HandleAction("Sleep", 3f, 15f);
    }

    public void EatSnacks()
    {
        audioSource.PlayOneShot(actionSE); // ���ʉ���炷
        studentAppearanceManager.SetActionAppearance("EatSnacks"); // �����ڂ�ύX
        HandleAction("EatSnacks", 20f, 70f);
    }

    public void PlayGame()
    {
        audioSource.PlayOneShot(actionSE); // ���ʉ���炷
        studentAppearanceManager.SetActionAppearance("PlayGame");
        HandleAction("PlayGame", 10f, 50f);
    }

    /// <summary>
    /// �A�N�V�������Ǘ����鋤�ʊ֐�
    /// </summary>
    private async void HandleAction(string actionName, float happinessIncrease, float detectionChance)
    {
        if (isActionRunning && currentAction == actionName)
        {
            // �����A�N�V�����̏ꍇ�͒�~
            Debug.Log($"�I��: {actionName}");
            teacher.SetPlayerSlacking(false); // ���t�ɃT�{�������ʒm
            await actionManager.StopAction(); // �A�N�V������~��ҋ@
            studentAppearanceManager.ResetAppearance(); // �����ڂ����Z�b�g
            currentAction = "";
            isActionRunning = false;
        }
        else
        {
            // �قȂ�A�N�V�����̏ꍇ�͐؂�ւ�
            Debug.Log($"�J�n: {actionName}");

            if (isActionRunning)
            {
                await actionManager.StopAction(); // ���݂̃A�N�V�������~
            }

            currentAction = actionName;
            isActionRunning = true;
            teacher.SetPlayerSlacking(true); // ���t�ɃT�{���Ԃ�ʒm
            actionManager.StartAction(happinessIncrease, detectionChance); // �V�����A�N�V�������J�n
        }
    }
}
