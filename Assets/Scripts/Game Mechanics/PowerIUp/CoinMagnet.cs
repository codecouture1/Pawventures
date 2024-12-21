using UnityEngine;

public class CoinMagnet : IPowerUp
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    public Sprite sprite
    {
        get { return referenceManager.m�nzmagnet; }
    }

    public void ApplyPowerup()
    {

    }

    public CoinMagnet()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
    }
}
