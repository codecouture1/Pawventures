using UnityEngine;

public class Doppelsprung : IPowerUp
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private PlayerScript pScript;

    public void ApplyPowerup()
    {
        SetUp();
        pScript.doubleJump = true;
    }

    public void SetUp()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

        pScript = referenceManager.playerScript;
    }
}
