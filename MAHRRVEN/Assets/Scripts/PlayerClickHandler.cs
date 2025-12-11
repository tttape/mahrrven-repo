using UnityEngine;

public class PlayerClickHandler : MonoBehaviour
{
    public Camera cam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                NPCSoul npc = hit.collider.GetComponent<NPCSoul>();

                if (npc != null)
                {
                    if (npc.state == NPCState.Alive)
                    {
                        npc.StartDialogue();
                    }
                    else if (npc.state == NPCState.Dead)
                    {
                        npc.CollectSoul();
                    }
                    return;
                }

                // If we clicked a soul
                SoulCollectable sc = hit.collider.GetComponent<SoulCollectable>();
                if (sc != null)
                {
                    sc.Collect();
                    return;
                }

                // If we clicked a statue
                SoulStatue statue = hit.collider.GetComponent<SoulStatue>();
                if (statue != null)
                {
                    statue.Deliver(); // Calls the method above
                    return;
                }
            }
        }
    }
}
