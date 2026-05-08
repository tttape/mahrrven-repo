using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
   
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Home");

       
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
