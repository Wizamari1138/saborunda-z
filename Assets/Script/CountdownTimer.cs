using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // �J�E���g�_�E���\���p��TextMeshPro
    public GameManager gameManager;       // GameManager �ւ̎Q��
    public Button[] actionButtons;        // �T�{��s���p�̃{�^���Q
    public float countdownTime = 3f;      // �J�E���g�_�E���̕b��

    private AudioSource audioSource;  // AudioSource
    public AudioClip chime; //�Q�[��BGM

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(chime); // ���ʉ���炷
        StartCountdown(); // �Q�[���J�n���ɃJ�E���g�_�E���J�n
    }

    public void StartCountdown()
    {
        // �{�^���̖�����
        foreach (var button in actionButtons)
        {
            button.interactable = false;
        }

        Time.timeScale = 0; // ���̃X�N���v�g���~
        StartCoroutine(CountdownCoroutine());
    }

    private System.Collections.IEnumerator CountdownCoroutine()
    {
        // 3, 2, 1 �̃J�E���g�_�E��
        for (int i = (int)countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();  // ������\��
            yield return new WaitForSecondsRealtime(1f);  // `Time.timeScale = 0` �ł�����
        }

        // ���ƊJ�n�̕\��
        countdownText.text = "���ƊJ�n�I";
        yield return new WaitForSecondsRealtime(1f);

        // ���ƊJ�n��̏���
        Time.timeScale = 1; // �Q�[�����ĊJ
        gameManager.StartGame(); // GameManager �ɒʒm
        countdownText.gameObject.SetActive(false); // �J�E���g�_�E��UI���\��

        // �{�^���̗L����
        foreach (var button in actionButtons)
        {
            button.interactable = true;
        }
    }
}
