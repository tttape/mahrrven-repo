using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ScreenFader : MonoBehaviour
{
    public Image fadeImage; // assign the black panel here
    public float fadeDuration = 0.5f;

    private void Awake()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public IEnumerator FadeOutIn(System.Action actionDuringFade)
    {
        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));

        // Perform action (e.g., teleport player, load level)
        actionDuringFade?.Invoke();

        // Fade back to transparent
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float t = 0f;
        Color c = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(startAlpha, endAlpha, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
        c.a = endAlpha;
        fadeImage.color = c;
    }
}
