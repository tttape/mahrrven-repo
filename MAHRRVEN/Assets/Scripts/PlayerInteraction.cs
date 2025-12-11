using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f; // Max distance to interact
    private Camera playerCamera;

    private void Awake()
    {
        playerCamera = Camera.main;
        if (playerCamera == null)
            Debug.LogError("No main camera assigned to PlayerInteraction!");
    }

    private void Update()
    {
        if (PauseMenu.GameIsPaused) return;

        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                NPCSoul npc = hit.collider.GetComponent<NPCSoul>();
                if (npc != null)
                {
                    if (npc.state == NPCState.Alive)
                        npc.StartDialogue();
                    else if (npc.state == NPCState.Dead)
                        npc.CollectSoul();
                }
            }
        }
    }
}
