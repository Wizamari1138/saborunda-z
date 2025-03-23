using System;
using Cysharp.Threading.Tasks; // UniTask ���g������
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private AudioSource audioSource;  // AudioSource
    public AudioClip buttonSE; // ���ʉ�

    private void Start()
    {
        // AudioSource �R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();
    }

    public async void Title()
    {
        await SE(); // ���ʉ���炵�Ă���J��
        SceneManager.LoadScene("TitleScene");
    }

    public async void Game()
    {
        await SE(); // ���ʉ���炵�Ă���J��
        SceneManager.LoadScene("MainScene");
    }

    public async void Credits()
    {
        await SE();
        SceneManager.LoadScene("CreditsScene");
    }

    private async UniTask SE()
    {
        audioSource.PlayOneShot(buttonSE); // ���ʉ���炷
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f)); // 0.5�b�҂��Ă���V�[���J��
    }
}
