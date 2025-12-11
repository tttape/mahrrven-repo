using UnityEngine;

public enum NPCState { Alive, Dead, Collected }

public class NPCSoul : MonoBehaviour
{
    [Header("State")]
    public NPCState state = NPCState.Alive;

    [Header("References")]
    public OutlineCreator outlineCreator;   // Outline/highlight script
    public GameObject soulPrefab;           // Floating soul prefab
    public Transform soulSpawnPoint;        // Where the soul appears

    [Header("Soul Settings")]
    public int soulValue = 1;
    public float soulLifetime = 1.2f;       // Time for soul orb to disappear

    [Header("Dialogue")]
    [TextArea(2, 6)]
    public string[] dialogueLines;

    // Start dialogue with this NPC
    public void StartDialogue()
    {
        if (state != NPCState.Alive) return;

        // Disable player movement during dialogue
        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        if (player != null) player.enabled = false;

        // Start dialogue and call KillNPC when finished
        DialogueSystem.Instance.StartDialogue(dialogueLines, () =>
        {
            KillNPC();

            // Re-enable player movement
            if (player != null) player.enabled = true;
        });
    }

    // Call when the NPC dies (after dialogue)
    public void KillNPC()
    {
        if (state != NPCState.Alive) return;

        state = NPCState.Dead;

        // Show outline to indicate soul can be collected
        if (outlineCreator != null)
            outlineCreator.SetOutlineVisible(true);

        // Optional: disable NPC collider so player can't "walk into" it
        // Collider col = GetComponent<Collider>();
        // if (col != null) col.enabled = false;
    }

    // Call when player clicks to collect soul
    public void CollectSoul()
    {
        if (state != NPCState.Dead) return;

        state = NPCState.Collected;

        // Hide outline
        if (outlineCreator != null)
            outlineCreator.SetOutlineVisible(false);

        // Spawn the soul prefab
        if (soulPrefab != null && soulSpawnPoint != null)
        {
            GameObject soul = Instantiate(
                soulPrefab,
                soulSpawnPoint.position,
                soulSpawnPoint.rotation // optional: keep prefab rotation
            );

            // Destroy prefab after lifetime (or let its animation handle destruction)
            Destroy(soul, soulLifetime);
        }

        // Add souls to SoulManager
        SoulManager.Instance.AddSoul(soulValue);
    }

    // Optional: click to collect soul
    private void OnMouseDown()
    {
        if (state == NPCState.Dead)
            CollectSoul();
    }
}
