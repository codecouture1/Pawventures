using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class CanvasManager : MonoBehaviour
{
    public GameObject spamW;
    public GameObject player;
    private PlayerScript pScript;
    public GameObject deathScreen;
    private Coroutine displayDeathscreen;
    void Start()
    {
        pScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        spamW.SetActive(pScript.slowed && !pScript.slowChallengeFailed);
        if (!pScript.alive() && displayDeathscreen == null)
            displayDeathscreen = StartCoroutine(DisplayDeathScreen());
        //TODO todessound
    }

    private IEnumerator DisplayDeathScreen()
    { 
        yield return new WaitForSeconds(1f);
        deathScreen.SetActive(true);
        yield return null;
    }

    //private void DisplayDeathScreen(bool input)
    //{
    //    deathScreen.SetActive(input);
    //}
}
