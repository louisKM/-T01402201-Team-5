using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFadeIn : MonoBehaviour
{
    //private SpriteRenderer spriteRenderer;

    //private void Awake()
    //{
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //}

    //private void Start()
    //{
    //    if (spriteRenderer != null)
    //    {
    //        StartCoroutine(FadeInCoroutine());
    //    }
    //}

    //public void StartFadeIn()
    //{
    //    StartCoroutine(FadeInCoroutine());
    //}

    //private IEnumerator FadeInCoroutine()
    //{
    //    Color targetColor = spriteRenderer.color;
    //    targetColor.a = 0f;
    //    spriteRenderer.color = targetColor;

    //    float duration = 1f; // ���̵� �ο� �ɸ��� �ð�
    //    float elapsedTime = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        elapsedTime += Time.unscaledDeltaTime;

    //        float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
    //        targetColor.a = alpha;
    //        spriteRenderer.color = targetColor;
    //        Debug.Log(spriteRenderer.color);

    //        yield return null;
    //    }

    //    // ���̵� ���� �Ϸ�� ���� �߰� �۾��� �����մϴ�.
    //    Debug.Log("���̵� ���� �Ϸ�Ǿ����ϴ�.");
    //    targetColor.a = 0f;
    //    spriteRenderer.color = targetColor;

    //    yield break;
    //}

    // ���� �ڵ�
}
