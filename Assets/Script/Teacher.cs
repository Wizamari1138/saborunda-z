using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//���t�X�N���v�g
public class Teacher : MonoBehaviour
{
    private bool isSuspicionMode = false; // ���t���^���Ă��邩�ǂ���
    private bool isGameOver = false;
    private float suspicionTimer = 3f;   // �^������
    private bool playerIsSlacking = false; // �v���C���[���T�{���Ă��邩�ǂ���

    public GameObject teacherObject;
    public GameObject mark_question;�@//�H�}�[�N
    public GameObject question_fukidash;
    public GameObject teacherText;
    public GameObject text_fukidash;
    public Sprite normalSprite; // �ʏ펞�̉摜
    public Sprite suspicionSprite; // �^�����̉摜
    public Sprite discoveredSprite; // �������̉摜
    public SpriteRenderer spriteRenderer; // SpriteRenderer�R���|�[�l���g�̎Q��
    public Button[] actionButtons;        // �T�{��s���p�̃{�^���Q

    private float[] teacherX = new float[] {-6.12f, -5.99f, -6.1f};
    private float[] teacherY = new float[] {0.42f, 0.2f, 0f};

    Vector3 worldPos;
    Transform teacherTransform;
    private CancellationTokenSource cancellationTokenSource;

    private AudioSource audioSource;  // AudioSource
    public AudioClip hatenaSE; //��a��SE
    public AudioClip hakkenSE;


    void Start()
    {
        // AudioSource �R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();

        teacherTransform = teacherObject.transform;
        worldPos = teacherTransform.position;

        spriteRenderer.sprite = normalSprite; // ������Ԃ͒ʏ펞�̉摜
        mark_question.SetActive(false);
        question_fukidash.SetActive(false);

        cancellationTokenSource = new CancellationTokenSource();
    }

    /// <summary>
    /// ���t���^�����[�h�ɓ���
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
    /// �v���C���[���T�{���Ă��邩�ǂ�����ݒ�
    /// </summary>
    public void SetPlayerSlacking(bool isSlacking)
    {
        playerIsSlacking = isSlacking;

    }

    /// <summary>
    /// �^�����[�h�̃J�E���g�_�E��
    /// </summary>
    private async UniTask SuspicionCountdown()
    {
        float elapsedTime = 0f;

        //��a�����o����
        audioSource.PlayOneShot(hatenaSE); // ���ʉ���炷
        mark_question.SetActive(true);
        question_fukidash.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
        mark_question.SetActive(false);
        question_fukidash.SetActive(false);

        if (teacherTransform == null) return; // Transform���폜����Ă����珈�����I��

        Debug.Log("���t���^���Ă���...");

        worldPos.x = teacherX[1];
        worldPos.y = teacherY[1];
        teacherTransform.position = worldPos;

        spriteRenderer.sprite = suspicionSprite; // �^�����̉摜�ɕύX

        // 3�b�ԃ`�F�b�N��������
        while (elapsedTime < suspicionTimer)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f)); // 0.1�b���ƂɊm�F
            elapsedTime += 0.1f;

            // �^�����[�h���ɃT�{�����瑦�Q�[���I�[�o�[
            if (playerIsSlacking)
            {
                TriggerGameOver();
                return; // �����ŃJ�E���g�_�E�����I��
            }
        }

        // �^�������ꂽ
        Debug.Log("�^�������ꂽ");

        worldPos.x = teacherX[0];
        worldPos.y = teacherY[0];
        teacherTransform.position = worldPos;

        spriteRenderer.sprite = normalSprite; // �ʏ펞�̉摜�ɖ߂�
        isSuspicionMode = false;
    }

    /// <summary>
    /// �Q�[���I�[�o�[����
    /// </summary>
    private async void TriggerGameOver()
    {
        Debug.Log("���t���T�{��𔭌��I");

        audioSource.PlayOneShot(hakkenSE); // ���ʉ���炷

        worldPos.x = teacherX[2];
        worldPos.y = teacherY[2];
        teacherTransform.position = worldPos;

        // �{�^���̖�����
        foreach (var button in actionButtons)
        {
            button.interactable = false;
        }

        //text�\��
        teacherText.SetActive(true);
        text_fukidash.SetActive(true);
        spriteRenderer.sprite = discoveredSprite; // �������̉摜�ɕύX
        await UniTask.Delay(TimeSpan.FromSeconds(2f));

        if (isGameOver) return;

        isGameOver = true;
        GameManager.isGameOverBranch = false;
        FindObjectOfType<GameManager>().GameOver(); // GameOver ���\�b�h�𒼐ڌĂяo��
    }
}
