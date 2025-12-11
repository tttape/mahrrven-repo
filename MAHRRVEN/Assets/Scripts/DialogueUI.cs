using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel; // panel root
    public TMP_Text dialogueText;
    public Button nextButton; // optional; can be left unassigned

    private string[] currentLines;
    private int currentIndex;
    private System.Action onFinished;

    void Start()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(false);

        // Optional: keep nextButton functionality if assigned
        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextPressed);
    }

    void Update()
    {
        // Only advance if dialogue panel is active
        if (dialoguePanel.activeSelf && Input.GetMouseButtonDown(0)) // left click
        {
            AdvanceDialogue();
        }
    }

    public void ShowDialogue(string[] lines, System.Action finishedCallback = null)
    {
        if (lines == null || lines.Length == 0) return;

        currentLines = lines;
        currentIndex = 0;
        onFinished = finishedCallback;

        dialoguePanel.SetActive(true);
        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        if (currentIndex < 0 || currentIndex >= currentLines.Length) return;

        dialogueText.text = currentLines[currentIndex];

        // optional: start a typewriter coroutine here if desired
    }

    private void AdvanceDialogue()
    {
        currentIndex++;

        if (currentIndex >= currentLines.Length)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();
        }
    }

    private void OnNextPressed()
    {
        AdvanceDialogue();
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        onFinished?.Invoke();
    }
}
