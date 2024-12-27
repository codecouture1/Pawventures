using UnityEngine;

public class InventoryItem
{
    PlayerData playerData;
    public PowerUps powerUp;
    public int amount;

    public InventoryItem(PowerUps powerUp)
    {
        this.playerData = PlayerDataManager.LoadData();
        this.powerUp = powerUp;
        this.amount = GetAmount(powerUp);
    }

    private int GetAmount(PowerUps powerUp)
    {
        switch (powerUp)
        {
            case PowerUps.Halsband:
                return playerData.halsBandCount;
            case PowerUps.Doppelsprung:
                return playerData.doubleJumpCount;
            case PowerUps.GigaBeller:
                return playerData.gigaBellerCount;
            case PowerUps.CoinMagnet:
                return playerData.coinMagnetCount;
            default:
                Debug.LogError("PowerUp does not Exist");
                return 0;

        }
    }
}
