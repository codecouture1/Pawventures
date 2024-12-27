using UnityEngine;
using System.IO;

public static class PlayerDataManager
{
    private static readonly string savePath = Path.Combine(Application.persistentDataPath, "playerData.json"); //Path of Data file

    public static PlayerData LoadData()
    {
        if (File.Exists(savePath))
        {
            // Read JSON from the file
            string json = File.ReadAllText(savePath);

            // Deserialize JSON to the PlayerData object
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            return playerData;
        }
        else
        {
            Debug.Log("No save file found.");
            return null;
        }
    }

    //save changes made to inventory to playerdata
    public static void SaveData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);

        // Write JSON to a file
        File.WriteAllText(savePath, json);
    }
}
