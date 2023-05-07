using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanelAnimation : MonoBehaviour
{

    #region Singleton
    //creating a single instance of the gamemanager script

    private static FadePanelAnimation _instance;

    public static FadePanelAnimation Instance => _instance;

    //checking that there is no other instances of the FadePanelAnimation manger running
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion


    public float fadeTime = 0.6f; // Time in seconds to fade in/out

    private Image panelImage;
    private Color originalColor;

    private void Start()
    {
        panelImage = GetComponent<Image>();
        originalColor = panelImage.color;

        FadeOut();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeAlpha(panelImage.color.a, 0f, fadeTime));
    }

    private IEnumerator FadeAlpha(float startAlpha, float endAlpha, float fadeTime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeTime);

            Color color = panelImage.color;
            color.a = alpha;
            panelImage.color = color;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure final alpha value is set correctly
        Color finalColor = panelImage.color;
        finalColor.a = endAlpha;
        panelImage.color = finalColor;

        // Disable the panel when faded out
        if (endAlpha == 0f)
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetFadePanel()
    {
        panelImage.color = originalColor;
        gameObject.SetActive(true);
        FadeOut();
    }
}