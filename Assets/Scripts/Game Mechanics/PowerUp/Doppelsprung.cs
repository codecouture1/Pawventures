using UnityEngine;

public class Doppelsprung : IPowerUp
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private PlayerScript pScript;

    public PowerUps PowerUp
    {
        get { return PowerUps.Doppelsprung; }
    }

    public Sprite Sprite
    {
        get { return referenceManager.doppelsprung; }
    }

    public AudioClip Sound
    {
        get { return referenceManager.doppelsprungSound; }
    }

    public void ApplyPowerup()
    {
        pScript.doubleJump = true;
    }

    public Doppelsprung()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

        pScript = referenceManager.playerScript;
    }
}
