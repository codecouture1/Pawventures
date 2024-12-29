using NUnit.Framework.Constraints;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class LevelBuilder : MonoBehaviour
{
    private Transform parentTransform; //transform of the empty parentobject

    public int numberOfModules; //the number of modules to be spawned
    public float coinSpawnProbability;
    public float powerupSpawnProbability;
    public GameObject[] modules; //Array containing all modules that can be spawned

    private GameObject currentPrefab; //the current prefab spawn
    private GameObject previous; //the previous prefab spawn
    private float x = 0; //the x coordinate of the current spawn

    private const int coinIndex = 0; //the coin container must always have the index 0 within the prefab
    private const int powerUpIndex = 1; //the powerup container must always have the index 0 within the prefab

    private void Awake()
    {
        parentTransform = GetComponent<Transform>();
    }

    void Start()
    {
        for (int c = 0; c < numberOfModules; c++)
        {
            //pick a random prefab from modules[]
            currentPrefab = modules[Random.Range(0, modules.Length)];

            if(previous != null)            
                x += GetPrefabWidth(previous);               

            //spawn prefab and attatch it to the parent object
            GameObject currentCopy = Instantiate(currentPrefab, new Vector3(x, 0, 0), Quaternion.identity, parentTransform);

            // Spawn coins or powerup
            if (SpawnPowerUp(currentCopy, ChanceOf(powerupSpawnProbability)))
            {
                // PowerUp spawned, ensure no coins are spawned
                Debug.Log($"PowerUp Spawned at {c}. No coins here.");
            }
            else if (SpawnCoins(currentCopy, ChanceOf(coinSpawnProbability)))
            {
                // Coins spawned, ensure no powerup exists
                Debug.Log($"Coins Spawned at {c}. No powerups here.");
            }
            else
            {
                Debug.Log($"Neither Coins nor PowerUp spawned at {c}.");
            }

            //update previous spawn for next loop iteration
            previous = currentPrefab;
        }
    }



    float GetPrefabWidth(GameObject prefab)
    {
        // Get the Renderer component of the prefab
        Renderer renderer = prefab.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Return the width of the prefab (size in the x-axis)
            return renderer.bounds.size.x;
        }

        // If no Renderer is found, return a default width
        return 1f;
    }

    private bool SpawnCoins(GameObject currentCopy, bool condition)
    {
        if (condition)
        {
            GameObject coins = currentCopy.transform.GetChild(coinIndex).gameObject;
            if(coins.CompareTag("CoinContainer")) //checks if object is a CoinContainer        
               coins.SetActive(true);
            return true; //coins were spawned
        }
        return false; //coins were NOT spawned
    }

    private bool SpawnPowerUp(GameObject currentCopy, bool condition)
    {
        if (condition)
        {
            GameObject powerUp = currentCopy.transform.GetChild(powerUpIndex).gameObject;
            if (powerUp.CompareTag("PowerUp")) //checks if object is a PowerUp
                powerUp.SetActive(true);
            return true; //powerup was spawned
        }
        return false; //powerup was NOT spawned
    }

    private bool ChanceOf(float chance)
    {
        if (Random.Range(0f, 1f) <= chance)
            return true;

        else return false;
    }
}
