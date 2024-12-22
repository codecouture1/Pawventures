using UnityEngine;
using UnityEngine.UI;


public class ItemSelector : MonoBehaviour
{
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();

        primaryImage = primaryIcon.GetComponent<Image>();
        secondaryImage = secondaryIcon.GetComponent<Image>();
        switchAnimator = switchIcon.GetComponent<Animator>();

        //primaryIconAnimator = primaryIcon.GetComponent<Animator>();
        //secondaryIconAnimator = secondaryIcon.GetComponent<Animator>();
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


        //    if (primaryPowerup is GigaBeller)
        //    {
        //        primaryIconAnimator.SetTrigger("Gigabeller");
        //    }

        //    if (primaryPowerup is Halsband)
        //    {
        //        primaryIconAnimator.SetTrigger("Halsband");
        //    }

        //    if (primaryPowerup is CoinMagnet)
        //    {
        //        primaryIconAnimator.SetTrigger("Magnet");
        //    }

        //    if (primaryPowerup is Doppelsprung)
        //    {
        //        primaryIconAnimator.SetTrigger("Doppelsprung");
        //    }

        //    if (primaryPowerup == null)
        //    {
        //        primaryIconAnimator.SetTrigger("Empty");
        //    }


        //    if (secondaryPowerup is GigaBeller)
        //    {
        //        secondaryIconAnimator.SetTrigger("Gigabeller");
        //    }

        //    if (secondaryPowerup is Halsband)
        //    {
        //        secondaryIconAnimator.SetTrigger("Halsband");
        //    }

        //    if (secondaryPowerup is CoinMagnet)
        //    {
        //        secondaryIconAnimator.SetTrigger("Magnet");
        //    }

        //    if (secondaryPowerup is Doppelsprung)
        //    {
        //        secondaryIconAnimator.SetTrigger("Doppelsprung");
        //    }

        //    if (secondaryPowerup == null)
        //    {
        //        secondaryIconAnimator.SetTrigger("Empty");
        //    }
    }

}