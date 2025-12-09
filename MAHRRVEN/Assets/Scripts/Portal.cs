using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Transform tunnelBSpawn;  // Where player appears when arriving at B
    public Transform tunnelCSpawn;  // Where player appears when returning to C

    public PlayerController playerController;
    public ScreenFader fader;

    //public GameObject tunnelBUI;    // UI canvas for Tunnel B options

    private bool justTeleported = false; // prevents instant retrigger

    

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered: " + other.tag);
        //Debug.Log("Tag is: " + other.tag);
        //if (other.CompareTag("TunnelC") && !justTeleported)
        //{
        //    playerController.TeleportToTunnelB(tunnelBSpawn);
        //    StartCoroutine(TeleportCooldown());
        //}

        //if (other.CompareTag("TunnelB") && !justTeleported)
        //{
        //    playerController.tunnelBUI.SetActive(true);
        //    StartCoroutine(TeleportCooldown());
        //}

        if (other.CompareTag("TunnelC") && !justTeleported)
        {
            StartCoroutine(TeleportPlayer(tunnelBSpawn));
        }

        if (other.CompareTag("TunnelB") && !justTeleported)
        {
            ShowTunnelBUI();
        }
    }

    private IEnumerator TeleportPlayer(Transform spawnPoint)
    {
        // Prevent re-triggering immediately
        justTeleported = true;

        if (fader != null)
        {
            yield return StartCoroutine(fader.FadeOutIn(() =>
            {
                playerController.Teleport(spawnPoint);
            }));
        }
        else
        {
            playerController.Teleport(spawnPoint);
        }


        //// Fade out first (optional)
        //if (fader != null)
        //{
        //    yield return StartCoroutine(fader.FadeOutIn(() =>
        //    {
        //        // Disable CharacterController before moving
        //        var cc = playerController.CharacterControllerRef;
        //        if (cc != null)
        //            cc.enabled = false;

        //        // Move player
        //        playerController.transform.position = spawnPoint.position;
        //        playerController.transform.rotation = spawnPoint.rotation;

        //        // Re-enable CharacterController
        //        if (cc != null)
        //            cc.enabled = true;
        //    }));
        //}
        //else
        //{
        //    // If no fade, just teleport
        //    var cc = playerController.CharacterControllerRef;
        //    if (cc != null)
        //        cc.enabled = false;

        //    playerController.transform.position = spawnPoint.position;
        //    playerController.transform.rotation = spawnPoint.rotation;

        //    if (cc != null)
        //        cc.enabled = true;
        //}

        // Optional: small cooldown to prevent retriggering
        yield return new WaitForSeconds(0.25f);
        justTeleported = false;
    }

    //void TeleportToTunnelB()
    //{
    //    Debug.Log("TeleportToTunnelB() ran!");

    //    if (tunnelBSpawn == null)
    //    {
    //        Debug.LogError("Tunnel B Spawn is NOT ASSIGNED!");
    //        return;
    //    }

    //    // Get the CharacterController
    //    CharacterController cc = GetComponent<CharacterController>();

    //    if (cc != null)
    //    {
    //        // MUST disable the controller before modifying position
    //        cc.enabled = false;
    //        transform.position = tunnelBSpawn.position;
    //        transform.rotation = tunnelBSpawn.rotation; // optional but recommended
    //        cc.enabled = true;  // re-enable
    //    }
    //    else
    //    {
    //        // Fallback if you don't have CC (rigidbody controller)
    //        transform.position = tunnelBSpawn.position;
    //    }

    //    StartCoroutine(TeleportCooldown());
    //}

    //void ReturnToTunnelC()
    //{
    //    transform.position = tunnelCSpawn.position;
    //    tunnelBUI.SetActive(false);
    //    StartCoroutine(TeleportCooldown());
    //}

    void ShowTunnelBUI()
    {
        playerController.tunnelBUI.SetActive(true);
        //tunnelBUI.SetActive(true);

        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        //StartCoroutine(TeleportCooldown());
    }

    //void ReturnToNormalGameplay()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //}

    public void Button_ReturnToTunnelC()
    {
        playerController.tunnelBUI.SetActive(false);
        StartCoroutine(TeleportPlayer(tunnelCSpawn));
        //playerController.ReturnToTunnelC(tunnelCSpawn);
    }

    public void Button_LoadOtherLevel()
    {
        StartCoroutine(LoadSceneWithFade("Home"));
        //SceneManager.LoadScene("Home");
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        if (fader != null)
        {
            yield return StartCoroutine(fader.FadeOutIn(() =>
            {
                SceneManager.LoadScene(sceneName);
            }));
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    //System.Collections.IEnumerator TeleportCooldown()
    //{
    //    justTeleported = true;
    //    yield return new WaitForSeconds(0.25f); // prevents retrigger loops
    //    justTeleported = false;
    //}
}
