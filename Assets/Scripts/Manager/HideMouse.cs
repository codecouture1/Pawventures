using UnityEngine;
using UnityEngine.SceneManagement;

public class HideMouse : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private PlayerScript pScript;
    private GameObject pauseMenu;
   
    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
    }

    void Update()
    {

        if (!referenceManager.playerScript.alive() || referenceManager.pauseMenu.activeSelf || referenceManager.inventory.activeSelf || referenceManager.strgPanel.activeSelf)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }

    void OnDisable()
    {
        // Reset cursor state when the script is disabled or scene changes
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
