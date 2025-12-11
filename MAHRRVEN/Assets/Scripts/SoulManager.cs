using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulManager : MonoBehaviour
{
    public static SoulManager Instance;
    public event System.Action OnSoulCountChanged;

    public int soulCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddSoul(int amount)
    {
        soulCount += amount;
        OnSoulCountChanged?.Invoke();
        Debug.Log("Souls collected: " + soulCount);
    }

    public bool UseSouls(int amount)
    {
        if (soulCount >= amount)
        {
            soulCount -= amount;
            return true;
        }
        return false;
    }
}
