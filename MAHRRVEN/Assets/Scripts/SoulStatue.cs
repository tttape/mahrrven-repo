using UnityEngine;

public class SoulStatue : MonoBehaviour
{
    [SerializeField] int requiredSouls = 1;

    // Called when the player clicks on the statue
    public void Deliver()
    {
        Debug.Log("Statue clicked!");
        if (SoulManager.Instance == null)
        {
            Debug.LogWarning("SoulManager not found!");
            return;
        }

        if (SoulManager.Instance.UseSouls(requiredSouls))
        {
            Debug.Log("Souls delivered!");
            // Optional: play effect or reward player
        }
        else
        {
            Debug.Log("Not enough souls to deliver.");
        }
    }
}
