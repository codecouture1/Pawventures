using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopMenuManual : MonoBehaviour
{

    // Das Panel, das aktiviert und deaktiviert werden soll
    public GameObject panel;



    // Button-Methoden zum Aktivieren und Deaktivieren des Panels
    public void ShowPanel()
    {
        panel.SetActive(true);  // Panel aktivieren
    }

    public void HidePanel()
    {
        panel.SetActive(false);  // Panel deaktivieren
    }
    public void ChangeScene()
        {
        SceneManager.LoadScene(1);
        }
}