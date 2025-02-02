using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GigaBeller : IPowerUp
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private HunterScript hunterScript;
    private CameraScript camScript;

    public PowerUps PowerUp
    {
        get { return PowerUps.GigaBeller; }
    }

    public Sprite Sprite 
    { 
        get { return referenceManager.gigaBeller; }
    }

    public AudioClip Sound
    {
        get { return referenceManager.gigaBellerSound; }
    }

    public float Duration
    {
        get
        {
            Debug.LogError("This PowerUp has no duration");
            return 0f;
        }
    }

    public GigaBeller()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

        hunterScript = referenceManager.hunterScript;
        camScript = referenceManager.cameraScript;
    }

    public void ApplyPowerup()
    {
        hunterScript.StartCoroutine(hunterScript.ResetPositionCoroutine());
        camScript.StartCoroutine(camScript.Rumble(5f, 0.5f));
    }
}
