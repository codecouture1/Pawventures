using UnityEngine;

public class Halsband : IPowerUp
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private PlayerScript pScript;

    public PowerUps PowerUp
    {
        get { return PowerUps.Halsband; }
    }

    public Sprite Sprite
    {
        get { return referenceManager.halsband; }
    }

    public AudioClip Sound
    {
        get { return referenceManager.halsbandSound; }
    }

    public void ApplyPowerup()
    {
        pScript.health = 2;
    }

    public Halsband()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

        pScript = referenceManager.playerScript;
    }
}
