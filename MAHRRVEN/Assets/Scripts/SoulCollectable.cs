using UnityEngine;

public class SoulCollectable : MonoBehaviour
{
    public int soulValue = 1;

    public void Collect()
    {
        SoulManager.Instance.AddSoul(soulValue);
        Destroy(gameObject); // Remove NPC object after collecting
    }
}
