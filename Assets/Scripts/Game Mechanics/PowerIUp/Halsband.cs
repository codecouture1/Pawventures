using UnityEngine;

public class Halsband : IPowerUp
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private PlayerScript pScript;

    public Sprite sprite
    {
        get { return referenceManager.halsband; }
    }

    public void ApplyPowerup()
    {
        pScript.health++;
    }

    public Halsband()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

        pScript = referenceManager.playerScript;
    }
}
