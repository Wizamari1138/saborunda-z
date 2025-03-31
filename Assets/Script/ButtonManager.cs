using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//サボり実行スクリプト
public class ButtonManager : MonoBehaviour
{
    public ActionManager actionManager;
    public Teacher teacher;
    public StudentAppearanceManager studentAppearanceManager; // StudentAppearanceManager への参照

    private string currentAction = ""; // 現在のアクション名
    private bool isActionRunning = false; // アクションが実行中かどうか

    private AudioSource audioSource;  // AudioSource
    public AudioClip actionSE; //アクションSE

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Sleep()
    {
        audioSource.PlayOneShot(actionSE); // 効果音を鳴らす
        studentAppearanceManager.SetActionAppearance("Sleep"); // 見た目を変更
        HandleAction("Sleep", 3f, 15f);
    }

    public void EatSnacks()
    {
        audioSource.PlayOneShot(actionSE); // 効果音を鳴らす
        studentAppearanceManager.SetActionAppearance("EatSnacks"); // 見た目を変更
        HandleAction("EatSnacks", 20f, 70f);
    }

    public void PlayGame()
    {
        audioSource.PlayOneShot(actionSE); // 効果音を鳴らす
        studentAppearanceManager.SetActionAppearance("PlayGame");
        HandleAction("PlayGame", 10f, 50f);
    }

    /// <summary>
    /// アクションを管理する共通関数
    /// </summary>
    private async void HandleAction(string actionName, float happinessIncrease, float detectionChance)
    {
        if (isActionRunning && currentAction == actionName)
        {
            // 同じアクションの場合は停止
            Debug.Log($"終了: {actionName}");
            teacher.SetPlayerSlacking(false); // 教師にサボり解除を通知
            await actionManager.StopAction(); // アクション停止を待機
            studentAppearanceManager.ResetAppearance(); // 見た目をリセット
            currentAction = "";
            isActionRunning = false;
        }
        else
        {
            // 異なるアクションの場合は切り替え
            Debug.Log($"開始: {actionName}");

            if (isActionRunning)
            {
                await actionManager.StopAction(); // 現在のアクションを停止
            }

            currentAction = actionName;
            isActionRunning = true;
            teacher.SetPlayerSlacking(true); // 教師にサボり状態を通知
            actionManager.StartAction(happinessIncrease, detectionChance); // 新しいアクションを開始
        }
    }
}
