using System.IO;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{


    private PlayerData playerData = new PlayerData();

    public TextMeshProUGUI coinText;

    private void Awake()
    {
        playerData = PlayerDataManager.LoadData();
    }

    void Update()
    {
        coinText.text = playerData.coinCount.ToString();
    }

    // Add coins and save the updated data
    public void AddCoins(int amount)
    {
        playerData.coinCount += amount;
        PlayerDataManager.SaveData(playerData);
    }

    public void RemoveCoins(int amount)
    {
        playerData = PlayerDataManager.LoadData();
        playerData.coinCount -= amount;
        PlayerDataManager.SaveData(playerData);
    }

    public int GetCoinCount()
    {
        return playerData.coinCount;
    }

    public void updateCointCount()
    {
        playerData = PlayerDataManager.LoadData();
    }

    public void ResetCoins()
    {
        playerData.coinCount = 0;
        PlayerDataManager.SaveData(playerData);
    }
}
