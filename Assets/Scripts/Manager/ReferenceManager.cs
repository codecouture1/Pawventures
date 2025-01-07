using UnityEngine;
using UnityEngine.SceneManagement;

public class ReferenceManager : MonoBehaviour
{
    //private GameObject referenceManagerObj;
    //private ReferenceManager referenceManager;

    //referenceManagerObj = GameObject.Find("ReferenceManager");
    //referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

    public GameObject player;
    public PlayerScript playerScript { get; private set; }

    public GameObject hunter;
    public HunterScript hunterScript { get; private set; }

    public GameObject m_camera;
    public CameraScript cameraScript { get; private set; }

    public GameObject itemSelector;
    public ItemSelector itemSelectorScript { get; private set; }
    
    public GameObject inventory;
    public InventoryInterface inventoryScript { get; private set; }

    public CoinManager coinManager;

    public GameObject deathscreen;
    public GameObject pauseMenu;
    public GameObject strgPanel;

    public int firstChapterIndex;
    public int LoadOnClick { get; set; } //the scene index that loads when inventory start button is pressed;

    public Animator exitAnimator;

    public Sprite halsband;
    public Sprite gigaBeller;
    public Sprite doppelsprung;
    public Sprite münzmagnet;

    public AudioClip halsbandSound;
    public AudioClip gigaBellerSound;
    public AudioClip doppelsprungSound;
    public AudioClip münzmagnetSound;

    void Awake()
    {
        LoadOnClick = firstChapterIndex;

        playerScript = player.GetComponent<PlayerScript>();
        hunterScript = hunter.GetComponent<HunterScript>();
        cameraScript = m_camera.GetComponent<CameraScript>();
        itemSelectorScript = itemSelector.GetComponent<ItemSelector>();
        inventoryScript = inventory.GetComponent<InventoryInterface>();
    }
}
