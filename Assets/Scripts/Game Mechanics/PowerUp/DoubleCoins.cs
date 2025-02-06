using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DoubleCoins : IPowerUp
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;
    private CollectibleManager collectibleManager;

    public PowerUps PowerUp
    {
        get { return PowerUps.DoubleCoins; }
    }

    public Sprite Sprite
    {
        get { return referenceManager.doubleCoins; }
    }

    public AudioClip Sound
    {
        //TODO: Add sound
        get { return referenceManager.halsbandSound; }
    }

    public float Duration
    {
        get { return 13f; }
    }

    public void ApplyPowerup()
    {
        referenceManager.TimerManager.AddTimer(Duration, Sprite, PowerUp);
        collectibleManager.StartCoroutine(DoubleCoinValue());
    }

    public IEnumerator DoubleCoinValue()
    {
        for (float elapsedTime = 0; elapsedTime <= Duration; elapsedTime += Time.deltaTime)
        {
            collectibleManager.amount = 2;
            yield return null;
        }
        collectibleManager.amount = 1;
    }

    public DoubleCoins()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

        collectibleManager = referenceManager.collectibleManager;
    }
}
