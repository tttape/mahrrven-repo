using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopExit : MonoBehaviour
{
    [Header("Scene to Load")]
    public string sceneToLoad = "City"; // Change to your shop scene name

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered doorway trigger!");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
