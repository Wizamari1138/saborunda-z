using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonIC : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 normalScale;
    private Vector3 mouseScale;
    private Coroutine scaleCoroutine;

    AudioSource audioSource;  // AudioSource
    public AudioClip onpointEnter; // ���ʉ�


    private void Start()
    {
        normalScale = transform.localScale;
        mouseScale = normalScale * 1.2f;

        //Component���擾
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleButton(mouseScale));
        audioSource.PlayOneShot(onpointEnter); // ���ʉ���炷
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleButton(normalScale));
    }

    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        float duration = 0.2f; // �g��E�k���ɂ����鎞��
        float time = 0f;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
