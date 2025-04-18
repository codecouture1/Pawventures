using System;
using UnityEngine;

public class PowerUpCollectible : MonoBehaviour
{
    public bool spawnAsHalsband = false; //force to spawn as Halsband (used in the tutorial)

    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private SpriteRenderer spriteRenderer;
    private ItemSelector itemSelector;

    private IPowerUp powerUp; //the kind of PowerUp this collectible holds

    //holds all powerups in the game
    private IPowerUp[] powerUps;

    void Start()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
        itemSelector = referenceManager.itemSelectorScript;

        powerUps = new IPowerUp[] { new Halsband(), new GigaBeller(), new Doppelsprung(), new CoinMagnet(), new DoubleCoins() };
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spawnAsHalsband)
            powerUp = new Halsband();
        else
            SetPowerUp();

        spriteRenderer.sprite = powerUp.Sprite; //assigns the correct PowerUp sprite to the game object
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

    //randomly chooses a PowerUp from powerUps[]
    public void SetPowerUp()
    {
        int powerUpSetter = UnityEngine.Random.Range(0, powerUps.Length);
        powerUp = powerUps[powerUpSetter];
    }
}
