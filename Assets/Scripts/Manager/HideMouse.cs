using UnityEngine;
using UnityEngine.SceneManagement;

public class HideMouse : MonoBehaviour
{
    public GameObject player;
    private playerScript pScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pScript = player.GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pScript.alive())
        {
            Cursor.visible = true;
        } else
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
