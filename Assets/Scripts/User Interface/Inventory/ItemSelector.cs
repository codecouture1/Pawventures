using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class ItemSelector : MonoBehaviour
{
    [HideInInspector] public IPowerUp primaryPowerup;
    [HideInInspector] public IPowerUp secondaryPowerup;

    //The GameObjects of the PowerUp icons
    public GameObject primaryIcon;
    public GameObject secondaryIcon;

    //The Image Components of the PowerUp icons
    private Image primaryImage;
    private Image secondaryImage;

    public GameObject switchIcon;
    private Animator switchAnimator;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {     
        primaryImage = primaryIcon.GetComponent<Image>();
        secondaryImage = secondaryIcon.GetComponent<Image>();
        switchAnimator = switchIcon.GetComponent<Animator>();

        primaryPowerup = GetPowerUp(GameData.Instance.firstPowerUp);
        secondaryPowerup = GetPowerUp(GameData.Instance.secondPowerUp);
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
            audioSource.clip = primaryPowerup.sound;
            audioSource.Play();
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
                return null;
            default:
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
            audioSource.clip = powerUp.sound;
            audioSource.Play();
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