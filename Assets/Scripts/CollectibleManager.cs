using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{

    public CoinManager coinManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Münzen
        if (coll.gameObject.CompareTag("Coin"))
        {
            Destroy(coll.gameObject);
            coinManager.AddCoins(1);   
        }
    }
}
