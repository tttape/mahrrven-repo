using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{


    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public Slider volumeSlider;

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);

        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f); // load saved volume or default 1
            AudioListener.volume = volumeSlider.value;

            // Add listener so volume updates live
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        // Lock cursor at start (if you’re using FPS-style control)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Hide and lock cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Unlock and show cursor for menu navigation
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
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

    void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value); // save volume setting
    }
}
