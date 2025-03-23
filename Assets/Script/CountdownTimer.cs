using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // カウントダウン表示用のTextMeshPro
    public GameManager gameManager;       // GameManager への参照
    public Button[] actionButtons;        // サボり行動用のボタン群
    public float countdownTime = 3f;      // カウントダウンの秒数

    private AudioSource audioSource;  // AudioSource
    public AudioClip chime; //ゲームBGM

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(chime); // 効果音を鳴らす
        StartCountdown(); // ゲーム開始時にカウントダウン開始
    }

    public void StartCountdown()
    {
        // ボタンの無効化
        foreach (var button in actionButtons)
        {
            button.interactable = false;
        }

        Time.timeScale = 0; // 他のスクリプトを停止
        StartCoroutine(CountdownCoroutine());
    }

    private System.Collections.IEnumerator CountdownCoroutine()
    {
        // 3, 2, 1 のカウントダウン
        for (int i = (int)countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();  // 数字を表示
            yield return new WaitForSecondsRealtime(1f);  // `Time.timeScale = 0` でも動く
        }

        // 授業開始の表示
        countdownText.text = "授業開始！";
        yield return new WaitForSecondsRealtime(1f);

        // 授業開始後の処理
        Time.timeScale = 1; // ゲームを再開
        gameManager.StartGame(); // GameManager に通知
        countdownText.gameObject.SetActive(false); // カウントダウンUIを非表示

        // ボタンの有効化
        foreach (var button in actionButtons)
        {
            button.interactable = true;
        }
    }
}
