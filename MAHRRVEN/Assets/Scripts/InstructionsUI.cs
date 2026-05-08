using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionsUI : MonoBehaviour
{
    [Header("References")]
    public CanvasGroup canvasGroup;
    public TMP_Text instructionsText;

    [Header("Content")]
    [TextArea(3, 10)]
    public string message = "Use WASD to move.\nPress Space to jump.\nReach the glowing orb to win!";

    [Header("Timing")]
    public float fadeInDuration = 0.8f;
    public float displayDuration = 0.0f;
    public float fadeOutDuration = 0.5f;

    [Header("Dismiss")]
    public KeyCode dismissKey = KeyCode.Return;
    public bool anyKey = true;   // if true, any key dismisses; if false, only dismissKey
    public bool autoClose = false;

    void Start()
    {
        instructionsText.text = message;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        StartCoroutine(RunInstructions());
    }

    IEnumerator RunInstructions()
    {
        yield return Fade(0f, 1f, fadeInDuration);

        if (autoClose && displayDuration > 0f)
            yield return new WaitForSeconds(displayDuration);
        else if (anyKey)
            yield return new WaitUntil(() => Input.anyKeyDown);
        else
            yield return new WaitUntil(() => Input.GetKeyDown(dismissKey));

        yield return Fade(1f, 0f, fadeOutDuration);
        gameObject.SetActive(false);
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        canvasGroup.alpha = from;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
