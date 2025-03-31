using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

//サボり処理スクリプト
public class ActionManager : MonoBehaviour
{
    public GameManager gameManager; // GameManagerへの参照
    public Teacher teacher;         // Teacherスクリプトへの参照

    private bool isRunning = false; // アクションが実行中かどうか
    private UniTaskCompletionSource stopCompletionSource; // 停止完了通知用

    /// <summary>
    /// アクションを開始する
    /// </summary>
    public void StartAction(float happinessIncrease, float detectionChance)
    {
        if (!isRunning)
        {
            isRunning = true;
            gameManager.SetSlacking(true); // サボり開始
            PerformAction(happinessIncrease, detectionChance).Forget();
        }
    }

    /// <summary>
    /// アクションを停止する
    /// </summary>
    public async UniTask StopAction()
    {
        if (isRunning)
        {
            isRunning = false;
            gameManager.SetSlacking(false); // サボり終了

            // 停止完了通知用の初期化
            stopCompletionSource = new UniTaskCompletionSource();
            await stopCompletionSource.Task; // 完了を待機
            Debug.Log("アクションが完全に停止しました");

            // **ゲームオーバー時に確実に停止**
            stopCompletionSource?.TrySetResult();
        }
    }

    /// <summary>
    /// 非同期でアクションを実行
    /// </summary>
    private async UniTask PerformAction(float happinessIncrease, float detectionChance)
    {
        while (isRunning)
        {
            // 幸福度を増加
            gameManager.IncreaseHappiness(happinessIncrease);
            Debug.Log($"幸福度を増加: {happinessIncrease}");

            // 教師が気づく可能性を計算
            if (UnityEngine.Random.Range(0f, 100f) < detectionChance)
            {
                teacher.EnterSuspicionMode();
            }

            // 3秒待機
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
        }

        // 停止完了を通知
        stopCompletionSource?.TrySetResult();
    }
}
