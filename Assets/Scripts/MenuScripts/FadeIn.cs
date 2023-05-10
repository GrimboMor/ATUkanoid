using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public float fadeInDuration = 1f; 
    private CanvasGroup[] canvasGroups; 

    private void Awake()
    {
        canvasGroups = GetComponentsInChildren<CanvasGroup>();
    }

    private IEnumerator Start()
    {
        // Set the alpha value of all the CanvasGroup components to 0
        foreach (CanvasGroup canvasGroup in canvasGroups)
        {
            canvasGroup.alpha = 0f;
        }

        // Wait for 0.5 seconds before starting the fade-in effect
        yield return new WaitForSeconds(0.5f);

        // Gradually increase the alpha value of all the CanvasGroup components over time
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            foreach (CanvasGroup canvasGroup in canvasGroups)
            {
                canvasGroup.alpha = alpha;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the alpha value of all the CanvasGroup components is fully set to 1
        foreach (CanvasGroup canvasGroup in canvasGroups)
        {
            canvasGroup.alpha = 1f;
        }
    }
}