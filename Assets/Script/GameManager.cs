using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;


public class GameManager : MonoBehaviour
{
    private bool isPaused = true; // 最初は停止状態

    public static bool isGameOverBranch;

    public StudentAppearanceManager studentAppearanceManager; // StudentAppearanceManager への参照
    public ScoreManager scoreManager;

    private AudioSource audioSource;  // AudioSource
    public AudioClip gameBGM; //ゲームBGM

    public Slider happinessSlider; // 幸福度ゲージ
    public TextMeshProUGUI elapsedTimeText;   // 経過時間を表示するテキスト
    public float happiness = 100f; // 初期幸福度
    public float happinessDecreaseRate = 2f; // 1秒ごとに幸福度が減少する値
    public float decreaseRateIncrease = 1f; // 20秒ごとに増加する減少量
    public float increaseInterval = 20f; // 減少量が増える間隔（秒）

    private float timeSinceLastIncrease = 0f; // 減少量増加用のタイマー
    private bool isGameOver = false;
    private float elapsedTime = 0f; // 授業開始からの経過時間
    private bool isSlacking = false; // サボり中かどうか

    private void Start()
    {
        // AudioSource コンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        // BGMをセットしてループ再生
        audioSource.clip = gameBGM;
        audioSource.loop = true; // ループを有効にする
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) return; // 停止中ならスキップ

        if (isGameOver) return;

        // 授業開始からの経過時間を計算
        elapsedTime += Time.deltaTime;
        UpdateElapsedTimeUI();

        // サボり中でなければ幸福度を減少
        if (!isSlacking)
        {
            happiness -= happinessDecreaseRate * Time.deltaTime;

            // 幸福度が0になったらゲームオーバー
            if (happiness <= 0)
            {
                isGameOverBranch = true;
                GameOver();
            }
        }
        happinessSlider.value = happiness / 100f;

        // 時間経過に応じて減少量を増加
        timeSinceLastIncrease += Time.deltaTime;
        if (timeSinceLastIncrease >= increaseInterval)
        {
            happinessDecreaseRate += decreaseRateIncrease;
            timeSinceLastIncrease = 0f; // タイマーをリセット
            Debug.Log($"幸福度の減少量が増加: {happinessDecreaseRate}");
        }
    }

    /// <summary>
    /// 幸福度を増加させる
    /// </summary>
    public void IncreaseHappiness(float amount)
    {
        happiness = Mathf.Clamp(happiness + amount, 0, 100);
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    public void GameOver()
    {
        isGameOver = true;

        // 経過時間を保存
        PlayerPrefs.SetFloat("ElapsedTime", elapsedTime);
        PlayerPrefs.Save(); // 保存を適用

        // ハイスコア更新
        scoreManager.CheckAndSaveHighScore(elapsedTime);

        // **幸福度の増加を止める**
        FindObjectOfType<ActionManager>()?.StopAction().Forget();

        // ゲームオーバー画面に遷移
        SceneManager.LoadScene("GameOverScene");
    }

    /// <summary>
    /// 経過時間を UI に更新する
    /// </summary>
    private void UpdateElapsedTimeUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f); // 分
        int seconds = Mathf.FloorToInt(elapsedTime % 60f); // 秒
        elapsedTimeText.text = $"経過時間: {minutes:00}:{seconds:00}";
    }

    /// <summary>
    /// サボり状態を設定
    /// </summary>
    public void SetSlacking(bool slacking)
    {
        isSlacking = slacking;
    }

    public void StartGame()
    {
        isPaused = false; // 授業開始！
        Debug.Log("授業開始！");

        audioSource.volume = 0.3f;
        audioSource.Play(); // BGMを鳴らす
    }

}