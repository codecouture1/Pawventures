using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;
    private PlayerScript pScript;

    public bool Opened { get; private set; } //returns true if pause menu is currently opened

    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
    }

    void Update()
    {
        if (referenceManager.deathscreen.activeSelf)
        {
            ClosePauseMenu();
        } else
        {
            // Öffnet das Pausenmenü bei Druck auf "P" oder "ESC"
            if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && pauseMenuPanel != null)
            {
                if (!Opened)
                {
                    OpenPauseMenu();
                }
                else
                {
                    ClosePauseMenu();
                }
            }
        }
    }

    public void OpenPauseMenu()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
            Opened = true;
            Time.timeScale = 0;
        }
    }

    public void ClosePauseMenu()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
            Opened = false;
            Time.timeScale = 1;
        }
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    
    public void OpenInstagram(){
        Application.OpenURL("https://www.instagram.com/play.pawventures?utm_source=ig_web_button_share_sheet&igsh=ZDNlZDc0MzIxNw==");
    }

    public void OpenTikTok(){
        Application.OpenURL("https://www.tiktok.com/@tierschutzverein_limes?is_from_webapp=1&sender_device=pc");
    }

     public void OpenSettings(GameObject Panel){

            if (Panel != null)
            {

                Panel.SetActive(true);
            }

    }

    public void OpenPauseMenuAfterSettings(GameObject Panel){

            if (pauseMenuPanel != null)
            {
                pauseMenuPanel.SetActive(true);
            }

            if (Panel != null)
            {
                bool isActive = Panel.activeSelf;
                Panel.SetActive(!isActive);
            }

    }
}