using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This class defines the behavior of the inventory interface that pops up when the player attempts to start a level

public class InventoryInterface : MonoBehaviour
{
    public GameObject inventoryInterface; //parent object
    public Toggle primarySlotToggle; //toggle component of the primary item slot
    public Toggle secondarySlotToggle; //toggle component of the secondary item slot
    private InventorySpriteManager inventorySpriteManager;

    public GameObject exitButton;
    public int LoadOnClick { get; set; } //index of the scene that gets loaded on click;

    public TextMeshProUGUI[] counters; //display how many of each items the player owns
    public Button[] buttons; //button components of each item slot

    public InventoryItem primaryItem;
    public InventoryItem secondaryItem;

    public InventoryItem Halsband { get; private set; }
    public InventoryItem Doppelsprung { get; private set; }
    public InventoryItem GigaBeller { get; private set; }
    public InventoryItem CoinMagnet { get; private set; }

    private void Awake()
    {
        //set loadOnClick to the current level start index from GameData
        try
        {
            LoadOnClick = GameData.Instance.levelStart[MainMenu.GetLevel()];
        } 
        catch (Exception e) { }

        //all items in the inventory
        Halsband = new(PowerUps.Halsband);
        Doppelsprung = new(PowerUps.Doppelsprung);
        GigaBeller = new(PowerUps.GigaBeller);
        CoinMagnet = new(PowerUps.CoinMagnet);

        inventorySpriteManager = GetComponent<InventorySpriteManager>();
    }

    void Start()
    {

        //initialize counters
        UpdateCounters();

        //initialize sprites for the inventory slots
        inventorySpriteManager.DisplayInventorySlots();

        //define the behavior of each button.
        buttons[0].onClick.AddListener(() => EquipItem(Halsband));
        buttons[1].onClick.AddListener(() => EquipItem(Doppelsprung));
        buttons[2].onClick.AddListener(() => EquipItem(GigaBeller));
        buttons[3].onClick.AddListener(() => EquipItem(CoinMagnet));

        //loads the items currently held by the player into the inventory
        primaryItem = GetItem(GameData.Instance.firstPowerUp);
        if (primaryItem != null)
            inventorySpriteManager.DisplayPrimaryPowerUp(primaryItem);

        secondaryItem = GetItem(GameData.Instance.secondPowerUp);
        if (secondaryItem != null)
            inventorySpriteManager.DisplaySecondaryPowerUp(secondaryItem);
    }

    private void EquipItem(InventoryItem item)
    {
        //check if player owns at least one of the selected item. 
        if (item.Amount > 0)
        {
            //current item is unequipped
            UnequipItem(GetActiveSlot());

            //the current item is equipped in the correct slot
            SetItem(GetActiveSlot(), item);

            //automatically switch to the second slot if it's empty;
            if (secondaryItem == null)
            {
                primarySlotToggle.isOn = false;
                secondarySlotToggle.isOn = true;
            }

            //Display the correct sprites
            inventorySpriteManager.DisplayPrimaryPowerUp(primaryItem);
            inventorySpriteManager.DisplaySecondaryPowerUp(secondaryItem);

            Debug.Log($"Equipped Player with {item.powerUp} in Slot {GetActiveSlot()}!");
        }
        else    
            Debug.Log($"Not Enough {item.powerUp}!");
    }

    //unequips the item in the selected slot
    public void UnequipItem(int slot)
    {
        if (slot == 1 && primaryItem != null)
        {
            GetItem(primaryItem.powerUp).AddAmount();
            primaryItem = null;
            Debug.Log($"Unequipped Slot {slot}!");
        }
        else if (slot == 2 && secondaryItem != null)
        {
            GetItem(secondaryItem.powerUp).AddAmount();
            secondaryItem = null;
            Debug.Log($"Unequipped Slot {slot}!");
        }

        inventorySpriteManager.DisplayPrimaryPowerUp(primaryItem);
        inventorySpriteManager.DisplaySecondaryPowerUp(secondaryItem);
        UpdateCounters();
    }

    private InventoryItem GetItem(PowerUps powerUp)
    {
        switch (powerUp)
        {
            case (PowerUps.Halsband):
                return Halsband;
            case (PowerUps.Doppelsprung):
                return Doppelsprung;
            case (PowerUps.GigaBeller):
                return GigaBeller;
            case (PowerUps.CoinMagnet):
                return CoinMagnet;
            default:
                return null;
        }
    }

    private void SetItem(int slot, InventoryItem item)
    {
        if (slot == 1)
        {
            primaryItem = item;
            item.ReduceAmount();
        }
        if (slot == 2)
        {
            secondaryItem = item;
            item.ReduceAmount();
        }
        UpdateCounters();
    }

    public int GetActiveSlot()
    {
        if (primarySlotToggle.isOn)
            return 1;
        return 2;
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

    //value of each item counter is initialized depending on how many items the player owns of each type
    private void UpdateCounters()
    {
        counters[0].text = $"x{Halsband.Amount}";
        counters[1].text = $"x{Doppelsprung.Amount}";
        counters[2].text = $"x{GigaBeller.Amount}";
        counters[3].text = $"x{CoinMagnet.Amount}";
    }

    //opens inventory
    public void OpenInventory()
    {
        inventoryInterface.SetActive(true);
        primarySlotToggle.isOn = true;
        secondarySlotToggle.isOn = false;
    }

    //closes inventory and unequips both items
    public void CloseInventory()
    {
        UnequipItem(1);      
        UnequipItem(2);
        
        GameData.Instance.SaveData();
        inventoryInterface.SetActive(false);
    }

    //disables the exit button. Exit button is not displayed in chapter transitions
    public void DisplayExitButton(bool value)
    {
        exitButton.SetActive(value);
    }

    //dynamically load the scene on start button click by getting LoadOnClick from ReferenceManager. This allows the scene to be changed during runtime
    public void LoadScene()
    {
        SceneManager.LoadScene(LoadOnClick);
    }

    //set the scene to be loaded on start button click manually
    public void SetSceneLoad(int level)
    {
        if(GameData.Instance.levelStart[level - 1] == 0)
        {
            GameData.Instance.levelStart[level - 1] = MainMenu.GetFirstChapter(level - 1);
            GameData.Instance.SaveData();
        }

        LoadOnClick = GameData.Instance.levelStart[level - 1];
        Debug.Log(LoadOnClick);
    }

}
