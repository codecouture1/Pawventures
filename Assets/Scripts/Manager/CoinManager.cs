using System.IO;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Update()
    {
        coinText.text = GameData.Instance.coinCount.ToString();

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                AddCoins(1000);
            }
        }
    }

    // Add coins and save the updated data
    public void AddCoins(int amount)
    {
        GameData.Instance.coinCount += amount;
        GameData.Instance.SaveData();
    }

    public void RemoveCoins(int amount)
    {
        GameData.Instance.coinCount -= amount;
        GameData.Instance.SaveData();
    }

    public int GetCoinCount()
    {
        return GameData.Instance.coinCount;
    }

    public void ResetCoins()
    {
        GameData.Instance.coinCount = 0;
        GameData.Instance.SaveData();
    }
}
