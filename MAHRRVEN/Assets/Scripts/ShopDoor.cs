using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Shop");
        }
    }
}
