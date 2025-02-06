using UnityEngine;
using System.Collections;

public class FadeCanvasGroup : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f; // Time in seconds

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.5f);
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f; // Ensure it reaches 1
    }
}
