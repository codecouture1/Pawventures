using System.Collections;
using UnityEngine;

public class GigaBeller : IPowerUp
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private HunterScript hunterScript;
    private CameraScript camScript;

    public void SetUp()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

        hunterScript = referenceManager.hunterScript;
        camScript = referenceManager.cameraScript;
    }

    public void  ApplyPowerup()
    {
        SetUp();
        hunterScript.StartCoroutine(hunterScript.ResetPositionCoroutine());
        camScript.StartCoroutine(camScript.Rumble(5f, 0.5f));
    }
}
