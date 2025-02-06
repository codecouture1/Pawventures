using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReferenceManager : MonoBehaviour
{
    //private GameObject referenceManagerObj;
    //private ReferenceManager referenceManager;

    //referenceManagerObj = GameObject.Find("ReferenceManager");
    //referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

    public GameObject player;
    public PlayerScript playerScript { get; private set; }

    public GameObject hunter;
    public HunterScript hunterScript { get; set; }

    public GameObject m_camera;
    public CameraScript cameraScript { get; private set; }

    public GameObject itemSelector;
    public ItemSelector itemSelectorScript { get; private set; }
    
    public GameObject inventory;
    public InventoryInterface inventoryScript { get; set; }

    public CoinManager coinManager;

    public CollectibleManager collectibleManager;

    public GameObject deathscreen;

    public TimerManager TimerManager;

    public CanvasManager canvasManager;

    public Animator exitAnimator;

    public Sprite halsband;
    public Sprite gigaBeller;
    public Sprite doppelsprung;
    public Sprite münzmagnet;
    public Sprite doubleCoins;

    public AudioClip halsbandSound;
    public AudioClip gigaBellerSound;
    public AudioClip doppelsprungSound;
    public AudioClip münzmagnetSound;

    void Awake()
    {
        playerScript = player.GetComponent<PlayerScript>();
        hunterScript = hunter.GetComponent<HunterScript>();
        cameraScript = m_camera.GetComponent<CameraScript>();
        itemSelectorScript = itemSelector.GetComponent<ItemSelector>();
        inventoryScript = inventory.GetComponent<InventoryInterface>();
    }
}
