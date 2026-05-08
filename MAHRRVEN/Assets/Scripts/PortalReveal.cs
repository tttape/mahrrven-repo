using UnityEngine;
using TMPro;

public class PortalReveal : MonoBehaviour
{
    [Header("Portal to Reveal")]
    public GameObject portal;

    [Header("Click Settings")]
    public float clickDistance = 5f;

    [Header("Prompt")]
    public GameObject interactPrompt;

    private Camera mainCam;
    private bool portalRevealed = false;

    void Start()
    {
        mainCam = Camera.main;

        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }

    void Update()
    {
        // Don't do anything if portal already revealed
        if (portalRevealed) return;

        float distance = Vector3.Distance(transform.position,
            mainCam.transform.position);

        // Show prompt when close enough
        if (interactPrompt != null)
            interactPrompt.SetActive(distance < clickDistance);

        // Check for click
        if (Input.GetMouseButtonDown(0) && distance < clickDistance)
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(
                    new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit, clickDistance))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    RevealPortal();
                }
            }
        }
    }


    void RevealPortal()
    {
        if (portal != null)
        {
            portal.SetActive(true);
            portalRevealed = true;

            // Hide prompt after revealing
            if (interactPrompt != null)
                interactPrompt.SetActive(false);

            Debug.Log("Portal revealed!");
        }
    }
}
