using UnityEngine;

//this script resets certain variables within the Game Data file to ensure certain conditions are met whenever Homebase is loaded
public class ResetGameData : MonoBehaviour
{
    void Awake()
    {
        //ensure that the player always starts a new level with 1 HP
        GameData.Instance.playerHealth = 1;

        //ensure that the player loses all their items on level exit
        GameData.Instance.firstPowerUp = PowerUps.None;
        GameData.Instance.secondPowerUp = PowerUps.None;

        GameData.Instance.SaveData();
    }
}
