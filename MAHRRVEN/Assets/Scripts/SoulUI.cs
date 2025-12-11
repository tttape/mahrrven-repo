using UnityEngine;
using TMPro;

public class SoulUI : MonoBehaviour
{
    public TMP_Text soulText; // assign in Inspector

    void Update()
    {
        if (SoulManager.Instance != null && soulText != null)
        {
            soulText.text = "Souls: " + SoulManager.Instance.soulCount;
        }
    }
}
