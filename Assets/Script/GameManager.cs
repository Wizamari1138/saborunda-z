using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;


public class GameManager : MonoBehaviour
{
    private bool isPaused = true; // �ŏ��͒�~���

    public static bool isGameOverBranch;

    public StudentAppearanceManager studentAppearanceManager; // StudentAppearanceManager �ւ̎Q��
    public ScoreManager scoreManager;

    private AudioSource audioSource;  // AudioSource
    public AudioClip gameBGM; //�Q�[��BGM

    public Slider happinessSlider; // �K���x�Q�[�W
    public TextMeshProUGUI elapsedTimeText;   // �o�ߎ��Ԃ�\������e�L�X�g
    public float happiness = 100f; // �����K���x
    public float happinessDecreaseRate = 2f; // 1�b���ƂɍK���x����������l
    public float decreaseRateIncrease = 1f; // 20�b���Ƃɑ������錸����
    public float increaseInterval = 20f; // �����ʂ�������Ԋu�i�b�j

    private float timeSinceLastIncrease = 0f; // �����ʑ����p�̃^�C�}�[
    private bool isGameOver = false;
    private float elapsedTime = 0f; // ���ƊJ�n����̌o�ߎ���
    private bool isSlacking = false; // �T�{�蒆���ǂ���

    private void Start()
    {
        // AudioSource �R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();

        // BGM���Z�b�g���ă��[�v�Đ�
        audioSource.clip = gameBGM;
        audioSource.loop = true; // ���[�v��L���ɂ���
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) return; // ��~���Ȃ�X�L�b�v

        if (isGameOver) return;

        // ���ƊJ�n����̌o�ߎ��Ԃ��v�Z
        elapsedTime += Time.deltaTime;
        UpdateElapsedTimeUI();

        // �T�{�蒆�łȂ���΍K���x������
        if (!isSlacking)
        {
            happiness -= happinessDecreaseRate * Time.deltaTime;

            // �K���x��0�ɂȂ�����Q�[���I�[�o�[
            if (happiness <= 0)
            {
                isGameOverBranch = true;
                GameOver();
            }
        }
        happinessSlider.value = happiness / 100f;

        // ���Ԍo�߂ɉ����Č����ʂ𑝉�
        timeSinceLastIncrease += Time.deltaTime;
        if (timeSinceLastIncrease >= increaseInterval)
        {
            happinessDecreaseRate += decreaseRateIncrease;
            timeSinceLastIncrease = 0f; // �^�C�}�[�����Z�b�g
            Debug.Log($"�K���x�̌����ʂ�����: {happinessDecreaseRate}");
        }
    }

    /// <summary>
    /// �K���x�𑝉�������
    /// </summary>
    public void IncreaseHappiness(float amount)
    {
        happiness = Mathf.Clamp(happiness + amount, 0, 100);
    }

    /// <summary>
    /// �Q�[���I�[�o�[����
    /// </summary>
    public void GameOver()
    {
        isGameOver = true;

        // �o�ߎ��Ԃ�ۑ�
        PlayerPrefs.SetFloat("ElapsedTime", elapsedTime);
        PlayerPrefs.Save(); // �ۑ���K�p

        // �n�C�X�R�A�X�V
        scoreManager.CheckAndSaveHighScore(elapsedTime);

        // **�K���x�̑������~�߂�**
        FindObjectOfType<ActionManager>()?.StopAction().Forget();

        // �Q�[���I�[�o�[��ʂɑJ��
        SceneManager.LoadScene("GameOverScene");
    }

    /// <summary>
    /// �o�ߎ��Ԃ� UI �ɍX�V����
    /// </summary>
    private void UpdateElapsedTimeUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f); // ��
        int seconds = Mathf.FloorToInt(elapsedTime % 60f); // �b
        elapsedTimeText.text = $"�o�ߎ���: {minutes:00}:{seconds:00}";
    }

    /// <summary>
    /// �T�{���Ԃ�ݒ�
    /// </summary>
    public void SetSlacking(bool slacking)
    {
        isSlacking = slacking;
    }

    public void StartGame()
    {
        isPaused = false; // ���ƊJ�n�I
        Debug.Log("���ƊJ�n�I");

        audioSource.volume = 0.3f;
        audioSource.Play(); // BGM��炷
    }

}