using UnityEngine;

public class CoinMagnet : IPowerUp
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    public PowerUps PowerUp
    {
        get { return PowerUps.CoinMagnet; }
    }

    public Sprite Sprite
    {
        get { return referenceManager.münzmagnet; }
    }

    public AudioClip Sound
    {
        get { return referenceManager.münzmagnetSound; }
    }

    public void ApplyPowerup()
    {
        Debug.Log("TODO");
    }

    public CoinMagnet()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
    }
}
