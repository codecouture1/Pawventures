using UnityEngine;
using UnityEngine.UI;

public class InventorySpriteManager : MonoBehaviour
{
    private InventoryInterface inventoryInterface;

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
        inventoryInterface = GetComponent<InventoryInterface>();
    }

    private void Update()
    {
        
    }

    public void DisplayItemSlots()
    {
        InventoryItem[] inventory = inventoryInterface.GetInventory();

        if (inventory[0].amount != 0)
            slot1.sprite = halsband;
        else
            slot1.sprite = halsband_EMPTY;

        if (inventory[1].amount != 0)
            slot2.sprite = doppelsprung;
        else
            slot2.sprite = doppelsprung_EMPTY;

        if (inventory[2].amount != 0)
            slot3.sprite = gigaBeller;
        else
            slot3.sprite = gigaBeller_EMPTY;

        if (inventory[3].amount != 0)
            slot4.sprite = münzmagnet;
        else
            slot4.sprite = münzmagnet_EMPTY;
    }

    public void DisplayEmptySlot(int slot)
    {
        if (slot == 1)
            primarySprite.enabled = false;

        if (slot == 2)
            secondarySprite.enabled = false;
    }

    public void DisplayPrimaryPowerUp(InventoryItem item)
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
                break;
        }
        
    }

    public void DisplaySecondaryPowerUp(InventoryItem item)
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
                break;
        }
        
    }
}
