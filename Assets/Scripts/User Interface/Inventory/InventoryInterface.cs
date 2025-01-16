using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This class defines the behavior of the inventory interface that pops up when the player attempts to start a level

public class InventoryInterface : MonoBehaviour
{
    private GameObject referenceManagerObj;
    private ReferenceManager referenceManager;

    private InventoryItem[] inventory; //The player's inventory

    public GameObject inventoryInterface; //parent object
    public Toggle primarySlotToggle; //toggle component of the primary item slot
    public Toggle secondarySlotToggle; //toggle component of the secondary item slot
    private InventorySpriteManager inventorySpriteManager;

    public GameObject exitButton;
    public TextMeshProUGUI[] counters; //display how many of each items the player owns
    public Button[] buttons; //button components of each item slot

    public InventoryItem primaryItem;
    public InventoryItem secondaryItem;


    private void Awake()
    {
        inventorySpriteManager = GetComponent<InventorySpriteManager>();
        inventory = new InventoryItem[] { new(PowerUps.Halsband), new(PowerUps.Doppelsprung), new(PowerUps.GigaBeller), new(PowerUps.CoinMagnet) };
    }

    void Start()
    {
        UpdateCounters();
        inventorySpriteManager.DisplayItemSlots();

        //define the behavior of each button.
        buttons[0].onClick.AddListener(() => EquipItem(inventory[0]));
        buttons[1].onClick.AddListener(() => EquipItem(inventory[1]));
        buttons[2].onClick.AddListener(() => EquipItem(inventory[2]));
        buttons[3].onClick.AddListener(() => EquipItem(inventory[3]));

        if (GameData.Instance.firstPowerUp != 0)
        {
            Debug.Log("Hawk");
            primaryItem = inventory[(int)GameData.Instance.firstPowerUp - 1];
            inventorySpriteManager.DisplayPrimaryPowerUp(primaryItem);
        }

        if (GameData.Instance.secondPowerUp != 0)
        {
            Debug.Log("Tuah");
            secondaryItem = inventory[(int)GameData.Instance.secondPowerUp - 1];
            inventorySpriteManager.DisplaySecondaryPowerUp(secondaryItem);
        }
    }

    private void EquipItem(InventoryItem item)
    {
        //determines wether the primary or secondary slot is currently selected
        bool isPrimarySlot = primarySlotToggle.isOn;

        //assigns the value of the currently equipped item (null if there is none)
        InventoryItem currentItem = isPrimarySlot ? primaryItem : secondaryItem;

        //check if player owns at least one of the selected item. 
        if (item.amount > 0)
        {
            //if a current item has already been set, it gets added back to the inventory
            if (currentItem != null)
                inventory[(int)currentItem.powerUp - 1].amount++;

            //the current item is assigned
            currentItem = item;

            //the current item is equipped in the correct slot
            if (isPrimarySlot)
            {
                primaryItem = currentItem;
                inventorySpriteManager.DisplayPrimaryPowerUp(item);

                //automatically switch to the second slot if it's empty;
                if(secondaryItem == null)
                {
                    primarySlotToggle.isOn = false;
                    secondarySlotToggle.isOn = true;
                }
            } 
            else
            {
                secondaryItem = currentItem;
                inventorySpriteManager.DisplaySecondaryPowerUp(item);
            }

            //the item is taken out of the inventory
            item.amount--;
            Debug.Log($"Equipped Player with {item.powerUp} in {(isPrimarySlot ? "Slot 1" : "Slot 2")}!");
        }
        else    
            Debug.Log($"Not Enough {item.powerUp}!");

        UpdateInventory();
        GameData.Instance.SaveData();
        UpdateCounters();
        inventorySpriteManager.DisplayItemSlots();
    }

    //safe equipped items to player data. Executed on level start
    public void SaveItemsToPlayerData()
    {
        if (primaryItem != null)
            GameData.Instance.firstPowerUp = primaryItem.powerUp;
        else
            GameData.Instance.firstPowerUp = PowerUps.None;

        if (secondaryItem != null)
            GameData.Instance.secondPowerUp = secondaryItem.powerUp;
        else
            GameData.Instance.secondPowerUp = PowerUps.None;

        GameData.Instance.SaveData();
    }

    private void UpdateInventory()
    {
        GameData.Instance.halsBandCount = inventory[0].amount;
        GameData.Instance.doubleJumpCount = inventory[1].amount;
        GameData.Instance.gigaBellerCount = inventory[2].amount;
        GameData.Instance.coinMagnetCount = inventory[3].amount;
    }


    //value of each item counter is initialized depending on how many items the player owns of each type
    private void UpdateCounters()
    {
        int counter = 0;
        foreach (TextMeshProUGUI text in counters)
        {            
            text.text = $"x{inventory[counter].amount}";
            counter++;
        }

    }

    //opens inventory
    public void OpenInventory()
    {
        inventoryInterface.SetActive(true);
    }

    //closes inventory and unequips both items
    public void CloseInventory()
    {
        if(primaryItem != null)
        {
            inventory[(int)primaryItem.powerUp - 1].amount++;
            primaryItem = null;
        }
        if (secondaryItem != null)
        {
            inventory[(int)secondaryItem.powerUp - 1].amount++;
            secondaryItem = null;
        }
        UpdateInventory();
        GameData.Instance.SaveData();
        inventoryInterface.SetActive(false);
    }

    public InventoryItem[] GetInventory()
    {
        return inventory;
    }

    //disables the exit button. Exit button is not displayed in chapter transitions
    public void DisplayExitButton(bool value)
    {
        exitButton.SetActive(value);
    }

    //dynamically load the scene on start button click by getting LoadOnClick from ReferenceManager. This allows the scene to be changed during runtime
    public void LoadScene()
    {
        referenceManagerObj = GameObject.Find("ReferenceManager");
        referenceManager = referenceManagerObj.GetComponent<ReferenceManager>();
        SceneManager.LoadScene(referenceManager.LoadOnClick);
    }

    //set the scene to be loaded on start button click manually
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
