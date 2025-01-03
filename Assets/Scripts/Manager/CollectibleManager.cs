using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    public CoinManager coinManager;

    void OnTriggerEnter2D(Collider2D coll)
    {
        //M�nzen
        if (coll.gameObject.CompareTag("Coin"))
        {
            coinManager.AddCoins(1);
        }
    }
}
