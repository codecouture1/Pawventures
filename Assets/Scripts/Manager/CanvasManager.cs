using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class CanvasManager : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    public GameObject spamW;
    private PlayerScript pScript;
    private GameObject deathScreen;
    public AudioSource deathSound;
    private Coroutine displayDeathscreen;

    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
    }

    private void Start()
    {
        pScript = referenceManager.playerScript;
        deathScreen = referenceManager.deathscreen;
    }

    void Update()
    {
        spamW.SetActive(pScript.slowed && !pScript.slowChallengeFailed);
        if (!pScript.alive() && displayDeathscreen == null)
            displayDeathscreen = StartCoroutine(DisplayDeathScreen());
    }

    private IEnumerator DisplayDeathScreen()
    { 
        yield return new WaitForSeconds(1f);
        deathScreen.SetActive(true);
        deathSound.Play();
        yield return null;
    }
}
