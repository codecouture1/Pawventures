using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private CoinManager coinManager;

    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
        coinManager = referenceManager.coinManager;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Münzen
        if (coll.gameObject.CompareTag("Coin"))
        {
            coinManager.AddCoins(1);
        }
    }
}
