using UnityEngine;

public class PowerUpCollectible : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public ItemSelector itemSelector;
    private IPowerUp powerUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetPowerUp(new GigaBeller());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

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

    public void SetPowerUp(IPowerUp collectedPowerup)
    {
        powerUp = collectedPowerup;
    }

    

}
