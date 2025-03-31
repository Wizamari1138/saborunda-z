using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//�Q�[���I�[�o�[��ʃX�N���v�g
public class GameOverManager : MonoBehaviour
{
    public GameObject characterObject; //�L�����N�^�[�I�u�W�F�N�g�Q��
    public TextMeshProUGUI gameOverText;�@//�Q�[���I�[�o�[�e�L�X�g�Q��

    public Sprite sermonSprite;�@//�����摜
    public Sprite escapeSprite;�@//�E���摜
    public SpriteRenderer spriteRenderer; // SpriteRenderer�R���|�[�l���g�̎Q��

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.isGameOverBranch) Escape();
        else Sermon();
    }

    private void Sermon()
    {
        gameOverText.text = "�΂ꂿ������E�E�E";
        spriteRenderer.sprite = sermonSprite;
    }

    private void Escape()
    {
        gameOverText.text = "�E�����Ă��܂����E�E�E";
        spriteRenderer.sprite = escapeSprite;
    }
}
