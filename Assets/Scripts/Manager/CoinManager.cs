using System.IO;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    private PlayerData playerData = new PlayerData();
    private string savePath;

    public TextMeshProUGUI coinText;

    private void Awake()
    {
        // Define the save file path
        savePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    }

    private void Start()
    {
        LoadCoins();
    }

    void Update()
    {
        coinText.text = playerData.coinCount.ToString();
    }

    // Add coins and save the updated data
    public void AddCoins(int amount)
    {
        playerData.coinCount += amount;
        SaveCoins();
    }

    private void SaveCoins()
    {
        // Convert the data to JSON format
        string json = JsonUtility.ToJson(playerData);

        // Write JSON to a file
        File.WriteAllText(savePath, json);
    }

    private void LoadCoins()
    {
        // Check if the save file exists
        if (File.Exists(savePath))
        {
            // Read JSON from the file
            string json = File.ReadAllText(savePath);

            // Deserialize JSON to the PlayerData object
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            Debug.Log("No save file found. Starting with 0 coins.");
        }
    }

    public int GetCoinCount()
    {
        return playerData.coinCount;
    }

    public void ResetCoins()
    {
        playerData.coinCount = 0;
        SaveCoins();
    }
}
