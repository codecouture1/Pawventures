using UnityEngine;
using UnityEngine.Windows;

public class CanvasManager : MonoBehaviour
{
    public GameObject spamW;
    public GameObject player;
    private PlayerScript pScript;
    public GameObject deathScreen;
    void Start()
    {
        pScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        spamW.SetActive(pScript.slowed && !pScript.slowChallengeFailed);
        deathScreen.SetActive(!pScript.alive());
    }

    //private void DisplayDeathScreen(bool input)
    //{
    //    deathScreen.SetActive(input);
    //}
}
