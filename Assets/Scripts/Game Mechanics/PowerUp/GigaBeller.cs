using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GigaBeller : IPowerUp
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private HunterScript hunterScript;
    private CameraScript camScript;

    public Sprite sprite 
    { 
        get { return referenceManager.gigaBeller; }
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
