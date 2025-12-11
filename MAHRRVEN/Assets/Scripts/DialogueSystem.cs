using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;
    public DialogueUI dialogueUI;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Start a linear dialogue. npc is optional - if provided, we'll call EndDialogueOnNPC when finished.
    /// </summary>
    public void StartDialogue(string[] lines, System.Action finishedCallback = null)
    {

        dialogueUI.ShowDialogue(lines, finishedCallback);
        
    }
}

//if (dialogueUI == null)
//{
//    Debug.LogError("DialogueUI not set on DialogueSystem.");
//    return;
//}

//dialogueUI.ShowDialogue(lines, () =>
//{
//    // On dialogue finished:
//    if (npc != null)
//        npc.KillNPC();
//});