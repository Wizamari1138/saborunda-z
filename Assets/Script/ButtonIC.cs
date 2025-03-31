using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

// �{�^���̃z�o�[���ɃA�j���[�V�����ƌ��ʉ�������X�N���v�g
public class ButtonIC : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 normalScale;  // �ʏ펞�̃X�P�[��
    private Vector3 mouseScale;   // �}�E�X�z�o�[���̃X�P�[��
    private Coroutine scaleCoroutine; // �X�P�[���ύX�̃R���[�`������p

    private AudioSource audioSource;  // ���ʉ����Đ�����AudioSource
    public AudioClip onpointEnter; // �z�o�[���ɖ炷���ʉ�

    private void Start()
    {
        // ���݂̃X�P�[�����擾
        normalScale = transform.localScale;
        // �z�o�[���͒ʏ��1.2�{�̑傫����
        mouseScale = normalScale * 1.2f;

        // AudioSource�R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();
    }

    // �}�E�X���{�^���ɏ�����Ƃ��̏���
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���ɃX�P�[���ύX�̃R���[�`���������Ă������~
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        // �{�^�����g�傷��A�j���[�V�����J�n
        scaleCoroutine = StartCoroutine(ScaleButton(mouseScale));

        // ���ʉ����Đ��i�����N���b�N�ōĐ������j
        audioSource.PlayOneShot(onpointEnter);
    }

    // �}�E�X���{�^�����痣�ꂽ�Ƃ��̏���
    public void OnPointerExit(PointerEventData eventData)
    {
        // ���ɃX�P�[���ύX�̃R���[�`���������Ă������~
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        // �{�^�������̑傫���ɖ߂��A�j���[�V�����J�n
        scaleCoroutine = StartCoroutine(ScaleButton(normalScale));
    }

    // �{�^���̃X�P�[����ύX����A�j���[�V����
    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        float duration = 0.2f; // �g��E�k���ɂ����鎞�ԁi�b�j
        float time = 0f; // �o�ߎ���
        Vector3 startScale = transform.localScale; // �����X�P�[��

        while (time < duration)
        {
            // �X���[�Y�ɃX�P�[������
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime; // �o�ߎ��Ԃ��X�V
            yield return null; // ���̃t���[���܂őҋ@
        }

        // �ŏI�I�ȃX�P�[�����m��
        transform.localScale = targetScale;
    }
}
