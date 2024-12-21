using System;
using UnityEngine;

public class PowerUpCollectible : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public ItemSelector itemSelector;
    private IPowerUp powerUp;
    private IPowerUp[] powerUps = new IPowerUp[5];

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        powerUps[0] = null;
        powerUps[1] = new Halsband();
        powerUps[2] = new GigaBeller();
        powerUps[3] = new Doppelsprung();
        powerUps[4] = new CoinMagnet();

        SetPowerUp(1);
        spriteRenderer.sprite = powerUp.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (powerUp != null)
            {
                itemSelector.AddItem(powerUp);
                spriteRenderer.enabled = false;
            }
            else
                Debug.Log("PowerUp undefined");
        }
    }

    public void SetPowerUp(int index)
    {
        powerUp = powerUps[index];
    }
}
