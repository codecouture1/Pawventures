using UnityEngine;
using UnityEngine.UI;

public class InventorySpriteManager : MonoBehaviour
{
    private InventoryInterface inventory;

    public Sprite halsband;
    public Sprite gigaBeller;
    public Sprite doppelsprung;
    public Sprite münzmagnet;

    public Sprite halsband_EMPTY;
    public Sprite gigaBeller_EMPTY;
    public Sprite doppelsprung_EMPTY;
    public Sprite münzmagnet_EMPTY;

    public Image primarySprite;
    public Image secondarySprite;

    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;

    private void Awake()
    {
        inventory = GetComponent<InventoryInterface>();
    }

    public void DisplayInventorySlots()
    {
        if (inventory.Halsband.Amount != 0)
            slot1.sprite = halsband;
        else
            slot1.sprite = halsband_EMPTY;

        if (inventory.Doppelsprung.Amount != 0)
            slot2.sprite = doppelsprung;
        else
            slot2.sprite = doppelsprung_EMPTY;

        if (inventory.GigaBeller.Amount != 0)
            slot3.sprite = gigaBeller;
        else
            slot3.sprite = gigaBeller_EMPTY;

        if (inventory.CoinMagnet.Amount != 0)
            slot4.sprite = münzmagnet;
        else
            slot4.sprite = münzmagnet_EMPTY;
    }

    public void DisplayPrimaryPowerUp(InventoryItem item)
    {
        if (item != null)
        {
            primarySprite.enabled = true;
            switch (item.powerUp)
            {
                case PowerUps.Halsband:
                    primarySprite.sprite = halsband;
                    break;
                case PowerUps.Doppelsprung:
                    primarySprite.sprite = doppelsprung;
                    break;
                case PowerUps.GigaBeller:
                    primarySprite.sprite = gigaBeller;
                    break;
                case PowerUps.CoinMagnet:
                    primarySprite.sprite = münzmagnet;
                    break;
                default:
                    primarySprite.enabled = false;
                    break;
            }
        }
        else
        {
            primarySprite.enabled = false;
        }
    }



    public void DisplaySecondaryPowerUp(InventoryItem item)
    {
        if (item != null)
        {
            secondarySprite.enabled = true;
            switch (item.powerUp)
            {
                case PowerUps.Halsband:
                    secondarySprite.sprite = halsband;
                    break;
                case PowerUps.Doppelsprung:
                    secondarySprite.sprite = doppelsprung;
                    break;
                case PowerUps.GigaBeller:
                    secondarySprite.sprite = gigaBeller;
                    break;
                case PowerUps.CoinMagnet:
                    secondarySprite.sprite = münzmagnet;
                    break;
                default:
                    secondarySprite.enabled = false;
                    break;
            }
        }
        else
        {
            secondarySprite.enabled = false;
        }        
    }
}
