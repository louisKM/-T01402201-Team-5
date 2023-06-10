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

    //    float duration = 1f; // 페이드 인에 걸리는 시간
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

    //    // 페이드 인이 완료된 후의 추가 작업을 수행합니다.
    //    Debug.Log("페이드 인이 완료되었습니다.");
    //    targetColor.a = 0f;
    //    spriteRenderer.color = targetColor;

    //    yield break;
    //}

    // 이후 코드
}
