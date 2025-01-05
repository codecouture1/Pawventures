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

    private PlayerData playerData = new(); //player Data

    private InventoryItem[] inventory; //The player's inventory

    public GameObject inventoryInterface; //parent object of the inventory
    public Toggle primarySlotToggle; //toggle component of the primary item slot
    private InventorySpriteManager inventorySpriteManager;

    public GameObject exitButton;
    public TextMeshProUGUI[] counters; //display how many of each items the player owns
    public Button[] buttons; //button components of each item slot

    public InventoryItem primaryItem;
    public InventoryItem secondaryItem;


    private void Awake()
    {
        playerData = PlayerDataManager.LoadData();
        inventorySpriteManager = GetComponent<InventorySpriteManager>();
        inventory = new InventoryItem[] { new(PowerUps.Halsband), new(PowerUps.Doppelsprung), new(PowerUps.GigaBeller), new(PowerUps.CoinMagnet), };
        UpdatePlayerData();
    }

    void Start()
    {
        inventorySpriteManager.DisplayEmptySlot(1);
        inventorySpriteManager.DisplayEmptySlot(2);
        UpdateCounters();
        inventorySpriteManager.DisplayItemSlots();

        //define the behavior of each button.
        buttons[0].onClick.AddListener(() => EquipItem(inventory[0]));
        buttons[1].onClick.AddListener(() => EquipItem(inventory[1]));
        buttons[2].onClick.AddListener(() => EquipItem(inventory[2]));
        buttons[3].onClick.AddListener(() => EquipItem(inventory[3]));
    }

    private void EquipItem(InventoryItem item)
    {
        InventoryItem currentItem;

        if (item.amount > 0)
        {
            if (primarySlotToggle.isOn)
            {
                if(primaryItem != null)
                {
                    currentItem = primaryItem;
                    inventory[(int)currentItem.powerUp - 1].amount++;
                }
                primaryItem = item;
                inventorySpriteManager.DisplayPrimaryPowerUp(item);
                item.amount--;
                Debug.Log($"Equippedd Player with {item.powerUp} in Slot 1!");
            }
            else
            {
                if (secondaryItem != null)
                {
                    currentItem = secondaryItem;
                    inventory[(int)currentItem.powerUp - 1].amount++;                   
                }
                inventorySpriteManager.DisplaySecondaryPowerUp(item);
                secondaryItem = item;
                item.amount--;
                Debug.Log($"Equippedd Player with {item.powerUp} in Slot 2!");
            }
        } else
        {
            Debug.Log($"Not Enough {item.powerUp}!");
        }
        UpdatePlayerData();
        PlayerDataManager.SaveData(playerData);
        UpdateCounters();
        inventorySpriteManager.DisplayItemSlots();
    }

    //safe equipped items to player data. Execute on level start
    public void SaveItemsToPlayerData()
    {
        if (primaryItem != null)
            playerData.firstPowerUp = primaryItem.powerUp;
        else
            playerData.firstPowerUp = PowerUps.None;

        if (secondaryItem != null)
            playerData.secondPowerUp = secondaryItem.powerUp;
        else
            playerData.secondPowerUp = PowerUps.None;

        PlayerDataManager.SaveData(playerData);
    }

    private void UpdatePlayerData()
    {
        playerData.halsBandCount = inventory[0].amount;
        playerData.doubleJumpCount = inventory[1].amount;
        playerData.gigaBellerCount = inventory[2].amount;
        playerData.coinMagnetCount = inventory[3].amount;
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

    //closes inventory
    public void CloseInventory()
    {
        inventoryInterface.SetActive(false);
    }

    public InventoryItem[] GetInventory()
    {
        return inventory;
    }

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
