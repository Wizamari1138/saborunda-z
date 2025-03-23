using System;
using Cysharp.Threading.Tasks; // UniTask を使うため
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private AudioSource audioSource;  // AudioSource
    public AudioClip buttonSE; // 効果音

    private void Start()
    {
        // AudioSource コンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    public async void Title()
    {
        await SE(); // 効果音を鳴らしてから遷移
        SceneManager.LoadScene("TitleScene");
    }

    public async void Game()
    {
        await SE(); // 効果音を鳴らしてから遷移
        SceneManager.LoadScene("MainScene");
    }

    public async void Credits()
    {
        await SE();
        SceneManager.LoadScene("CreditsScene");
    }

    private async UniTask SE()
    {
        audioSource.PlayOneShot(buttonSE); // 効果音を鳴らす
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f)); // 0.5秒待ってからシーン遷移
    }
}
