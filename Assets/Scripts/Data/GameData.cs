using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData _instance;
    public static GameData Instance
    {
        get
        {
            if (_instance == null)
            {
                // Search for an existing instance in the scene
                _instance = FindAnyObjectByType<GameData>();

                // If no instance is found, create a new one
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("GameData");
                    _instance = gameObject.AddComponent<GameData>();
                }
            }
            return _instance;
        }
    }

    //coins
    public int coinCount;

    //amount of purchased items in inventory
    public int gigaBellerCount;
    public int doubleJumpCount;
    public int coinMagnetCount;
    public int halsBandCount;
    public int doubleCoinCount;
    public int checkPointCount;
    public int reviveCount;

    //equipped powerUps
    public PowerUps firstPowerUp;
    public PowerUps secondPowerUp;

    //player
    public int playerHealth;

    //level checkpoints
    public int[] levelStart = new int[3]; //contains the starting scene index for each level
    public int[] levelRemain = new int[3]; //cointains the amount of active checkpoints remaining in each level

    //other
    public bool tutorialCompleted;
    public bool readDisclaimer;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Preserve this instance across scenes

            LoadData();
            readDisclaimer = false;
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Example method to save data
    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/GameData.json", jsonData);
        //Debug.Log("Data saved to " + Application.persistentDataPath);
    }

    // Example method to load data
    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/GameData.json";
        if (System.IO.File.Exists(filePath))
        {
            string jsonData = System.IO.File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(jsonData, this);
            Debug.Log("Data loaded from " + filePath);
        }
    }
}
