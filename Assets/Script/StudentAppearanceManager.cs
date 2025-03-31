using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���k�̌����ڃX�N���v�g
public class StudentAppearanceManager : MonoBehaviour
{
    public SpriteRenderer studentSpriteRenderer; // ���k�̌����ڂ�ύX���邽�߂̃X�v���C�g�����_���[
    public List<Sprite> happinessStages;        // �K���x�ɉ����������ځi4�i�K���̃X�v���C�g�j
    public Sprite sleepSprite;                 // �����蒆�̌�����
    public Sprite eatSnacksSprite;             // ���َq��H�ׂĂ���Ƃ��̌�����
    public Sprite playGameSprite;              // �Q�[�������Ă���Ƃ��̌�����

    private GameManager gameManager;           // �K���x���擾���邽�߂̎Q��
    private string currentAction = "";         // ���݂̃A�N�V������ǐ�
    private Sprite defaultSprite;              // �f�t�H���g�̌����ڂ�ێ�

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (studentSpriteRenderer != null)
        {
            defaultSprite = studentSpriteRenderer.sprite; // �����X�v���C�g��ۑ�
        }
    }

    void Update()
    {
        // �A�N�V�������ݒ肳��Ă��Ȃ��ꍇ�͍K���x�ɉ����������ڂ�ύX
        if (string.IsNullOrEmpty(currentAction))
        {
            UpdateAppearanceByHappiness();
        }
    }

    /// <summary>
    /// �K���x�ɉ����������ڂ�ύX
    /// </summary>
    private void UpdateAppearanceByHappiness()
    {
        float happiness = gameManager.happiness;

        // �K���x�ɉ������X�v���C�g��ݒ�i4�i�K�j
        if (happiness > 75)
        {
            studentSpriteRenderer.sprite = happinessStages[0];
        }
        else if (happiness > 50)
        {
            studentSpriteRenderer.sprite = happinessStages[1];
        }
        else if (happiness > 25)
        {
            studentSpriteRenderer.sprite = happinessStages[2];
        }
        else
        {
            studentSpriteRenderer.sprite = happinessStages[3];
        }
    }

    /// <summary>
    /// ����̃A�N�V�����ɉ����������ڂ�ݒ�
    /// </summary>
    /// <param name="action">�A�N�V������</param>
    public void SetActionAppearance(string action)
    {
        currentAction = action;

        switch (action)
        {
            case "Sleep":
                studentSpriteRenderer.sprite = sleepSprite;
                break;
            case "EatSnacks":
                studentSpriteRenderer.sprite = eatSnacksSprite;
                break;
            case "PlayGame":
                studentSpriteRenderer.sprite = playGameSprite;
                break;
            default:
                currentAction = "";
                UpdateAppearanceByHappiness(); // �f�t�H���g�̌����ڂɖ߂�
                break;
        }
    }

    /// <summary>
    /// �A�N�V�����I�����Ɍ����ڂ����Z�b�g
    /// </summary>
    public void ResetAppearance()
    {
        currentAction = "";
        UpdateAppearanceByHappiness();
    }
}
