using System.Collections.Generic;
using UnityEngine;

public class CoinMagnetScript : MonoBehaviour
{
    CircleCollider2D myCollider;
    private float moveSpeed = 30f; // Speed of the coin movement
    private List<Transform> targetCoins = new List<Transform>(); // List to track multiple coins

    void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Check if the collided object is a coin
        if (coll.gameObject.CompareTag("Coin"))
        {
            targetCoins.Add(coll.transform); // Add the coin to the list
        }
    }

    void Update()
    {
        // Move each coin in the list toward the center of this game object
        for (int i = targetCoins.Count - 1; i >= 0; i--)
        {
            Transform coin = targetCoins[i];
            coin.position = Vector3.MoveTowards(
                coin.position,
                transform.position,
                moveSpeed * Time.deltaTime
            );

            // Remove the coin from the list if it reaches the center
            if (Vector3.Distance(coin.position, transform.position) < 0.1f)
            {
                coin.position = transform.position; // Snap to center
                targetCoins.RemoveAt(i); // Remove from the list
            }
        }
    }
}