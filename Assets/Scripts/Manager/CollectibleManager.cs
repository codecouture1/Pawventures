using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;
    public GameObject coinMagnet;

    [HideInInspector] public int amount;

    private CoinManager coinManager;

    private void Awake()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
        coinManager = referenceManager.coinManager;
        amount = 1;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Münzen
        if (coll.gameObject.CompareTag("Coin"))
        {
            coinManager.AddCoins(amount);
        }
    }

    public IEnumerator CoinMagnetCoroutine(float duration)
    {
        Debug.Log("Coin Magnet activated for " + duration + " seconds.");
        GameObject coinMagnetObj = Instantiate(coinMagnet, transform.position, Quaternion.identity);
        coinMagnetObj.transform.SetParent(transform);
        yield return new WaitForSeconds(duration);
        Destroy(coinMagnetObj);
    }
}
