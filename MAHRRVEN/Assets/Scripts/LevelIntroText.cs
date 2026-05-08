using System.Collections;
using UnityEngine;
using TMPro;

public class LevelIntroText : MonoBehaviour
{
    [Header("References")]
    public TMP_Text introText;

    [Header("Content")]
    [TextArea(2, 6)]
    public string message = "Level 2 - The Dark Caves";

    [Header("Timing")]
    public float fadeInDuration = 0.6f;
    public float displayDuration = 2.5f;
    public float fadeOutDuration = 0.8f;

    [Header("Position")]
    public float bottomPadding = 40f;

    void Start()
    {
        introText.text = message;

 

        Color c = introText.color;
        c.a = 0f;
        introText.color = c;

        StartCoroutine(RunIntro());
    }

    IEnumerator RunIntro()
    {
        yield return Fade(0f, 1f, fadeInDuration);
        yield return new WaitForSeconds(displayDuration);
        yield return Fade(1f, 0f, fadeOutDuration);
        gameObject.SetActive(false);
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color c = introText.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, elapsed / duration);
            introText.color = c;
            yield return null;
        }
        c.a = to;
        introText.color = c;
    }
}