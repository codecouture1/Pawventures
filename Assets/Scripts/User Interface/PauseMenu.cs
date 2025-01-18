using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject controlsUI;
    public GameObject settingsPanel;
    public AudioSource clickSound;

    public static PauseMenu Instance { get; private set; } // Singleton instance

    public bool IsPaused { get; private set; } //returns true if pause menu is currently opened

    private void Awake()
    {
        // Singleton implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
    }

    void Update()
    {
        // Öffnet das Pausenmenü bei Druck auf "P" oder "ESC"
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && pauseMenuUI != null)
        {
            if (!IsPaused)
            {
                OpenPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
        
    }

    public void OpenPauseMenu()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
            clickSound.Play();
            IsPaused = true;
            Time.timeScale = 0;

        }
    }

    public void ClosePauseMenu()
    {
        if (pauseMenuUI != null)
        {
            settingsPanel.SetActive(false);
            pauseMenuUI.SetActive(false);
            IsPaused = false;
            Time.timeScale = 1;
        }
        if(controlsUI != null)
        {
            controlsUI.SetActive(false);
        }
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        ClosePauseMenu();
        SceneManager.LoadScene(1);
    }
    
    public void OpenInstagram(){
        Application.OpenURL("https://www.instagram.com/play.pawventures?utm_source=ig_web_button_share_sheet&igsh=ZDNlZDc0MzIxNw==");
    }

    public void OpenTikTok(){
        Application.OpenURL("https://www.tiktok.com/@tierschutzverein_limes?is_from_webapp=1&sender_device=pc");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        // Im Unity Editor wird dieser Code ausgeführt
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Im Build wird dieser Code ausgeführt
        Application.Quit();
#endif
    }
}