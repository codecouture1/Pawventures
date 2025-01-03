using UnityEngine;

public class CoinMagnet : IPowerUp
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    public Sprite sprite
    {
        get { return referenceManager.m�nzmagnet; }
    }

    public AudioClip sound
    {
        get { return referenceManager.m�nzmagnetSound; }
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
