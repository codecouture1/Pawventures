using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class ItemSelector : MonoBehaviour
{
    private PlayerData playerData = new();

    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    [HideInInspector] public IPowerUp primaryPowerup;
    [HideInInspector] public IPowerUp secondaryPowerup;

    //The GameObjects of the PowerUp icons
    public GameObject primaryIcon;
    public GameObject secondaryIcon;

    //The Image Components of the PowerUp icons
    private Image primaryImage;
    private Image secondaryImage;


    //private Animator primaryIconAnimator;
    // private Animator secondaryIconAnimator;

    public GameObject switchIcon;
    private Animator switchAnimator;

    private void Awake()
    {
        playerData = PlayerDataManager.LoadData();
    }

    void Start()
    {     
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

        primaryImage = primaryIcon.GetComponent<Image>();
        secondaryImage = secondaryIcon.GetComponent<Image>();
        switchAnimator = switchIcon.GetComponent<Animator>();

        primaryPowerup = GetPowerUp(playerData.firstPowerUp);
        secondaryPowerup = GetPowerUp(playerData.secondPowerUp);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayItem();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchItems();
        }

        if (Input.GetKeyDown(KeyCode.E) && primaryPowerup != null)
        {
            primaryPowerup.ApplyPowerup();
            primaryPowerup = null;
        }
    }

    private IPowerUp GetPowerUp(PowerUps powerUpEnum)
    {
        switch (powerUpEnum)
        {
            case PowerUps.Halsband:
                return new Halsband();
            case PowerUps.Doppelsprung:
                return new Doppelsprung();
            case PowerUps.GigaBeller:
                return new GigaBeller();
            case PowerUps.CoinMagnet:
                return new CoinMagnet();
            case PowerUps.None:
                Debug.Log("No PowerUp Equipped");
                return null;
            default:
                Debug.LogError("PowerUp does not Exist");
                return null;

        }
    }

    private void SwitchItems()
    {
        IPowerUp primaryChache = primaryPowerup;
        IPowerUp secondaryChache = secondaryPowerup;
        SetItem(1, secondaryChache);
        SetItem(2, primaryChache);
        switchAnimator.SetTrigger("switch");
    }

    private void SetItem(int slot, IPowerUp powerUp)
    {
        switch (slot)
        {
            case 1:
                primaryPowerup = powerUp;
                break;
            case 2:
                secondaryPowerup = powerUp;
                break;
        }
    }

    public void AddItem(IPowerUp powerUp)
    {
        if(primaryPowerup == null)
        {
            primaryPowerup = powerUp;
        } 
        else if (secondaryPowerup == null)
        {
            secondaryPowerup = powerUp;
        } else
        {
            powerUp.ApplyPowerup();
        }
    }

    //animation
    private void DisplayItem()
    {

        if (primaryPowerup != null)
        {
            primaryImage.enabled = true;
            primaryImage.sprite = primaryPowerup.sprite;
        }      
        else
            primaryImage.enabled = false;

        if (secondaryPowerup != null)
        {
            secondaryImage.enabled = true;
            secondaryImage.sprite = secondaryPowerup.sprite;
        }
        else
            secondaryImage.enabled = false;
    }

}