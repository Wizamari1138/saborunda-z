using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//教師スクリプト
public class Teacher : MonoBehaviour
{
    private bool isSuspicionMode = false; // 教師が疑っているかどうか
    private bool isGameOver = false;
    private float suspicionTimer = 3f;   // 疑う時間
    private bool playerIsSlacking = false; // プレイヤーがサボっているかどうか

    public GameObject teacherObject;
    public GameObject mark_question;　//？マーク
    public GameObject question_fukidash;
    public GameObject teacherText;
    public GameObject text_fukidash;
    public Sprite normalSprite; // 通常時の画像
    public Sprite suspicionSprite; // 疑い時の画像
    public Sprite discoveredSprite; // 発見時の画像
    public SpriteRenderer spriteRenderer; // SpriteRendererコンポーネントの参照
    public Button[] actionButtons;        // サボり行動用のボタン群

    private float[] teacherX = new float[] {-6.12f, -5.99f, -6.1f};
    private float[] teacherY = new float[] {0.42f, 0.2f, 0f};

    Vector3 worldPos;
    Transform teacherTransform;
    private CancellationTokenSource cancellationTokenSource;

    private AudioSource audioSource;  // AudioSource
    public AudioClip hatenaSE; //違和感SE
    public AudioClip hakkenSE;


    void Start()
    {
        // AudioSource コンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        teacherTransform = teacherObject.transform;
        worldPos = teacherTransform.position;

        spriteRenderer.sprite = normalSprite; // 初期状態は通常時の画像
        mark_question.SetActive(false);
        question_fukidash.SetActive(false);

        cancellationTokenSource = new CancellationTokenSource();
    }

    /// <summary>
    /// 教師が疑いモードに入る
    /// </summary>
    public void EnterSuspicionMode()
    {
        if (!isSuspicionMode)
        {
            isSuspicionMode = true;
            SuspicionCountdown().Forget();
        }
    }

    /// <summary>
    /// プレイヤーがサボっているかどうかを設定
    /// </summary>
    public void SetPlayerSlacking(bool isSlacking)
    {
        playerIsSlacking = isSlacking;

    }

    /// <summary>
    /// 疑いモードのカウントダウン
    /// </summary>
    private async UniTask SuspicionCountdown()
    {
        float elapsedTime = 0f;

        //違和感を覚える
        audioSource.PlayOneShot(hatenaSE); // 効果音を鳴らす
        mark_question.SetActive(true);
        question_fukidash.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
        mark_question.SetActive(false);
        question_fukidash.SetActive(false);

        if (teacherTransform == null) return; // Transformが削除されていたら処理を終了

        Debug.Log("教師が疑っている...");

        worldPos.x = teacherX[1];
        worldPos.y = teacherY[1];
        teacherTransform.position = worldPos;

        spriteRenderer.sprite = suspicionSprite; // 疑い時の画像に変更

        // 3秒間チェックし続ける
        while (elapsedTime < suspicionTimer)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f)); // 0.1秒ごとに確認
            elapsedTime += 0.1f;

            // 疑いモード中にサボったら即ゲームオーバー
            if (playerIsSlacking)
            {
                TriggerGameOver();
                return; // ここでカウントダウンを終了
            }
        }

        // 疑いが晴れた
        Debug.Log("疑いが晴れた");

        worldPos.x = teacherX[0];
        worldPos.y = teacherY[0];
        teacherTransform.position = worldPos;

        spriteRenderer.sprite = normalSprite; // 通常時の画像に戻す
        isSuspicionMode = false;
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    private async void TriggerGameOver()
    {
        Debug.Log("教師がサボりを発見！");

        audioSource.PlayOneShot(hakkenSE); // 効果音を鳴らす

        worldPos.x = teacherX[2];
        worldPos.y = teacherY[2];
        teacherTransform.position = worldPos;

        // ボタンの無効化
        foreach (var button in actionButtons)
        {
            button.interactable = false;
        }

        //text表示
        teacherText.SetActive(true);
        text_fukidash.SetActive(true);
        spriteRenderer.sprite = discoveredSprite; // 発見時の画像に変更
        await UniTask.Delay(TimeSpan.FromSeconds(2f));

        if (isGameOver) return;

        isGameOver = true;
        GameManager.isGameOverBranch = false;
        FindObjectOfType<GameManager>().GameOver(); // GameOver メソッドを直接呼び出し
    }
}
