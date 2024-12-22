using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    void Update()
    {
        // Öffnet das Pausenmenü bei Druck auf "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseMenuPanel != null)
            {
                pauseMenuPanel.SetActive(true);
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        //Schließt bisher nur das PausemenuPanel...
        if (pauseMenuPanel != null)
      {
         bool isActive = pauseMenuPanel.activeSelf;
         pauseMenuPanel.SetActive(!isActive);
      }

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