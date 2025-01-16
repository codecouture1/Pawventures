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

        //create IPowerup objects based on the gameData;
        primaryPowerup = IPowerUp.GetPowerUp(GameData.Instance.firstPowerUp);
        secondaryPowerup = IPowerUp.GetPowerUp(GameData.Instance.secondPowerUp);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayItem();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchItems();
            switchAnimator.SetTrigger("switch");
        }

        if (Input.GetKeyDown(KeyCode.E) && primaryPowerup != null)
        {
            //apply the powerup in the primary slot
            primaryPowerup.ApplyPowerup();

            //play the powerup sound
            audioSource.clip = primaryPowerup.Sound;
            audioSource.Play();

            //clear the slot after use
            primaryPowerup = null;

            //auto-switch so the primary slot isn't empty
            SwitchItems();
        }
    }

    //switch positions of the items
    private void SwitchItems()
    {
        IPowerUp primaryChache = primaryPowerup;
        SetItem(1, secondaryPowerup);
        SetItem(2, primaryChache);
    }

    //equip an item in the first or secod slot
    private void SetItem(int slot, IPowerUp powerUp)
    {
        switch (slot)
        {
            //equip item in slot 1 and update Game Data
            case 1:
                primaryPowerup = powerUp;
                if (powerUp != null)
                    GameData.Instance.firstPowerUp = powerUp.PowerUp;
                else
                    GameData.Instance.firstPowerUp = PowerUps.None;
                break;

            //equip item in slot 2 and update Game Data
            case 2:
                secondaryPowerup = powerUp;
                if (powerUp != null)
                    GameData.Instance.secondPowerUp = powerUp.PowerUp;
                else
                    GameData.Instance.secondPowerUp = PowerUps.None;
                break;

            default:
                Debug.LogError($"{slot} is an invalid slot number. plase select 1 or 2 as a slot number");
                break;
        }
    }

    //add item to a slot or apply it directly on pickup
    public void AddItem(IPowerUp powerUp)
    {
        //if primary slot is empty, item will be added here
        if(primaryPowerup == null)
        {
            SetItem(1, powerUp);
        } 
        //if primary slot is occupied, item will be added here
        else if (secondaryPowerup == null)
        {
            SetItem(2, powerUp);
        }
        //if both slots are occupied, item is applied directly
        else
        {
            audioSource.clip = powerUp.Sound;
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
            primaryImage.sprite = primaryPowerup.Sprite;
        }      
        else
            primaryImage.enabled = false;

        if (secondaryPowerup != null)
        {
            secondaryImage.enabled = true;
            secondaryImage.sprite = secondaryPowerup.Sprite;
        }
        else
            secondaryImage.enabled = false;
    }

}