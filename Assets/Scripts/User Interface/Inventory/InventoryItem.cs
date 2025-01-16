using UnityEngine;

public class InventoryItem
{
    public PowerUps powerUp;
    public int amount;

    public InventoryItem(PowerUps powerUp)
    {
        this.powerUp = powerUp;
        amount = GetAmount(powerUp);
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
            default:
                Debug.LogError("PowerUp does not Exist");
                return 0;

        }
    }
}
