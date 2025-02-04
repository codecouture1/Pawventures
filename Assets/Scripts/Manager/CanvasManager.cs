using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class CanvasManager : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;
    public GameObject checkpointObject;
    private Checkpoint checkpoint;

    public GameObject spamW;
    private PlayerScript pScript;
    public GameObject deathScreen;
    private Coroutine displayDeathscreen;

    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
    }

    private void Start()
    {
        pScript = referenceManager.playerScript;
    }

    void Update()
    {
        spamW.SetActive(pScript.slowed && !pScript.slowChallengeFailed);
        if (!pScript.alive() && displayDeathscreen == null)
        {
            displayDeathscreen = StartCoroutine(DisplayDeathScreen());
        }
    }

    public void NullCoroutine()
    {
        displayDeathscreen = null;
    }

    private IEnumerator DisplayDeathScreen()
    {
        yield return new WaitForSeconds(1f);
        deathScreen.SetActive(true);
        yield return null;
    }
}
