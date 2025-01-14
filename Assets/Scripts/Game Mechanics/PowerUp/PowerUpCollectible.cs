using System;
using UnityEngine;

public class PowerUpCollectible : MonoBehaviour
{
    public bool spawnAsHalsband = false;
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

        if (spawnAsHalsband)
            SetAsHalsband();
        else
            SetPowerUp();

        spriteRenderer.sprite = powerUp.sprite; //assigns the correct PowerUp sprite to the game object
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

    public void SetAsHalsband()
    {
        powerUp = new Halsband();
    }
}
