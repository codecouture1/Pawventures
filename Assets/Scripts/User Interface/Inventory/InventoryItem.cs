using UnityEngine;

public class InventoryItem
{
    public PowerUps powerUp;
    public int Amount { get; private set; }

    public Sprite Sprite { get; private set; }

    public InventoryItem(PowerUps powerUp)
    {     
        this.powerUp = powerUp;
        Amount = GetAmount(powerUp);
    }

    private int GetAmount(PowerUps powerUp)
    {
        switch (powerUp)
        {
            case PowerUps.Halsband:
                return GameData.Instance.halsBandCount;
            case PowerUps.Doppelsprung:
                return GameData.Instance.doubleJumpCount;
            case PowerUps.GigaBeller:
                return GameData.Instance.gigaBellerCount;
            case PowerUps.CoinMagnet:
                return GameData.Instance.coinMagnetCount;
            case PowerUps.DoubleCoins:
                return GameData.Instance.doubleCoinCount;
            default:
                Debug.LogError("PowerUp does not Exist");
                return 0;

        }
    }

    //increments the amount based on the input value
    public void AddAmount(int amount)
    {
        this.Amount += amount;
        UpdatePlayerData();
    }

    //increments the amount by one
    public void AddAmount()
    {
        Amount++;
        UpdatePlayerData();
    }

    //reduces the amount based on the input value
    public void ReduceAmount(int amount)
    {
        this.Amount -= amount;
        UpdatePlayerData();
    }

    //reduces the amount based by one
    public void ReduceAmount()
    {
        Amount--;
        UpdatePlayerData();
    }

    //sets the item count of the player data to the amount of the inventoryitem 
    private void UpdatePlayerData()
    {
        switch (powerUp)
        {
            case PowerUps.Halsband:
                GameData.Instance.halsBandCount = Amount;
                break;
            case PowerUps.Doppelsprung:
                GameData.Instance.doubleJumpCount = Amount;
                break;
            case PowerUps.GigaBeller:
                GameData.Instance.gigaBellerCount = Amount;
                break;
            case PowerUps.CoinMagnet:
                GameData.Instance.coinMagnetCount = Amount;
                break;
            case PowerUps.DoubleCoins:
                GameData.Instance.doubleCoinCount = Amount;
                break;
            default:
                Debug.LogError("PowerUp does not Exist");
                break;
        }
        GameData.Instance.SaveData();
    }
}
