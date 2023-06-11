using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preuna : MonoBehaviour
{
    private bool isInteracting = false;
    private bool isFilling = false;
    private bool isFading = false;
    private int currentIndex = 0;
    private float fillAmount = 0f;
    private float fillSpeed = 0.09f;
    public Slider slider;
    public SpriteRenderer[] backgroundImages = new SpriteRenderer[3];
    public GameObject fillArea;
    public GameObject spriteToDisable;
    private float prevTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInteracting = true;
            slider.gameObject.SetActive(true);
            fillArea.SetActive(false);
            StartFadeIn();
            PauseGame();
        }
    }

    private void PauseGame()
    {
        prevTime = Time.timeScale;
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    private void StartFadeIn()
    {
        if (currentIndex >= backgroundImages.Length)
        {
            if(fillAmount >= 1f)
            {
                ResetSlider();
                ResumeGame();
                DisableBackgroundImages();
            }
            else
            {
                GameOver();
            }
            return;
        }

        SpriteRenderer currentImage = backgroundImages[currentIndex];
        StartCoroutine(FadeInCoroutine(currentImage));
    }

    private IEnumerator FadeInCoroutine(SpriteRenderer spriteRenderer)
    {
        Color targetColor = spriteRenderer.color;
        targetColor.a = 0f;
        spriteRenderer.color = targetColor;

        float duration = 2f; // 페이드 인에 걸리는 시간
        float elapsedTime = 0f;
        isFading = true;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            targetColor.a = alpha;
            spriteRenderer.color = targetColor;

            yield return null;
        }
        isFading = false;
        currentIndex++;
        StartFadeIn();
        targetColor.a = 0f;
        spriteRenderer.color = targetColor;
    }

    // Start is called before the first frame update 
    void Start()
    {
        slider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((isInteracting && !isFilling) || isFilling)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                isFilling = true;
                fillArea.SetActive(true);
                fillAmount += fillSpeed;
                fillAmount = Mathf.Clamp01(fillAmount);
                UpdateSlider();

                if(currentIndex < backgroundImages.Length -1)
                {
                    if(currentIndex == 0 || currentIndex == 1)
                    {
                        if(fillAmount >= 1f)
                        {
                            GameOver();
                        }
                    }
                }
                else if(currentIndex == backgroundImages.Length -1)
                {
                    if(fillAmount >= 1f)
                    {
                        isFilling = false;
                        ResetSlider();
                        ResumeGame();
                        DisableBackgroundImages();
                        DisableSprite();
                    }
                }
            }
        }
    }

    private void UpdateSlider()
    {
        if (slider != null)
        {
            slider.value = fillAmount;
        }
    }

    private void ResetSlider()
    {
        fillAmount = 0f;
        UpdateSlider();
        slider.gameObject.SetActive(false);
        //fillArea.gameObject.SetActive(false);
    }

    private void DisableSprite()
    {
        if(spriteToDisable != null)
        {
            spriteToDisable.SetActive(false);
        }
    }

    private void DisableBackgroundImages()
    {
        foreach(SpriteRenderer image in backgroundImages)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void GameOver()
    {

        SceneManager.LoadScene("GameOver");
    }
}


