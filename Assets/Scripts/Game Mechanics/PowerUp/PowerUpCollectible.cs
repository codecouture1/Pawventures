using System;
using UnityEngine;

public class PowerUpCollectible : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private SpriteRenderer spriteRenderer;
    private ItemSelector itemSelector;

    private IPowerUp powerUp; //the kind of PowerUp this collectible holds

    //holds all powerups in the game
    private IPowerUp[] powerUps;


    void Awake()
    {
       
    }

    void Start()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
        itemSelector = referenceManager.itemSelectorScript;

        powerUps = new IPowerUp[] { null, new Halsband(), new GigaBeller(), new Doppelsprung(), new CoinMagnet() };
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetPowerUp();
        spriteRenderer.sprite = powerUp.sprite; //assigns the correct PowerUp sprite to the game object
        Debug.Log(itemSelector);
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log(powerUp);
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
        int powerUpSetter = UnityEngine.Random.Range(1, 5);
        powerUp = powerUps[powerUpSetter];
    }
}
