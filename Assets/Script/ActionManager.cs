using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

//�T�{�菈���X�N���v�g
public class ActionManager : MonoBehaviour
{
    public GameManager gameManager; // GameManager�ւ̎Q��
    public Teacher teacher;         // Teacher�X�N���v�g�ւ̎Q��

    private bool isRunning = false; // �A�N�V���������s�����ǂ���
    private UniTaskCompletionSource stopCompletionSource; // ��~�����ʒm�p

    /// <summary>
    /// �A�N�V�������J�n����
    /// </summary>
    public void StartAction(float happinessIncrease, float detectionChance)
    {
        if (!isRunning)
        {
            isRunning = true;
            gameManager.SetSlacking(true); // �T�{��J�n
            PerformAction(happinessIncrease, detectionChance).Forget();
        }
    }

    /// <summary>
    /// �A�N�V�������~����
    /// </summary>
    public async UniTask StopAction()
    {
        if (isRunning)
        {
            isRunning = false;
            gameManager.SetSlacking(false); // �T�{��I��

            // ��~�����ʒm�p�̏�����
            stopCompletionSource = new UniTaskCompletionSource();
            await stopCompletionSource.Task; // ������ҋ@
            Debug.Log("�A�N�V���������S�ɒ�~���܂���");

            // **�Q�[���I�[�o�[���Ɋm���ɒ�~**
            stopCompletionSource?.TrySetResult();
        }
    }

    /// <summary>
    /// �񓯊��ŃA�N�V���������s
    /// </summary>
    private async UniTask PerformAction(float happinessIncrease, float detectionChance)
    {
        while (isRunning)
        {
            // �K���x�𑝉�
            gameManager.IncreaseHappiness(happinessIncrease);
            Debug.Log($"�K���x�𑝉�: {happinessIncrease}");

            // ���t���C�Â��\�����v�Z
            if (UnityEngine.Random.Range(0f, 100f) < detectionChance)
            {
                teacher.EnterSuspicionMode();
            }

            // 3�b�ҋ@
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
        }

        // ��~������ʒm
        stopCompletionSource?.TrySetResult();
    }
}
