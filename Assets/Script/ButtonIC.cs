using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

// ボタンのホバー時にアニメーションと効果音をつけるスクリプト
public class ButtonIC : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 normalScale;  // 通常時のスケール
    private Vector3 mouseScale;   // マウスホバー時のスケール
    private Coroutine scaleCoroutine; // スケール変更のコルーチン制御用

    private AudioSource audioSource;  // 効果音を再生するAudioSource
    public AudioClip onpointEnter; // ホバー時に鳴らす効果音

    private void Start()
    {
        // 現在のスケールを取得
        normalScale = transform.localScale;
        // ホバー時は通常の1.2倍の大きさに
        mouseScale = normalScale * 1.2f;

        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    // マウスがボタンに乗ったときの処理
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 既にスケール変更のコルーチンが動いていたら停止
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        // ボタンを拡大するアニメーション開始
        scaleCoroutine = StartCoroutine(ScaleButton(mouseScale));

        // 効果音を再生（ワンクリックで再生される）
        audioSource.PlayOneShot(onpointEnter);
    }

    // マウスがボタンから離れたときの処理
    public void OnPointerExit(PointerEventData eventData)
    {
        // 既にスケール変更のコルーチンが動いていたら停止
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        // ボタンを元の大きさに戻すアニメーション開始
        scaleCoroutine = StartCoroutine(ScaleButton(normalScale));
    }

    // ボタンのスケールを変更するアニメーション
    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        float duration = 0.2f; // 拡大・縮小にかける時間（秒）
        float time = 0f; // 経過時間
        Vector3 startScale = transform.localScale; // 初期スケール

        while (time < duration)
        {
            // スムーズにスケールを補間
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime; // 経過時間を更新
            yield return null; // 次のフレームまで待機
        }

        // 最終的なスケールを確定
        transform.localScale = targetScale;
    }
}
