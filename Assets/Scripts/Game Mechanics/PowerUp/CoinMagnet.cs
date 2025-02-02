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

    public float Duration
    {
        get { return 10f; }
    }

    public void ApplyPowerup()
    {
        Debug.Log("TODO");
        referenceManager.TimerManager.AddTimer(Duration, Sprite, PowerUp);
    }

    public CoinMagnet()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
    }
}
