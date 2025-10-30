using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene("City");

        //Debug.Log("Scene not setup in build yet");

        //builds next scene when pressing play
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        Debug.Log("Options not implemented yet");
    }

    public void QuitGame()
    {

        //for actual game build
        //if (UNITY_EDITOR)
        //{
        //    EditorApplication.isPlaying = false;
        //}
        //else
        //{
        //    Application.Quit();
        //    Debug.Log("Game has been quit");
        //}

        //for in engine testing
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
                Debug.Log("Game has been quit");
        #endif

    }
}
